namespace CityTransport.Services.Admin.Interfaces
{
    using CityTransport.Common.Models;
    using CityTransport.Common.Models.Admin.InputModels.Route;

    public interface IRouteService
    {
        BaseModel CreateRoute(CreateRouteInputModel model);

    }
}
