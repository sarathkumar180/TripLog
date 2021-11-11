using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripLog.Models
{
    public class OldTrip
    {
        public int TripId { get; set; } 

        [Required(ErrorMessage = "Please enter a destination.")]
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

    public class Trip
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TripId { get; set; }
        public DateTime StartDate { get; set; }
        public string EndDate { get; set; }
        
        public ICollection<Destination> Destinations { get; set; }

        public virtual ICollection<TripActivity> TripActivities { get; set; }

    }

    public class Destination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DestinationId { get; set; }

        public string DestinationName { get; set; }

        [ForeignKey("TripId")]
        public Trip Trip { get; set; }

        public int TripId { get; set; }

        public ICollection<Accommodation> Accommodations { get; set; }
           

    }

    public class Accommodation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccommodationId { get; set; }

        public string AccommodationName { get; set; }

        public string AccommodationPhone { get; set; }

        public string AccommodationEmail { get; set; }

        [ForeignKey("DestinationId")]
        public Destination Destination { get; set; }

        public int DestinationId { get; set; }
        
        
    }

    public class Activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityId { get; set; }

        public string ActivityName { get; set; }

        public virtual ICollection<TripActivity> TripActivities { get; set; }

    }

    public class TripActivity
    {
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

    }


}
