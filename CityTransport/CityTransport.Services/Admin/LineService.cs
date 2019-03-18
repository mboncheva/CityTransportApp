namespace CityTransport.Services.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CityTransport.Common.Constants;
    using CityTransport.Common.Models;
    using CityTransport.Common.Models.Admin.InputModels.Line;
    using CityTransport.Common.Models.Admin.ViewModels.Line;
    using CityTransport.Data;
    using CityTransport.Models;
    using CityTransport.Models.Enums;
    using CityTransport.Services.Admin.Interfaces;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class LineService : BaseService, ILineService
    {
        private readonly BaseModel BaseModel;

        public LineService(CityTransprtDbContext Db)
            :base(Db)
        {
            this.BaseModel = new BaseModel();
        }

        public ICollection<LineViewModel> Lines()
        {
            var lines = this.Db.Lines.Select(x => new LineViewModel
            {
                Id = x.Id,
                LineName = x.LineName,
                Type = x.Type
            }).ToList();

            return lines;
        }

        public BaseModel CreateLine(CreateLineInputModel model)
        {
            var hasLineName = this.Db.Lines.Any(x => x.LineName == model.LineName);
            if (hasLineName)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = string.Format(MessageConstants.HaveLineName, model.LineName);
                return this.BaseModel;
            }

            if (!Enum.TryParse(model.TypeTransport, out TypeTransport typeTransport))
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidTypeTransport;
                return this.BaseModel;
            }

            var line = new Line
            {
                LineName = model.LineName,
                Type = typeTransport
            };

            try
            {
                this.Db.Lines.Add(line);
                this.Db.SaveChanges();

                this.BaseModel.HasError = false;
                this.BaseModel.Message = MessageConstants.CreateLine;
            }
            catch (Exception)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.NoCreateLine;
            }

            return this.BaseModel;
        }

        public EditLineInputModel GetEditLineModel(int? id)
        {
            var line = this.Db.Lines
                           .Where(x => x.Id == id)
                           .Include(x => x.Stations)
                           .Include(x => x.Routes)
                           .FirstOrDefault();

            if (line == null)
            {
                return null;
            }

            var transportTypes = Enum.GetValues(typeof(TypeTransport)).Cast<TypeTransport>()
              .Select(x => new SelectListItem
              {
                  Text = Enum.GetName(typeof(TypeTransport), x),
                  Value = x.ToString()
              }).ToList();

            var lineEdit = new EditLineInputModel
            {
                Id = line.Id,
                LineName = line.LineName,
                TypeTransport = line.Type.ToString(),
                TransportTypes = transportTypes
            };

            return lineEdit;
        }

        public BaseModel EditLine(EditLineInputModel model)
        {
            var line = this.Db.Lines.FirstOrDefault(x => x.Id == model.Id);
            if (line == null)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidLineId;
                return this.BaseModel;
            }

            if (!Enum.TryParse(model.TypeTransport, out TypeTransport typeTransport))
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidTypeTransport;
                return this.BaseModel;
            }

            var hasLineName = this.Db.Lines.Any(x => x.LineName == model.LineName && x.Id != model.Id);
            if (hasLineName)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = string.Format(MessageConstants.HaveLineName, model.LineName);
                return this.BaseModel;
            }

            line.LineName = model.LineName;
            line.Type = typeTransport;

            try
            {
                this.Db.Lines.Update(line);
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




        public DetailsLineViewModel Details(int? id)
        {
            var line = this.Db.Lines
                .Where(x => x.Id == id)
                .Include(x => x.Stations)
                .Include(x => x.Routes)
                .FirstOrDefault();

            if (line == null)
            {
                return null;
            }

            var addStationModel = AddStationsToLine(line);

            var model = new DetailsLineViewModel
            {
                AddStationToLine = addStationModel
            };

            return model;
        }

        private AddStationToLineInputModel AddStationsToLine(Line line)
        {
            var addStationModel = new AddStationToLineInputModel();

            var allStations = this.Db.Stations.ToList();

            var staionsItems = new List<SelectListItem>();
            if (!line.Stations.Any())
            {
                staionsItems = allStations.Select(x => new SelectListItem
                {
                    Text = x.StationName,
                    Value = x.StationName
                }).ToList();

                addStationModel.Stations = staionsItems;
            }
            else
            {
                var lineStations = line.Stations.Select(x => new SelectListItem
                {
                    Text = x.Station.StationName,
                    Value = x.Station.StationName
                }).ToList();


                foreach (var item in line.Stations)
                {
                    staionsItems = allStations.Where(x => x.Id != item.StationId)
                         .Select(x => new SelectListItem
                         {
                             Text = x.StationName,
                             Value = x.StationName
                         }).ToList();
                }

                addStationModel.LineStations = lineStations;
                addStationModel.Stations = staionsItems;
            }

            return addStationModel;
        }

       
    }
}
