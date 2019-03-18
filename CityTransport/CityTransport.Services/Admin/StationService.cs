namespace CityTransport.Services.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CityTransport.Common.Constants;
    using CityTransport.Common.Models;
    using CityTransport.Common.Models.Admin.InputModels.Station;
    using CityTransport.Common.Models.Admin.ViewModels.Station;
    using CityTransport.Data;
    using CityTransport.Models;
    using CityTransport.Services.Admin.Interfaces;

    public class StationService : BaseService, IStationService
    {
        private readonly BaseModel BaseModel;

        public StationService(CityTransprtDbContext Db)
            :base(Db)
        {
            this.BaseModel = new BaseModel();
        }

        public ICollection<StationViewModel> Stations()
        {
            var stations = this.Db.Stations.Select(x => new StationViewModel
            {
                Id = x.Id,
                StationCode = x.StationCode,
                StationName = x.StationName
            }).ToList();

            return stations;
        }

        public BaseModel CreateStation(CreateStationInputModel model)
        {
            var hasStationName = this.Db.Stations.Any(x => x.StationName == model.StationName);
            var hasStationCode = this.Db.Stations.Any(x => x.StationCode == model.StationCode);

            if (hasStationName)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = string.Format(MessageConstants.HaveStationName, model.StationName);
                return this.BaseModel;
            }
            if (hasStationCode)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = string.Format(MessageConstants.HaveStationCode, model.StationCode);
                return this.BaseModel;
            }

            var station = new Station
            {
                StationName = model.StationName,
                StationCode = model.StationCode,
            };

            try
            {
                this.Db.Stations.Add(station);
                this.Db.SaveChanges();

                this.BaseModel.HasError = false;
                this.BaseModel.Message = MessageConstants.CreateStation;
            }
            catch (Exception)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.NoCreateStation;
            }

            return this.BaseModel;
        }

        public EditStationInputModel GetEditStationModel(int? id)
        {
            var station = this.Db.Stations.Where(x => x.Id == id)
                .Select(x => new EditStationInputModel
                {
                    Id = x.Id,
                    StationCode = x.StationCode,
                    StationName = x.StationName
                }).FirstOrDefault();

            if (station == null)
            {
                return null;
            }

            return station;
        }

        public BaseModel EditStation(EditStationInputModel model)
        {
            var station = this.Db.Stations.FirstOrDefault(x => x.Id == model.Id);

            if (station == null)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidStationId;
                return this.BaseModel;
            }

            var hasStationName = this.Db.Stations.Any(x => x.StationName == model.StationName && x.Id != model.Id);
            var hasStationCode = this.Db.Stations.Any(x => x.StationCode == model.StationCode && x.Id != model.Id);

            if (hasStationName)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = string.Format(MessageConstants.HaveStationName, model.StationName);
                return this.BaseModel;
            }
            if (hasStationCode)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = string.Format(MessageConstants.HaveStationCode, model.StationCode);
                return this.BaseModel;
            }
            station.StationName = model.StationName;
            station.StationCode = model.StationCode;

            try
            {
                this.Db.Stations.Update(station);
                this.Db.SaveChanges();

                this.BaseModel.HasError = false;
                this.BaseModel.Message = MessageConstants.EditStation;
            }
            catch (Exception)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.NoEditStation;
            }

            return this.BaseModel;
        }

        public DeleteStationViewModel GetDeleteStationModel(int? id)
        {
            var model = this.Db.Stations.Where(x => x.Id == id)
                .Select(x => new DeleteStationViewModel
                {
                    Id = x.Id,
                    StationName = x.StationName
                }).FirstOrDefault();

            if (model == null)
            {
                return null;
            }

            return model;
        }
    }
}
