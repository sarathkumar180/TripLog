using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TripLogDaAL.Entities
{
    public class Destination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DestinationId { get; set; }

        public string DestinationName { get; set; }

        public ICollection<Trip> Trips { get; set; }



    }
}
