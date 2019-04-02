namespace CityTransport.Common.Models.Main.ViewModels.TimeTables
{
    using System;
    using System.Collections.Generic;

    public class RoutesViewModel
    {
        public RoutesViewModel()
        {
            this.DepartureTimes = new HashSet<TimeSpan>();
        }

        public string StationName { get; set; }

        public IEnumerable<TimeSpan> DepartureTimes { get; set; }
    }
}
