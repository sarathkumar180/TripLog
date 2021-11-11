using System;
using Microsoft.AspNetCore.Mvc;
using TripLog.DBContext;
using TripLog.Models;

namespace TripLog.Controllers
{
    public class TripController : Controller
    {
        public TripController(TripDbContext context)
        {

        }
        public RedirectToActionResult Index() => RedirectToAction("Index", "Home");

        [HttpGet]
        public ViewResult Add(string id = "")
        {
            var vm = new TripViewModel();

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

            if (id.ToLower() == "page2")
            {
                var accommodation = TempData[nameof(OldTrip.Accommodation)]?.ToString();

                if (string.IsNullOrEmpty(accommodation)) {  // skip to page 3
                    vm.PageNumber = 3;
                    var destination = TempData.Peek(nameof(OldTrip.Destination)).ToString();
                    vm.OldTrip = new OldTrip { Destination = destination };
                    return View("Add3", vm);  
                }
                else {
                    vm.PageNumber = 2;
                    vm.OldTrip = new OldTrip { Accommodation = accommodation };
                    TempData.Keep(nameof(OldTrip.Accommodation));
                    return View("Add2", vm);  
                }
            }  
            else if (id.ToLower() == "page3") 
            {
                vm.PageNumber = 3;
                vm.OldTrip = new OldTrip { Destination = TempData.Peek(nameof(OldTrip.Destination)).ToString() };

                return View("Add3", vm);
            }
            else 
            {
                vm.PageNumber = 1;
                return View("Add1", vm);
            }      
        }

        [HttpPost]
        public IActionResult Add(TripViewModel vm)
        {
            if (vm.PageNumber == 1)
            {
                if (ModelState.IsValid) // only page 1 has required data
                {
                    /***************************************************
                    * Store data in TempData and redirect (PRG pattern)
                    ****************************************************/
                    TempData[nameof(OldTrip.Destination)] = vm.OldTrip.Destination;
                    TempData[nameof(OldTrip.Accommodation)] = vm.OldTrip.Accommodation;
                    TempData[nameof(OldTrip.StartDate)] = vm.OldTrip.StartDate;
                    TempData[nameof(OldTrip.EndDate)] = vm.OldTrip.EndDate;
                    return RedirectToAction("Add", new { id = "Page2" });
                }
                else
                {
                    return View("Add1", vm);
                }
            }
            else if (vm.PageNumber == 2)
            {
                /***************************************************
                    * Store data in TempData and redirect (PRG pattern)
                ****************************************************/
                TempData[nameof(OldTrip.AccommodationPhone)] = vm.OldTrip.AccommodationPhone;
                TempData[nameof(OldTrip.AccommodationEmail)] = vm.OldTrip.AccommodationEmail;
                return RedirectToAction("Add", new { id = "Page3" });
            }
            else if (vm.PageNumber == 3)
            {
                /***************************************************
                    *  Store data in TempData and redirect (PRG pattern)
                ****************************************************/
               
                TempData[nameof(OldTrip.ThingToDo1)] = vm.OldTrip.ThingToDo1;
                TempData[nameof(OldTrip.ThingToDo2)] = vm.OldTrip.ThingToDo2;
                TempData[nameof(OldTrip.ThingToDo3)] = vm.OldTrip.ThingToDo3;

                TempData["message"] = $"The Following oldTrip to {TempData.Peek(nameof(OldTrip.Destination)).ToString()} has been added.";
           
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