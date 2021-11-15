using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TripLogDaAL.Entities
{
    public class Trip
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TripId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please enter the date your oldTrip starts.")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please enter the date your oldTrip ends.")]
        public DateTime EndDate { get; set; }

        [ForeignKey("DestinationId")]

        public Destination Destination { get; set; }

        public int DestinationId { get; set; }
        
        [NotMapped]
        public IEnumerable<int> SelectedActivities { get; set; }


        [ForeignKey("AccommodationId")]
        public virtual Accommodation Accommodation { get; set; }

        public int? AccommodationId { get; set; }

        public virtual ICollection<TripActivity> TripActivities { get; set; }

        
    }
}
