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
    public class ActivityController : Controller
    {
        public readonly TripDbContext _context;
        public ActivityController(TripDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string errormessage = "")
        {
            TripDAL dal = new TripDAL(_context);
            var allActivities = dal.GetAllActivity();
            var activity = new Activity();
            ActivityViewModel vm = new ActivityViewModel();
            vm.Activities = allActivities;
            if (allActivities != null && allActivities.Any())
            {
                vm.SelectedActivityId = allActivities[0].ActivityId;
                vm.SelectedActivityName = allActivities[0].ActivityName;
            }

            TempData["message"] = errormessage;
            return View(vm);
        }

        public IActionResult AddActivity(string NewActivityName)
        {
            string error = string.Empty;
            if (string.IsNullOrEmpty(NewActivityName))
            {
                error = "Enter Activity Name";

                return RedirectToAction("Index", new { errorMessage = error });
            }
            TripDAL dal = new TripDAL(_context);
            var allActivities = dal.GetAllActivity();
            if (allActivities != null && allActivities.Any())
            {
                if (allActivities.Any(x => x.ActivityName.ToLower() == NewActivityName.ToLower()))
                {
                    error = "Activity Name already exists";
                    return RedirectToAction("Index", new { errorMessage = error });
                }
            }

            Activity newActivity = new Activity() {  ActivityName = NewActivityName};
            bool success = dal.AddActvity(newActivity);
            if (success)
            {
                TempData["message"] = $"Activity - {NewActivityName} added successfully";
                return RedirectToAction("Add", "Trip");
            }
            else
            {
                error = $"DBError : Cannot create new Activity - {NewActivityName}. Please try again";
                return RedirectToAction("Index", new { errorMessage = error });
            }
        }

        public IActionResult DeleteActivity(int SelectedActivityId, string SelectedActivityName)
        {
            string error = string.Empty;
            TripDAL dal = new TripDAL(_context);
            bool success = dal.DeleteActivity(SelectedActivityId);
            if (success)
            {
                TempData["message"] = $"Activity-   {SelectedActivityName} deleted successfully";
                return RedirectToAction("Add", "Trip");

            }
            else
            {
                error = $"DBError : Cannot delete Activity - {SelectedActivityName}.";
                return RedirectToAction("Index", new { errorMessage = error });
            }

        }
    }
}
