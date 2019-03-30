namespace CityTransport.Services.Main
{
    using System.Linq;
    using CityTransport.Common.Models.Main.ViewModels.Line;
    using CityTransport.Data;
    using CityTransport.Models.Enums;
    using CityTransport.Services.Main.Interfaces;

    public class LinesService : BaseService, ILinesService
    {
        public LinesService(CityTransprtDbContext Db)
            :base(Db)
        {
        }

        public AllLinesViewModel AllLines()
        {
            var lines = this.Db.Lines.ToList();

            var busLines = lines
                .Where(x => x.Type == TypeTransport.Bus)
                .Select(x => new LineViewModel
                {
                    Id = x.Id,
                    Name = x.LineName
                }).ToList();

            var tramLines= lines
                .Where(x => x.Type == TypeTransport.Tram)
                .Select(x => new LineViewModel
                {
                    Id = x.Id,
                    Name = x.LineName
                }).ToList();

            var trolleiLines = lines
               .Where(x => x.Type == TypeTransport.Trollei)
               .Select(x => new LineViewModel
               {
                   Id = x.Id,
                   Name = x.LineName
               }).ToList();

            var model = new AllLinesViewModel
            {
                BusLines = busLines,
                TramLines = tramLines,
                TrolleiLines = trolleiLines
            };

            return model;
        }
    }
}
