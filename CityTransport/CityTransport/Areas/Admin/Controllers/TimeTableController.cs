namespace CityTransport.Web.Areas.Admin.Controllers
{
    using CityTransport.Common.Constants;
    using CityTransport.Common.Models.Admin.InputModels.TimeTable;
    using CityTransport.Common.Models.Admin.ViewModels.TimeTable;
    using CityTransport.Models.Enums;
    using CityTransport.Services.Admin.Interfaces;
    using CityTransport.Web.Common.Extensions;
    using CityTransport.Web.Common.Helpers.Messages;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TimeTableController : AdminController
    {
        private readonly ITimeTableService TimeTableService;

        public TimeTableController(ITimeTableService timeTableService)
        {
            this.TimeTableService = timeTableService;
        }

        public IActionResult Index(int? id)
        {
            List<SelectListItem> directionTypes, dayTypes;
            GetCardTypesAndValidityCards(out directionTypes, out dayTypes);

            var model = this.TimeTableService.GetModelWIthoutRoutes(id);
            if (model == null)
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel
                {
                    Type = MessageType.Danger,
                    Message = MessageConstants.InvalidLineId
                });

                return this.RedirectToAction(nameof(LineController.Index), "Line");

            }
            if (!model.HasRoutes)
            {
                model.CreateRoute.DirectionTypes = directionTypes;
                model.CreateRoute.DayTypes = dayTypes;

                return this.View(model);
            }

            var modelWithRoutes = new TimeTableIndexWithRoutesViewModel
            {
                DirectionTypes = directionTypes,
                DayTypes = dayTypes,
                LineId = id.Value,
                Tab="0"
            };

            return this.View("IndexWithRoutes", modelWithRoutes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FilterRoutes
            ([Bind(nameof(TimeTableIndexWithRoutesViewModel.Direction),
                  nameof(TimeTableIndexWithRoutesViewModel.Day),
                  nameof(TimeTableIndexWithRoutesViewModel.LineId))]
                  string direction, string day, int? lineId)
        {

            if (!Enum.TryParse(direction, out Direction directionType))
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel
                {
                    Type = MessageType.Danger,
                    Message = MessageConstants.InvalidDirectionType
                });
                return this.RedirectToAction(nameof(Index), new { id = lineId });
            }
            if (!Enum.TryParse(day, out DayType dayType))
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel
                {
                    Type = MessageType.Danger,
                    Message = MessageConstants.InvalidDayType
                });
                return this.RedirectToAction(nameof(Index), new { id = lineId });
            }

            var model = this.TimeTableService.GetModelWithRoutes(directionType, dayType, lineId);

            if (model == null)
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel
                {
                    Type = MessageType.Danger,
                    Message = MessageConstants.InvalidLineId
                });

                return this.RedirectToAction(nameof(LineController.Index), "Line");
            }

            List<SelectListItem> directionTypes, dayTypes;
            GetCardTypesAndValidityCards(out directionTypes, out dayTypes);

            model.DirectionTypes = directionTypes;
            model.DayTypes = dayTypes;

            return this.View("IndexWithRoutes", model);
        }

        public IActionResult EditRoute(int? id,int? lineId)
        {
            var model = this.TimeTableService.GetModelWithEditRoute(id);
            if (model == null)
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel
                {
                    Type = MessageType.Danger,
                    Message = MessageConstants.InvalidRouteId
                });
                return this.RedirectToAction(nameof(Index), new { id = lineId });
            }

            List<SelectListItem> directionTypes, dayTypes;
            GetCardTypesAndValidityCards(out directionTypes, out dayTypes);

            model.DirectionTypes = directionTypes;
            model.DayTypes = dayTypes;

            return this.View("IndexWithRoutes", model);
        }

        public IActionResult DetailsRoute(int? id, int? lineId)
        {
            var model = this.TimeTableService.GetModelWithRouteStops(id);

            if (model == null)
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel
                {
                    Type = MessageType.Danger,
                    Message = MessageConstants.InvalidRouteId
                });
              return this.RedirectToAction(nameof(Index), new { id = lineId });
            }

            List<SelectListItem> directionTypes, dayTypes;
            GetCardTypesAndValidityCards(out directionTypes, out dayTypes);

            model.DirectionTypes = directionTypes;
            model.DayTypes = dayTypes;
            return this.View("IndexWithRoutes", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStop
            ([Bind(Prefix = nameof(TimeTableIndexWithRoutesViewModel.CreateStop))] CreateStopInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(DetailsRoute), new { id = model.RouteId });
            }

            var result = this.TimeTableService.CreateStop(model);

            if (!result.HasError)
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel
                {
                    Type = MessageType.Success,
                    Message = result.Message
                });
            }
            else
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel
                {
                    Type = MessageType.Danger,
                    Message = result.Message
                });

            }

            return this.RedirectToAction(nameof(DetailsRoute), new { id = model.RouteId });
        }

        //TODO: EDIT STOP
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStop()
        {
            return null;
        }
        private static void GetCardTypesAndValidityCards
            (out List<SelectListItem> directionTypes, out List<SelectListItem> dayTypes)
        {
            directionTypes = Enum.GetValues(typeof(Direction)).Cast<Direction>()
                           .Select(x => new SelectListItem
                           {
                               Text = Enum.GetName(typeof(Direction), x),
                               Value = x.ToString()
                           }).ToList();

            dayTypes = Enum.GetValues(typeof(DayType)).Cast<DayType>()
               .Select(x => new SelectListItem
               {
                   Text = Enum.GetName(typeof(DayType), x),
                   Value = x.ToString()
               }).ToList();

        }
    }
}