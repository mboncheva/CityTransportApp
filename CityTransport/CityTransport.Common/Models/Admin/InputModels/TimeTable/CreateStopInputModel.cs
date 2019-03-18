namespace CityTransport.Common.Models.Admin.InputModels.TimeTable
{
    using CityTransport.Common.Constants;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateStopInputModel
    {
        public CreateStopInputModel()
        {
            this.Stations = new HashSet<SelectListItem>();
        }

        [Required]
        public int RouteId { get; set; }

        [Required]
        [MinLength(ValidationConstants.StationNameMinLength,
             ErrorMessage = ValidationConstants.StationNameMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.StationNameMaxLength,
             ErrorMessage = ValidationConstants.StationNameMaxLengthErrorMessage)]
        public string StationName { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan DepartureTime { get; set; }

        public IEnumerable<SelectListItem> Stations { get; set; }

    }
}
