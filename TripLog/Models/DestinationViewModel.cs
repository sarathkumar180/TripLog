using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripLogDaAL.Entities;

namespace TripLog.Models
{
    public class DestinationViewModel
    {
        public  List<Destination> Destinations { get; set; }
        public string NewDestinationName { get; set; }
        public string SelectedDestinationName { get; set; }
        public int SelectedDestinationId { get; set; }
        

    }
}
