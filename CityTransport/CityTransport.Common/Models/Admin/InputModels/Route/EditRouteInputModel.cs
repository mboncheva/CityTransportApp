namespace CityTransport.Common.Models.Admin.InputModels.Route
{
    using CityTransport.Common.Constants;
    using System.ComponentModel.DataAnnotations;

    public class EditRouteInputModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.RouteNameMinLength,
            ErrorMessage = ValidationConstants.RouteNameMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.RouteNameMaxLength,
           ErrorMessage = ValidationConstants.RouteNameMaxLengthErrorMessage)]
        public string RouteName { get; set; }

        [Required]
        public string Day { get; set; }
    }
}
