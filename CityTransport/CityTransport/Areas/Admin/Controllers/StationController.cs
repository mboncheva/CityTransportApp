namespace CityTransport.Web.Areas.Admin.Controllers
{
    using CityTransport.Common.Constants;
    using CityTransport.Common.Models.Admin.InputModels.Station;
    using CityTransport.Services.Admin.Interfaces;
    using CityTransport.Web.Common.Extensions;
    using CityTransport.Web.Common.Helpers.Messages;
    using Microsoft.AspNetCore.Mvc;

    public class StationController : AdminController
    {
        private readonly IStationService StationService;
        public StationController(IStationService stationService)
        {
            this.StationService = stationService;
        }

        public IActionResult Index()
        {
            var model = this.StationService.Stations();

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateStationInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = this.StationService.CreateStation(model);
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

                return this.RedirectToAction(nameof(Create));
            }

            return this.RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            var model = this.StationService.GetEditStationModel(id);
            if (model == null)
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel
                {
                    Type = MessageType.Danger,
                    Message = MessageConstants.InvalidStationId
                });

                return this.RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditStationInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = this.StationService.EditStation(model);
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

                return this.RedirectToAction(nameof(Edit), new { id = model.Id});
            }

            return this.RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            var model = this.StationService.GetDeleteStationModel(id);
            if (model == null)
            {
            }

            return View(model);
        }

        public IActionResult DeleteConfirmed(int? id)
        {
            //TODO: DELETE STATION LOGIC
           
            return RedirectToAction(nameof(Index));
        }

    }
}