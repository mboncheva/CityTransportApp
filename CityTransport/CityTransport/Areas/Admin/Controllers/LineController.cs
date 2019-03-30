namespace CityTransport.Web.Areas.Admin.Controllers
{
    using CityTransport.Common.Constants;
    using CityTransport.Common.Models.Admin.InputModels.Line;
    using CityTransport.Common.Models.Admin.ViewModels.Line;
    using CityTransport.Models.Enums;
    using CityTransport.Services.Admin.Interfaces;
    using CityTransport.Web.Common.Extensions;
    using CityTransport.Web.Common.Helpers.Messages;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Linq;

    public class LineController : AdminController
    {
        private readonly ILineService LineService;

        public LineController(ILineService lineService)
        {
            this.LineService = lineService;
        }

        public IActionResult Index()
        {
            var model = this.LineService.Lines().ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            var transportTypes = Enum.GetValues(typeof(TypeTransport)).Cast<TypeTransport>()
                          .Select(x => new SelectListItem
                          {
                              Text = Enum.GetName(typeof(TypeTransport), x),
                              Value = x.ToString()
                          }).ToList();

            var model = new CreateLineInputModel
            {
                TransportTypes = transportTypes
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateLineInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create));
            }

            var result = this.LineService.CreateLine(model);

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
            var model = this.LineService.GetEditLineModel(id);
            if (model == null)
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel
                {
                    Type = MessageType.Danger,
                    Message = MessageConstants.InvalidLineId
                });

                return this.RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditLineInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = this.LineService.EditLine(model);

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

                return this.RedirectToAction(nameof(Edit), new { id = model.Id });
            }

            return this.RedirectToAction(nameof(Index));
        }

    }
}