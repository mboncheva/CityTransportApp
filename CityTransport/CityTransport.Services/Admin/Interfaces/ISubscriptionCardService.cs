namespace CityTransport.Services.Admin.Interfaces
{
    using CityTransport.Common.Models;
    using CityTransport.Common.Models.Admin.InputModels.SubscriptionCard;
    using CityTransport.Common.Models.Admin.ViewModels.SubscriptionCard;
    using System.Collections.Generic;

    public interface ISubscriptionCardService
    {
        ICollection<SubscriptionCardsViewModel> SubscriptionCards();

        BaseModel CreateSubscriptionCard(CreateSubscriptionCardInputModel model);

        BaseModel EditSubscriptionCard(EditSubscriptionCardInputModel model);

        EditSubscriptionCardInputModel GetEditSubscriptionCardModel(int id);

    }
}
