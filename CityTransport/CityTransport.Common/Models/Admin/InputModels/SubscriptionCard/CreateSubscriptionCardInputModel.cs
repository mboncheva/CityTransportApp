namespace CityTransport.Common.Models.Admin.InputModels.SubscriptionCard
{
    using CityTransport.Common.Constants;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateSubscriptionCardInputModel
    {
        public CreateSubscriptionCardInputModel()
        {
            this.CardTypes = new HashSet<SelectListItem>();
            this.ValidityCards = new HashSet<SelectListItem>();
        }

        [Required]
        public string TypeCard { get; set; }

        [Required]
        public string ValidityCard { get; set; }

        [Required]
        [Range(ValidationConstants.CountTripsMin,ValidationConstants.CountTripsMax,
             ErrorMessage = ValidationConstants.CountTripsMinErrorMessage)]
        public int CountTrips { get; set; }

        [Required]
        [Range(typeof(decimal), 
            ValidationConstants.SubscriptionCardMinPrice, ValidationConstants.SubscriptionCardMaxPrice, 
            ErrorMessage = ValidationConstants.SubscriprionCardPriceErrorMessage)]
        public decimal Price { get; set; }

        public IEnumerable<SelectListItem> CardTypes { get; set; }

        public IEnumerable<SelectListItem> ValidityCards { get; set; }

    }
}
