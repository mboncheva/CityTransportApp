namespace CityTransport.Services.Main.Interfaces
{
    using CityTransport.Common.Models.Main.ViewModels.Line;

    public interface ILinesService
    {
        AllLinesViewModel AllLines();
    }
}
