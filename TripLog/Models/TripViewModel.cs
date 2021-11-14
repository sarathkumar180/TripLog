using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using TripLogDaAL.Entities;

namespace TripLog.Models
{
    public class TripViewModel
    {
        public OldTrip OldTrip { get; set; }
        public int PageNumber { get; set; }
        public Trip trip { get; set; }
        public List<Destination> Destinations { get; set; }
        public List<Accommodation> Accommodations { get; set; }
        public List<Activity> Activities { get; set; }
        public  string SelectedDestinationName { get; set; }
    }
}
