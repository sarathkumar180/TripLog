using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TripLogDaAL.Entities
{
    public class Accommodation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccommodationId { get; set; }

        public string AccommodationName { get; set; }

        public string AccommodationPhone { get; set; }

        public string AccommodationEmail { get; set; }
        
        public ICollection<Trip> Trips { get; set; }
    }

}
