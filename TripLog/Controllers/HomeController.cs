using System;
using Microsoft.AspNetCore.Mvc;
using TripLog.Models;
using TripLogDaAL.DAL;
using TripLogDaAL.DBContext;


namespace TripLog.Controllers
{
    public class HomeController : Controller
    {
        private readonly TripDbContext _context;
        public HomeController(TripDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TripDAL dal = new TripDAL(_context);
            var trips = dal.GetAllTrips();
            return View(trips);
        }

        public IActionResult Delete(int TripId)
        {
            TripDAL dal = new TripDAL(_context);
            bool success = dal.DeleteTrip(TripId);
            if (success)
                TempData["message"] = "Trip deleted successfully";
            else
                TempData["message"] = "DB Error : Failed to delete trip. Please try agian";

            return RedirectToAction("Index");
        }

    }
}
