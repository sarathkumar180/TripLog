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
    public class AccommodationController : Controller
    {
        public readonly TripDbContext _context;
        public AccommodationController(TripDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string errormessage = "")
        {
            TripDAL dal = new TripDAL(_context);
            var allAccommodations = dal.GetAllAccommodations();
            AccommodationViewModel vm = new AccommodationViewModel();
            vm.Accommodations = allAccommodations;
            if (allAccommodations != null && allAccommodations.Any())
            {
                vm.SelectedAccommodationId = allAccommodations[0].AccommodationId;
                vm.SelectedAccommodationName = allAccommodations[0].AccommodationName;
            }

            TempData["message"] = errormessage;
            return View(vm);
        }

        public IActionResult AddAccommodation(string NewAccommodationName, string NewAccommodationPhone,string NewAccommodationEmail)
        {
            string error = string.Empty;
            if (string.IsNullOrEmpty(NewAccommodationName))
            {
                error = "Enter Accommodation Name";

                return RedirectToAction("Index", new { errorMessage = error });
            }
            TripDAL dal = new TripDAL(_context);
            var allAccommodations = dal.GetAllAccommodations();
            if (allAccommodations != null && allAccommodations.Any())
            {
                if (allAccommodations.Any(x => x.AccommodationName.ToLower() == NewAccommodationName.ToLower()))
                {
                    error = "Accommodation Name already exists";
                    return RedirectToAction("Index", new { errorMessage = error });
                }
            }

            Accommodation newAccommodation = new Accommodation() {  AccommodationName = NewAccommodationName,AccommodationEmail =  NewAccommodationEmail, AccommodationPhone = NewAccommodationPhone};
            bool success = dal.AddAccommodation(newAccommodation);
            if (success)
            {
                TempData["message"] = $"Acommodation - {NewAccommodationName} added successfully";
                return RedirectToAction("Add", "Trip");
            }
            else
            {
                error = $"DBError : Cannot create new Accommodation - {NewAccommodationName}. Please try again";
                return RedirectToAction("Index", new { errorMessage = error });
            }
        }

        public IActionResult DeleteAccommodation(int SelectedAccommodationId, string SelectedAccommodationName)
        {
            string error = string.Empty;
            TripDAL dal = new TripDAL(_context);
            bool success = dal.DeleteAccommodation(SelectedAccommodationId);
            if (success)
            {
                TempData["message"] = $"Accommodation  {SelectedAccommodationName} deleted successfully";
                return RedirectToAction("Add", "Trip");

            }
            else
            {
                error = "DBError : Cannot delete Accommodation - {SelectedAccommodationName}";
                return RedirectToAction("Index", new { errorMessage = error });
            }

        }
    }
}
