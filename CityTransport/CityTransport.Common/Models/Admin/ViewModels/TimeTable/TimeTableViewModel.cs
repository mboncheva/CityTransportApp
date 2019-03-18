namespace CityTransport.Common.Models.Admin.ViewModels.TimeTable
{
    using System;

    public class TimeTableViewModel
    {
        public int Id { get; set; }

        public string StationName { get; set; }

        public TimeSpan DepartureTime { get; set; }
    }
}
