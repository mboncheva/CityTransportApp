namespace CityTransport.Services.Admin.Interfaces
{
    using CityTransport.Common.Models;
    using CityTransport.Common.Models.Admin.InputModels.TimeTable;
    using CityTransport.Common.Models.Admin.ViewModels.TimeTable;
    using CityTransport.Models.Enums;

    public interface ITimeTableService
    {
        TimeTableIndexViewModel GetModelWIthoutRoutes(int? id);

        TimeTableIndexWithRoutesViewModel GetModelWithRoutes(Direction direction, DayType day, int? lineId);

        TimeTableIndexWithRoutesViewModel GetModelWithEditRoute(int? id);

        TimeTableIndexWithRoutesViewModel GetModelWithRouteStops(int? id);

        BaseModel CreateStop(CreateStopInputModel model);
    }
}
