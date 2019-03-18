namespace CityTransport.Common.Models.Admin.InputModels.Line
{
    using CityTransport.Common.Constants;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateLineInputModel
    {
        public CreateLineInputModel()
        {
            this.TransportTypes = new HashSet<SelectListItem>();
        }

        [Required]
        [MaxLength(ValidationConstants.LineNameMaxLength,
             ErrorMessage = ValidationConstants.LineNameMaxLengthErrorMessage)]
        [RegularExpression(@"^[A-Z]{0,1}\d+[A-Z]{0,1}$", ErrorMessage = ValidationConstants.LineNameRegexErrorMessage)]
        public string LineName { get; set; }

        [Required]
        public string TypeTransport { get; set; }

        public IEnumerable<SelectListItem> TransportTypes { get; set; }

    }
}
