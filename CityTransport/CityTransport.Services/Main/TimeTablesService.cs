namespace CityTransport.Services.Main
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CityTransport.Common.Models.Main.ViewModels.Line;
    using CityTransport.Common.Models.Main.ViewModels.TimeTables;
    using CityTransport.Data;
    using CityTransport.Models;
    using CityTransport.Models.Enums;
    using CityTransport.Services.Main.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TimeTablesService : BaseService, ITimeTablesService
    {
        public TimeTablesService(CityTransprtDbContext Db)
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

        public TimeTableForLineViewModel TimeTable(int? lineId, Direction direction)
        {
            var lineRoutes = this.Db.Routes
                .Include(x => x.Line)
                .Where(x => x.LineId == lineId)
                .Include(x => x.TimeTables).ToList();

            if (!lineRoutes.Any())
            {
                return null;
            }

            var holidayModel = new List<RoutesViewModel>();
            var workDayModel = new List<RoutesViewModel>();
            if (direction == Direction.Go)
            {
                holidayModel = GetTimeTableGo(lineRoutes, direction, DayType.Holiday);
                workDayModel = GetTimeTableGo(lineRoutes, direction, DayType.WorkingDay);
            }
            else if (direction == Direction.Return)
            {
                holidayModel = GetTimeTableReturn(lineRoutes, direction, DayType.Holiday);
                workDayModel = GetTimeTableReturn(lineRoutes, direction, DayType.WorkingDay);
            }

            var directionModel = GetDirectionModel(lineId, lineRoutes);

            var lineName = lineRoutes.FirstOrDefault().Line.LineName;

            var model = new TimeTableForLineViewModel
            {
                LineId = lineId.Value,
                LineName = lineName,
                RoutesHoliday = holidayModel,
                RoutesWorkDay = workDayModel,
                DirectionModel = directionModel
            };

            return model;
        }

       

        private List<RoutesViewModel> GetTimeTableGo(List<Route> allRoutes, Direction direction, DayType dayType)
        {
            List<Route> routes;
            List<int> orderRouteIds, stationsIdsOnTimeTablesMax;
            SortedRoutesTimeTables(allRoutes, direction, dayType, out routes, out orderRouteIds, out stationsIdsOnTimeTablesMax);

            var allStations = this.Db.Stations.ToList();
            allStations = allStations.Where(x => stationsIdsOnTimeTablesMax.Contains(x.Id)).ToList();

            // set timeatables in model
            var routesModel = allStations.Select(x => new RoutesViewModel
            {
                StationName = x.StationName
            }).ToList();

            SetRoutesOnViewModel(routes, orderRouteIds, allStations, routesModel);

            return routesModel;
        }

        private List<RoutesViewModel> GetTimeTableReturn(List<Route> allRoutes, Direction direction, DayType dayType)
        {
            List<Route> routes;
            List<int> orderRouteIds, stationsIdsOnTimeTablesMax;
            SortedRoutesTimeTables(allRoutes, direction, dayType, out routes, out orderRouteIds, out stationsIdsOnTimeTablesMax);

            var allStations = this.Db.Stations.ToList();
            allStations = allStations.Where(x => stationsIdsOnTimeTablesMax.Contains(x.Id))
                .OrderByDescending(x=>x.Id).ToList();

            // set timeatables in model
            var routesModel = allStations.Select(x => new RoutesViewModel
            {
                StationName = x.StationName
            }).ToList();

            SetRoutesOnViewModel(routes, orderRouteIds, allStations, routesModel);

            return routesModel;
        }

        private static void SortedRoutesTimeTables(List<Route> allRoutes, Direction direction, DayType dayType, out List<Route> routes, out List<int> orderRouteIds, out List<int> stationsIdsOnTimeTablesMax)
        {
            // routes on dayType with Direction
            routes = allRoutes
                .Where(x => x.Direction == direction && x.DayType == dayType)
                .ToList();

            // order TimeTables on every Route
            foreach (var route in routes)
            {
                var timeTables = route.TimeTables.OrderBy(x => x.DepartureTime).ToList();
                routes.FirstOrDefault(x => x.Id == route.Id).TimeTables = timeTables;
            }

            // order routes and get their Ids
            var orderRoutes = new Dictionary<int, TimeSpan>();
            foreach (var route in routes)
            {
                var timetable = route.TimeTables.FirstOrDefault();
                if (timetable != null)
                {
                    orderRoutes.Add(route.Id, timetable.DepartureTime);
                }
            }
            orderRouteIds = orderRoutes.OrderBy(x => x.Value).Select(x => x.Key).ToList();

            //get Stations names
            var maxCount = routes.Select(x => x.TimeTables.Count()).Max();
            var maxTimeTables = routes.FirstOrDefault(x => x.TimeTables.Count() == maxCount).TimeTables.ToList();
            stationsIdsOnTimeTablesMax = maxTimeTables.Select(x => x.StationId).ToList();
        }

        private static void SetRoutesOnViewModel(List<Route> routes, List<int> orderRouteIds, List<Station> allStations, List<RoutesViewModel> routesModel)
        {
            foreach (var routeId in orderRouteIds)
            {
                var timeTables = routes.FirstOrDefault(x => x.Id == routeId).TimeTables.ToList();

                foreach (var route in routesModel)
                {
                    var departuresTimes = route.DepartureTimes.ToList();
                    var stationId = allStations.FirstOrDefault(x => x.StationName == route.StationName).Id;

                    var timeatable = timeTables.FirstOrDefault(x => x.StationId == stationId);
                    if (timeatable == null)
                    {
                        departuresTimes.Add(default(TimeSpan));
                    }
                    else
                    {
                        departuresTimes.Add(timeatable.DepartureTime);
                    }

                    routesModel.FirstOrDefault(x => x.StationName == route.StationName).DepartureTimes = departuresTimes;

                }
            }
        }

        private DirectionViewModel GetDirectionModel(int? lineId, List<Route> lineRoutes)
        {
            var fromGo = string.Empty;
            var toGo = string.Empty;
            var fromReturn = string.Empty;
            var toReturn = string.Empty;

            var routeWithMaxTimeTables = lineRoutes.OrderByDescending(x => x.TimeTables.Count).FirstOrDefault();
            var sortedTimeTables = routeWithMaxTimeTables.TimeTables.OrderBy(x => x.DepartureTime).ToList();
            var stations = this.Db.LineStations.Where(x => x.LineId == lineId)
                .Select(x => x.Station).ToList();

            if (routeWithMaxTimeTables.Direction == Direction.Go)
            {
                fromGo = sortedTimeTables.First().Station.StationName;
                toGo = sortedTimeTables.Last().Station.StationName;

                fromReturn = sortedTimeTables.Last().Station.StationName;
                toReturn = sortedTimeTables.First().Station.StationName;

            }
            else if (routeWithMaxTimeTables.Direction == Direction.Return)
            {
                fromReturn = sortedTimeTables.First().Station.StationName;
                toReturn = sortedTimeTables.Last().Station.StationName;

                fromGo = sortedTimeTables.Last().Station.StationName;
                toGo = sortedTimeTables.First().Station.StationName;
            }

            var directionModel = new DirectionViewModel
            {
                FromDirectionGo = fromGo,
                ToDirectionGo = toGo,
                FromDirectionReturn = fromReturn,
                ToDirectionReturn = toReturn
            };

            return directionModel;
        }
    }

}
