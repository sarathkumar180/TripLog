using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TripLogDaAL.Entities;

namespace TripLog.Models
{
    public class AccommodationViewModel
    {
        public  List<Accommodation> Accommodations { get; set; }
        [Required]
        public string NewAccommodationName { get; set; }

        public string NewAccommodationPhone { get; set; }
         
        [DataType(DataType.EmailAddress)]
        public string NewAccommodationEmail { get; set; }

        public string SelectedAccommodationName { get; set; }
        public int SelectedAccommodationId { get; set; }
        

    }
}
