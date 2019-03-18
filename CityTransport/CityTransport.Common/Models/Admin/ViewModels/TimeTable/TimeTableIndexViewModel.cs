namespace CityTransport.Common.Models.Admin.ViewModels.TimeTable
{
    using CityTransport.Common.Models.Admin.InputModels.Route;

    public class TimeTableIndexViewModel
    {
        public CreateRouteInputModel CreateRoute { get; set; }
       
        public bool HasRoutes { get; set; }

        public string Tab { get; set; }

    }
}
