namespace CityTransport.Web.Areas.Admin.Controllers
{
    using CityTransport.Common.Constants;
    using CityTransport.Common.Models.Admin.InputModels.CustomerCard;
    using CityTransport.Common.Models.Admin.ViewModels.User;
    using CityTransport.Services.Admin.Interfaces;
    using CityTransport.Web.Common.Extensions;
    using CityTransport.Web.Common.Helpers.Messages;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CustomerCardController : AdminController
    {
        private readonly ICustomerCardService CustomerCardService;

        public CustomerCardController(ICustomerCardService customerCardService)
        {
            this.CustomerCardService = customerCardService;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create
            ([Bind(Prefix = nameof(UserDetailsViewModel.CreateCustomerCard))] CreateCustomerCardInputModel model, string id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(UserController.Details), "User", new { id = id, tab = "3"});
            }

            var result = await this.CustomerCardService.CreateCustomerCard(model, id);

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

            return RedirectToAction(nameof(UserController.Details), "User", new { id = id, tab = "3"});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit
            ([Bind(Prefix = nameof(UserDetailsViewModel.EditCustomerCard))] EditCustomerCardInputModel model, string id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(UserController.Details), "User", new { id = id, tab = "3" });
            }

            var result = await this.CustomerCardService.EditCustomerCard(model, id);

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

            return RedirectToAction(nameof(UserController.Details), "User", new { id = id, tab = "3" });
        }
    }
}