using CityTransport.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace CityTransport.Common.Models.Admin.InputModels.Station
{
    public class CreateStationInputModel
    {
        [Required]
        [MinLength(ValidationConstants.StationNameMinLength,
            ErrorMessage =ValidationConstants.StationNameMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.StationNameMaxLength, 
            ErrorMessage = ValidationConstants.StationNameMaxLengthErrorMessage)]
        public string StationName { get; set; }

        [Required]
        [MinLength(ValidationConstants.StationCodeMinLength,
            ErrorMessage = ValidationConstants.StationCodeMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.StationCodeMaxLength,
            ErrorMessage = ValidationConstants.StationCodeMaxLengthErrorMessage)]
        [RegularExpression("^[0-9]+$", ErrorMessage = ValidationConstants.StationCodeRegexErrorMessage)]
        public string StationCode { get; set; }
    }
}
