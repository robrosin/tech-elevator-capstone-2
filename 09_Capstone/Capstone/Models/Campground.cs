using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Campground
    {
        public int CampgroundId { get; set; }
        public int ParkId { get; set; }
        public string Name { get; set; }
        public int OpenFrom { get; set; }
        public int OpenTo { get; set; }
        public decimal DailyFee { get; set; }

    }
}
