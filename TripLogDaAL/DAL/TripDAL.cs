using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TripLogDaAL.DBContext;
using TripLogDaAL.Entities;

namespace TripLogDaAL.DAL
{
    public class TripDAL
    {
        private readonly TripDbContext _context;

        public TripDAL(TripDbContext context)
        {
            _context = context;
        }

        public List<Trip> GetAllTrips()
        {
            return _context.Trips
                .Include(x => x.Accommodation)
                .Include(y => y.Destination)
                .Include(z => z.TripActivities).ThenInclude(p => p.Activity)
                .ToList();
        }
        public List<Destination> GetAllDestinations()
        {
            return _context.Destinations.ToList();
        }

        public List<Accommodation> GetAllAccommodations()
        {
            return _context.Accommodations.ToList();
        }

        public List<Activity> GetAllActivity()
        {
            return _context.Activities.ToList();
        }

        public bool AddTrip(Trip tripEntity)
        {

            using (_context)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {


                        var trip = _context.Trips.Add(tripEntity);
                        _context.SaveChanges();

                        if (tripEntity.TripId > 0 && tripEntity.SelectedActivities != null && tripEntity.SelectedActivities.Any())
                        {
                            foreach (int activityId in tripEntity.SelectedActivities)
                            {
                                _context.TripActivities.Add(new TripActivity()
                                    {TripId = tripEntity.TripId, ActivityId = activityId});
                                _context.SaveChanges();
                            }
                        }
                        else
                        {
                            transaction.Rollback();
                            return false;
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }




            _context.Trips.Add(tripEntity);
            _context.SaveChanges();
        }

        public void AddDestination(Destination destEntity)
        {
            _context.Destinations.Add(destEntity); 
            _context.SaveChanges();
        }

        public void AddAccommodation(Accommodation accommodationEntity)
        {
            _context.Accommodations.Add(accommodationEntity);
            _context.SaveChanges();
        }

        public void AddActvity(Activity activityEntity)
        {
            _context.Activities.Add(activityEntity);
            _context.SaveChanges();
        }

        public void DeleteDestination(int id)
        {
            var destEntity = _context.Destinations.FirstOrDefault(x => x.DestinationId == id);
            if (destEntity != null)
            {
                _context.Destinations.Remove(destEntity);
                _context.SaveChanges();
            }
        }

        public void DeleteAccommodation(int id)
        {
            var accommodationEntity = _context.Accommodations.FirstOrDefault(x => x.AccommodationId == id);
            if (accommodationEntity != null)
            {
                _context.Accommodations.Remove(accommodationEntity);
                _context.SaveChanges();
            }
        }

        public void DeleteActivity(int id)
        {
            var activityEntity = _context.Activities.FirstOrDefault(x => x.ActivityId == id);
            if (activityEntity != null)
            {
                _context.Activities.Remove(activityEntity);
                _context.SaveChanges();
            }
        }

        public void DeleteTrip(int id)
        {
            var tripEntity = _context.Trips.FirstOrDefault(x => x.TripId == id);
            if (tripEntity != null)
            {
                _context.Trips.Remove(tripEntity);
                _context.SaveChanges();
            }
        }

    }
}
