namespace CityTransport.Common.Models.Admin.InputModels.CustomerCard
{
    using CityTransport.Common.Constants;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EditCustomerCardInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string CustomerCardNumber { get; set; }

        [Required]
        public string TypeCard { get; set; }

        [Required]
        [Range(ValidationConstants.CountTripsMin, ValidationConstants.CountTripsMax,
             ErrorMessage = ValidationConstants.CountTripsMinErrorMessage)]
        public int CountTrips { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ValidateFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime ValidateTo { get; set; }
    }
}
