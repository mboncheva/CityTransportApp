namespace CityTransport.Services.Admin
{
    using CityTransport.Common.Constants;
    using CityTransport.Common.Models;
    using CityTransport.Common.Models.Admin.InputModels.Route;
    using CityTransport.Data;
    using CityTransport.Models;
    using CityTransport.Models.Enums;
    using CityTransport.Services.Admin.Interfaces;
    using System;
    using System.Linq;

    public class RouteService : BaseService, IRouteService
    {
        private readonly BaseModel BaseModel;

        public RouteService(CityTransprtDbContext Db)
            :base(Db)
        {
            this.BaseModel = new BaseModel();
        }

        public BaseModel CreateRoute(CreateRouteInputModel model)
        {
            if (!Enum.TryParse(model.Direction, out Direction directionType))
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidDirectionType;
                return this.BaseModel;
            }

            if (!Enum.TryParse(model.Day, out DayType dayType))
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidDayType;
                return this.BaseModel;
            }

            var anyRouteName = this.Db.Routes
                .Any(x => x.LineId == model.LineId
                 && x.RouteName == model.RouteName
                 && x.Direction == directionType
                 && x.DayType == dayType);

            if (anyRouteName)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = string.Format(MessageConstants.HaveRouteName, model.RouteName);
                return this.BaseModel;
            }

            var route = new Route
            {
                RouteName = model.RouteName,
                Direction = directionType,
                DayType = dayType,
                LineId = model.LineId
            };

            try
            {
                this.Db.Routes.Add(route);
                this.Db.SaveChanges();

                this.BaseModel.HasError = false;
                this.BaseModel.Message = MessageConstants.CreateRoute;
            }
            catch (Exception)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.NoCreateRoute;
            }

            return this.BaseModel;
        }

    }
}
