namespace CityTransport.Common.Models.Admin.InputModels.Line
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddStationToLineInputModel
    {
        public AddStationToLineInputModel()
        {
            this.LineStations = new HashSet<SelectListItem>();
            this.Stations = new HashSet<SelectListItem>();
        }

        [Required]
        public string StationName { get; set; }

        public IEnumerable<SelectListItem> LineStations { get; set; }

        public IEnumerable<SelectListItem> Stations { get; set; }

    }
}
