namespace CityTransport.Web.Areas.Admin.Controllers
{
    using CityTransport.Common.Constants;
    using CityTransport.Common.Models.Admin.InputModels.Route;
    using CityTransport.Common.Models.Admin.ViewModels.TimeTable;
    using CityTransport.Services.Admin.Interfaces;
    using CityTransport.Web.Common.Extensions;
    using CityTransport.Web.Common.Helpers.Messages;
    using Microsoft.AspNetCore.Mvc;

    public class RouteController : AdminController
    {
        private readonly IRouteService RouteService;

        public RouteController(IRouteService routeService)
        {
            this.RouteService = routeService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create
            ([Bind(Prefix = nameof(TimeTableIndexViewModel.CreateRoute))] CreateRouteInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(TimeTableController.Index), "TimeTable", new { id = model.LineId });
            }

            var result = this.RouteService.CreateRoute(model);
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

            return RedirectToAction(nameof(TimeTableController.Index), "TimeTable", new { id = model.LineId });
        }

        //TODO: EDIT ROUTE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit
            ([Bind(Prefix = nameof(TimeTableIndexWithRoutesViewModel.EditRoute))] EditRouteInputModel model)
        {
            if (ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(TimeTableController.EditRoute),"TimeTable", new { id = model.Id });
            }

            return this.RedirectToAction(nameof(TimeTableController.EditRoute), "TimeTable", new { id = model.Id });

        }
    }
}