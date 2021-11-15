using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripLog.Models;
using TripLogDaAL.DAL;
using TripLogDaAL.DBContext;
using TripLogDaAL.Entities;

namespace TripLog.Controllers
{
    public class DestinationController : Controller
    {
        public readonly TripDbContext _context;
        public DestinationController(TripDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string errormessage = "")
        {
            TripDAL dal = new TripDAL(_context);
            var allDestinations = dal.GetAllDestinations();
            var newDestination = new Destination();
            DestinationViewModel vm = new DestinationViewModel();
            vm.Destinations = allDestinations;
            if (allDestinations != null && allDestinations.Any())
            {
                vm.SelectedDestinationId = allDestinations[0].DestinationId;
                vm.SelectedDestinationName = allDestinations[0].DestinationName;
            }

            TempData["message"] = errormessage;
            return View(vm);
        }

        public IActionResult AddDestination(string NewDestinationName)
        {
            string error = string.Empty;
            if (string.IsNullOrEmpty(NewDestinationName))
            {
                error = "Enter Destination Name";

                return RedirectToAction("Index", new { errorMessage = error });
            }
            TripDAL dal = new TripDAL(_context);
            var allDestinations = dal.GetAllDestinations();
            if (allDestinations != null && allDestinations.Any())
            {
                if (allDestinations.Any(x => x.DestinationName.ToLower() == NewDestinationName.ToLower()))
                {
                    error = "Destination Name already exists";
                    return RedirectToAction("Index", new { errorMessage = error });
                }
            }

            Destination newDest = new Destination() { DestinationName = NewDestinationName };
            bool success = dal.AddDestination(newDest);
            if (success)
            {
                TempData["message"] = $"Destination - {NewDestinationName} added successfully";
                return RedirectToAction("Add", "Trip");
            }
            else
            {
                error = "DBError : Cannot create new destination. Please try again";
                return RedirectToAction("Index", new { errorMessage = error });
            }
        }
        
        public IActionResult DeleteDestination(int SelectedDestinationId, string SelectedDestinationName)
        {
            string error = string.Empty;
            TripDAL dal = new TripDAL(_context);
            bool success = dal.DeleteDestination(SelectedDestinationId);
            if (success)
            {
                TempData["message"] = $"Destination  {SelectedDestinationName} deleted successfully";
                return RedirectToAction("Add", "Trip");
                
            }
            else
            {
                error = "DBError : Cannot delete destination. Make sure no trips are mapped against selected destination";
                return RedirectToAction("Index", new { errorMessage = error });
            }
            
        }
    }
}
