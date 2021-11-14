using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripLog.Models
{
    public class OldTrip
    {
        public int TripId { get; set; } 

        public string Destination { get; set; }

        [Required(ErrorMessage = "Please enter the date your oldTrip starts.")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Please enter the date your oldTrip ends.")]
        public DateTime? EndDate { get; set; }

        public string Accommodation { get; set; }
        public string AccommodationPhone { get; set; }
        public string AccommodationEmail { get; set; }

        public string ThingToDo1 { get; set; }
        public string ThingToDo2 { get; set; }
        public string ThingToDo3 { get; set; }
    }

    

    

    
    

   

}
