namespace CityTransport.Services.Admin.Interfaces
{
    using CityTransport.Common.Models;
    using CityTransport.Common.Models.Admin.InputModels.Station;
    using CityTransport.Common.Models.Admin.ViewModels.Station;
    using System.Collections.Generic;

    public interface IStationService
    {
        ICollection<StationViewModel> Stations();

        BaseModel CreateStation(CreateStationInputModel model);

        EditStationInputModel GetEditStationModel(int? id);

        BaseModel EditStation(EditStationInputModel model);

        DeleteStationViewModel GetDeleteStationModel(int? id);
    }
}
