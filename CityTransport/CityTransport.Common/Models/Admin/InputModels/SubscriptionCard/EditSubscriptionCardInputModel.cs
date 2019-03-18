namespace CityTransport.Common.Models.Admin.InputModels.SubscriptionCard
{
    using CityTransport.Common.Constants;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EditSubscriptionCardInputModel
    {
        public EditSubscriptionCardInputModel()
        {
            this.ValidityCards = new HashSet<SelectListItem>();
        }

        public int Id { get; set; }

        [Required]
        public string ValidityCard { get; set; }

        [Required]
        [Range(typeof(decimal),
            ValidationConstants.SubscriptionCardMinPrice, ValidationConstants.SubscriptionCardMaxPrice,
            ErrorMessage = ValidationConstants.SubscriprionCardPriceErrorMessage)]
        public decimal Price { get; set; }

        public IEnumerable<SelectListItem> ValidityCards { get; set; }

    }
}
