using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanySelector.Models
{
    public class  AppFeatures
    {
        public MainScreen mainScreen { get; set; }
    }

    public class MainScreen
    {
        public bool flightStatus { get; set; }
        public bool itinerary { get; set; }
        public bool hotelBookingStatus { get; set; }
        public bool flightBookingStatus { get; set; }
        public bool emergencyContact { get; set; }
    }
}
