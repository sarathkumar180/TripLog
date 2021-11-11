using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TripLog.Models;

namespace TripLog.Controllers
{
    public class HomeController : Controller
    {

        public ViewResult Index()
        {
            var trips = new List<OldTrip>();
            if (TempData[nameof(OldTrip.Destination)] != null)
            {
                OldTrip oldTrip = new OldTrip();
                oldTrip.Destination = TempData[nameof(OldTrip.Destination)].ToString();
                oldTrip.Accommodation = TempData[nameof(OldTrip.Accommodation)]?.ToString();
                oldTrip.StartDate = (DateTime)TempData[nameof(OldTrip.StartDate)];
                oldTrip.EndDate = (DateTime)TempData[nameof(OldTrip.EndDate)];
                oldTrip.AccommodationPhone = TempData[nameof(OldTrip.AccommodationPhone)]?.ToString();
                oldTrip.AccommodationEmail = TempData[nameof(OldTrip.AccommodationEmail)]?.ToString();
                oldTrip.ThingToDo1 = TempData[nameof(OldTrip.ThingToDo1)]?.ToString();
                oldTrip.ThingToDo2 = TempData[nameof(OldTrip.ThingToDo2)]?.ToString();
                oldTrip.ThingToDo3 = TempData[nameof(OldTrip.ThingToDo3)]?.ToString();
                trips.Add(oldTrip);
            }
            return View(trips);
        }

    }
}
