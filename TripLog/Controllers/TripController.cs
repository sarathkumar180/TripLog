using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using TripLog.Models;
using TripLogDaAL.DAL;
using TripLogDaAL.DBContext;
using TripLogDaAL.Entities;

namespace TripLog.Controllers
{
    public class TripController : Controller
    {
        private readonly TripDbContext _context;
        public TripController(TripDbContext context)
        {
            _context = context;
        }
        public RedirectToActionResult Index() => RedirectToAction("Index", "Home");

        [HttpGet]
        public ViewResult Add(string id = "")
        {
            var vm = new TripViewModel();
            TripDAL dal = new TripDAL(_context);
            vm.Destinations = dal.GetAllDestinations();
            vm.Accommodations = dal.GetAllAccommodations();
            vm.Activities = dal.GetAllActivity();   
            /********************************************************************************************
            * need to pass Accommodation value, or Destination value (depending on page number and 
            * Accommodation value), to by view. 
            * 
            * Accommodation is an optional value - don't need it to persist after being read if it's null.
            * So, do straight read, and if it's not null, use Keep() method to make sure it persists.
            * 
            * Destination is a a required value - always need it to persist after being read. 
            * So, use Peek() method to read it and make sure it persists.
            *********************************************************************************************/
               
            if (id.ToLower() == "page3")
            {
                vm.PageNumber = 3;
                vm.trip = new Trip
                {
                    DestinationId = (int)TempData.Peek(nameof(Trip.DestinationId)),
                    AccommodationId = (int)TempData.Peek(nameof(Trip.AccommodationId)),
                    StartDate = (DateTime)TempData.Peek(nameof(Trip.StartDate)),
                    EndDate = (DateTime)TempData.Peek(nameof(Trip.EndDate)),
                };
                vm.SelectedDestinationName = TempData.Peek(nameof(TripViewModel.SelectedDestinationName)).ToString();
                return View("Add3", vm);
            }
            else 
            {
                vm.PageNumber = 1;
                if (vm.Destinations != null && vm.Destinations.Any())
                {
                    vm.SelectedDestinationName = vm.Destinations[0].DestinationName;
                }
                return View("Add1", vm);
            }      
        }
            
        [HttpPost]
        public IActionResult Add(TripViewModel vm)
        {
            TripDAL dal = new TripDAL(_context);
            if (vm.PageNumber == 1)
            {
                if (ModelState.IsValid) // only page 1 has required data
                {
                    /***************************************************
                    * Store data in TempData and redirect (PRG pattern)
                    ****************************************************/
                    TempData[nameof(Trip.AccommodationId)] = vm.trip.AccommodationId;
                    TempData[nameof(Trip.DestinationId)] = vm.trip.DestinationId;
                    TempData[nameof(Trip.StartDate)] = vm.trip.StartDate;
                    TempData[nameof(Trip.EndDate)] = vm.trip.EndDate;
                    var selectedItem = dal.GetAllDestinations().Find(p => p.DestinationId == vm.trip.DestinationId);
                    TempData[nameof(TripViewModel.SelectedDestinationName)] = selectedItem?.DestinationName;
                    
                    return RedirectToAction("Add", new { id = "Page3" });
                }
                else
                {
                    return View("Add1", vm);
                }
            }
            else if (vm.PageNumber == 3)
            {
                /***************************************************
                    *  Store data in TempData and redirect (PRG pattern)
                ****************************************************/
                

                if (vm.trip != null)
                {
                    bool success = dal.AddTrip(vm.trip);
                    if (success)
                    {
                        TempData["message"] = $"The Following Trip to {TempData.Peek(nameof(TripViewModel.SelectedDestinationName))} has been added.";
                    }
                    else
                    {
                        TempData["message"] = $"Database Error : The Following Trip to {TempData.Peek(nameof(TripViewModel.SelectedDestinationName)).ToString()} has been added.";
                    }
                }
                else
                {
                    TempData["message"] = $"Failed to add trip : {TempData.Peek(nameof(TripViewModel.SelectedDestinationName)).ToString()}";
                }
                
                
           
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public RedirectToActionResult Cancel()
        {
            TempData.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}