namespace CityTransport.Common.Models.Admin.ViewModels.TimeTable
{
    using CityTransport.Common.Models.Admin.InputModels.Route;
    using CityTransport.Common.Models.Admin.InputModels.TimeTable;
    using CityTransport.Common.Models.Admin.ViewModels.Route;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TimeTableIndexWithRoutesViewModel
    {
        public TimeTableIndexWithRoutesViewModel()
        {
            this.Routes = new HashSet<RouteViewModel>();
            this.TimeTables = new HashSet<TimeTableViewModel>();
            this.DirectionTypes = new HashSet<SelectListItem>();
            this.DayTypes = new HashSet<SelectListItem>();

        }

        [Required]
        public int LineId { get; set; }

        [Required]
        public string Direction { get; set; }

        [Required]
        public string Day { get; set; }

        public string Tab { get; set; }

        public CreateRouteInputModel CreateRoute { get; set; }

        public EditRouteInputModel EditRoute { get; set; }

        public CreateStopInputModel CreateStop { get; set; }

        public IEnumerable<SelectListItem> DirectionTypes { get; set; }

        public IEnumerable<SelectListItem> DayTypes { get; set; }

        public IEnumerable<RouteViewModel> Routes { get; set; }

        public IEnumerable<TimeTableViewModel> TimeTables { get; set; }

    }
}
