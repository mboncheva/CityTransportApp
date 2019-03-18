namespace CityTransport.Services.Admin.Interfaces
{
    using CityTransport.Common.Models;
    using CityTransport.Common.Models.Admin.InputModels.CustomerCard;
    using System.Threading.Tasks;

    public interface ICustomerCardService
    {
       Task<BaseModel> CreateCustomerCard(CreateCustomerCardInputModel createCustomerCard, string id);

        Task<BaseModel> EditCustomerCard(EditCustomerCardInputModel editCustomerCard, string id);

    }
}
