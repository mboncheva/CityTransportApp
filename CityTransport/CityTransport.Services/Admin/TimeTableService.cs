namespace CityTransport.Services.Admin
{
    using CityTransport.Common.Constants;
    using CityTransport.Common.Models;
    using CityTransport.Common.Models.Admin.InputModels.Route;
    using CityTransport.Common.Models.Admin.InputModels.TimeTable;
    using CityTransport.Common.Models.Admin.ViewModels.Route;
    using CityTransport.Common.Models.Admin.ViewModels.TimeTable;
    using CityTransport.Data;
    using CityTransport.Models;
    using CityTransport.Models.Enums;
    using CityTransport.Services.Admin.Interfaces;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    public class TimeTableService : BaseService, ITimeTableService
    {
        private readonly BaseModel BaseModel;

        public TimeTableService(CityTransprtDbContext Db)
            :base(Db)
        {
            this.BaseModel = new BaseModel();
        }

        public TimeTableIndexViewModel GetModelWIthoutRoutes(int? id)
        {
            var line = this.Db.Lines
                .Where(x => x.Id == id)
                .Include(x => x.Routes)
                .FirstOrDefault();

            if (line == null)
            {
                return null;
            }

            var createRouteModel = new CreateRouteInputModel
            {
                LineId = id.Value
            };

            var model = new TimeTableIndexViewModel
            {
                CreateRoute = createRouteModel
            };

            if (!line.Routes.Any())
            {
                model.HasRoutes = default(bool);
                model.Tab = "1";

            }
            else
            {
                model.HasRoutes = true;
            }

            return model;
        }

        public TimeTableIndexWithRoutesViewModel GetModelWithRoutes(Direction direction, DayType day, int? lineId)
        {
            var line = this.Db.Lines
                .Where(x => x.Id == lineId)
                .Include(x => x.Routes)
                .FirstOrDefault();

            if (line == null)
            {
                return null;
            }
            
            var model = new TimeTableIndexWithRoutesViewModel
            {
                Day = day.ToString(),
                Direction = direction.ToString(),
                LineId = line.Id,
                Tab = "0"
            };
            var routes = line.Routes
                .Where(x => x.Direction == direction && x.DayType == day)
                .Select(x => new RouteViewModel
                {
                    Id = x.Id,
                    RouteName = x.RouteName
                }).ToList();

            model.Routes = routes;
            return model;
        }

        public TimeTableIndexWithRoutesViewModel GetModelWithEditRoute(int? id)
        {
            var route = this.Db.Routes.Where(x => x.Id == id).Include(x => x.Line).FirstOrDefault();

            if (route == null)
            {
                return null;
            }

            var routemodel = new EditRouteInputModel
            {
                Id = route.Id,
                Day = route.DayType.ToString(),
                RouteName = route.RouteName
            };

            var model = new TimeTableIndexWithRoutesViewModel
            {
                EditRoute = routemodel,
                LineId = route.LineId,
                Tab = "1",
                Day = route.DayType.ToString(),
                Direction = route.Direction.ToString(),
            };

            return model;
        }

        public TimeTableIndexWithRoutesViewModel GetModelWithRouteStops(int? id)
        {
            var route = this.Db.Routes
                .Where(x => x.Id == id)
                .Include(x => x.Line)
                .Include(x => x.TimeTables)
                .FirstOrDefault();

            if (route == null)
            {
                return null;
            }

            var allStations = this.Db.Stations.ToList();
            var timeTables = route.TimeTables.Select(x => new TimeTableViewModel
            {
                Id = x.Id,
                StationName= allStations.FirstOrDefault(s => s.Id == x.StationId).StationName,
                DepartureTime = x.DepartureTime
            }).ToList();

            //TODO: GET ONLY STATIONS THAT LINE HAVE
            var stations = allStations
             .Select(x => new SelectListItem
             {
                 Text = x.StationName,
                 Value = x.StationName
             })
             .ToList();

            var createStopModel = new CreateStopInputModel
            {
                RouteId = route.Id,
                Stations= stations
            };

            var orderTimeTables = timeTables.OrderBy(x => x.DepartureTime).ToList();
            var model = new TimeTableIndexWithRoutesViewModel
            {
                LineId = route.LineId,
                Tab = "2",
                Day = route.DayType.ToString(),
                Direction = route.Direction.ToString(),
                TimeTables = orderTimeTables,
                CreateStop = createStopModel
            };

            return model;
        }

        public BaseModel CreateStop(CreateStopInputModel model)
        {
            var route = this.Db.Routes
                .Where(x => x.Id == model.RouteId)
                .Include(x => x.TimeTables)
                .Include(x=>x.Line).FirstOrDefault();

            if (route == null)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidRouteId;
                return this.BaseModel;
            }

            var stationId = this.Db.Stations.FirstOrDefault(x => x.StationName == model.StationName).Id;

            var hasStation = route.TimeTables.Any(x => x.StationId == stationId);
            var hasDepTime = route.TimeTables.Any(x => x.DepartureTime == model.DepartureTime);

            if (hasStation)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message =string.Format(MessageConstants.HaveStopName, model.StationName);
                return this.BaseModel;
            }

            if (hasDepTime)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.HaveStopHour;
                return this.BaseModel;
            }

            //create TT
            var stop = new TimeTable
            {
                StationId = stationId,
                DepartureTime = model.DepartureTime,
                RouteId = route.Id
            };

            try
            {
                this.Db.TimeTables.Add(stop);
                this.Db.SaveChanges();

                this.BaseModel.HasError = false;
                this.BaseModel.Message = MessageConstants.CreateStop;
            }
            catch (Exception)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.NoCreateStop;
                return this.BaseModel;
            }

            var lineId = route.LineId;
            var allStationsIdsForLineId = this.Db.LineStations
                .Where(x => x.LineId == lineId)
                .Select(x => x.StationId).ToList();

            foreach (var timeTable in route.TimeTables)
            {
                var containsStation = allStationsIdsForLineId.Contains(timeTable.StationId);
                if (!containsStation)
                {
                    var lineStation = new LineStation
                    {
                        LineId = lineId,
                        StationId = timeTable.StationId
                    };

                    this.Db.LineStations.Add(lineStation);
                    this.Db.SaveChanges();
                }
            }

            return this.BaseModel;
        }
    }
}
