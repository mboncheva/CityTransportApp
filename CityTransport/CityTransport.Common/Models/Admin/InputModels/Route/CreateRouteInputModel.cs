namespace CityTransport.Common.Models.Admin.InputModels.Route
{
    using CityTransport.Common.Constants;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateRouteInputModel
    {
        public CreateRouteInputModel()
        {
            this.DirectionTypes = new HashSet<SelectListItem>();
            this.DayTypes = new HashSet<SelectListItem>();
        }
        [Required]
        [MinLength(ValidationConstants.RouteNameMinLength,
             ErrorMessage = ValidationConstants.RouteNameMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.RouteNameMaxLength, 
            ErrorMessage = ValidationConstants.RouteNameMaxLengthErrorMessage)]
        public string RouteName { get; set; }

        [Required]
        public string Direction { get; set; }

        [Required]
        public string Day { get; set; }

        [Required]
        public int LineId { get; set; }

        public IEnumerable<SelectListItem> DirectionTypes { get; set; }

        public IEnumerable<SelectListItem> DayTypes { get; set; }

    }
}
