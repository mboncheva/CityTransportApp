namespace CityTransport.Common.Models.Main.ViewModels.TimeTables
{
    using System.Collections.Generic;

    public class TimeTableForLineViewModel
    {
        public TimeTableForLineViewModel()
        {
            this.RoutesHoliday = new HashSet<RoutesViewModel>();
            this.RoutesWorkDay = new HashSet<RoutesViewModel>();
        }

        public int LineId { get; set; }

        public string LineName { get; set; }

        public DirectionViewModel DirectionModel { get; set; }

        public IEnumerable<RoutesViewModel> RoutesHoliday { get; set; }

        public IEnumerable<RoutesViewModel> RoutesWorkDay { get; set; }

    }
}
