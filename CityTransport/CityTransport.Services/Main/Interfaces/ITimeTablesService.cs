namespace CityTransport.Services.Main.Interfaces
{
    using CityTransport.Common.Models.Main.ViewModels.Line;
    using CityTransport.Common.Models.Main.ViewModels.TimeTables;
    using CityTransport.Models.Enums;

    public interface ITimeTablesService
    {
        AllLinesViewModel AllLines();

        TimeTableForLineViewModel TimeTable(int? id, Direction direction);

    }
}
