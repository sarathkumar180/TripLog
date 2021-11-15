using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripLogDaAL.Entities;

namespace TripLog.Models
{
    public class ActivityViewModel
    {
        public  List<Activity> Activities { get; set; }
        public string NewActivityName { get; set; }
        public string SelectedActivityName { get; set; }
        public int SelectedActivityId { get; set; }
        

    }
}
