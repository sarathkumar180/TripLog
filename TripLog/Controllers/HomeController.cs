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
        public ViewResult Index()
        {
            TripDAL dal = new TripDAL(_context);
            var trips = dal.GetAllTrips();
            return View(trips);
        }

    }
}
