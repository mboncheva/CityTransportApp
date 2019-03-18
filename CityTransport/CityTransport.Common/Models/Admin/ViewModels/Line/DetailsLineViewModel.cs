namespace CityTransport.Common.Models.Admin.ViewModels.Line
{
    using CityTransport.Common.Models.Admin.InputModels.Line;

    public class DetailsLineViewModel
    {
        public EditLineInputModel EditLine { get; set; }

        public AddStationToLineInputModel AddStationToLine { get; set; }
    }
}
