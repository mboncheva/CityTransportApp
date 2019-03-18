namespace CityTransport.Web.Areas.Admin.Controllers
{
    using CityTransport.Common.Constants;
    using CityTransport.Common.Models.Admin.InputModels.CustomerCard;
    using CityTransport.Common.Models.Admin.InputModels.User;
    using CityTransport.Common.Models.Admin.ViewModels.User;
    using CityTransport.Services.Admin.Interfaces;
    using CityTransport.Web.Common.Extensions;
    using CityTransport.Web.Common.Helpers.Messages;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class UserController : AdminController
    {
        public readonly IUserService UserService;

        public UserController(IUserService userService)
        {
            this.UserService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.UserService.UsersAsync();
            return this.View(users);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = await this.UserService.CreateUserAsync(model);

            if (result != null)
            {
                return RedirectToAction(nameof(Details), new { id = result });
            }
            else
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel
                {
                    Type = MessageType.Danger,
                    Message = "The User has not been successfully created!"
                });

                return View(model);
            }

        }

        public async Task<IActionResult> Details(string id, string tab)
        {
            var model = await this.UserService.Details(id,tab);

            if (model == null)
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel
                {
                    Type = MessageType.Danger,
                    Message = "No User!"
                });
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind(Prefix = nameof(UserDetailsViewModel.UserEdit))] EditUserInputModel model, string id)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(Details), new { id = id});
            }

            var result = await this.UserService.EditUser(id, model);

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

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword
            ([Bind(Prefix = nameof(UserDetailsViewModel.UserChangePassword))] ChangePasswordInputModel model, string id)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(Details), new { id = id, tab = "1"});

            }
            var result = await this.UserService.ChangePasswordAsync(id, model);
            if (!result.HasError)
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel()
                {
                    Type = MessageType.Success,
                    Message = result.Message
                });

            }
            else
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = result.Message
                });
            }

            return RedirectToAction(nameof(Details), new { id = id });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToRole(string id, string role)
        {
            var result = await this.UserService.AddToRoleAsync(id, role);

            if (!result.HasError)
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel()
                {
                    Type = MessageType.Success,
                    Message = result.Message
                });
            }
            else
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = result.Message
                });
            }

            return this.RedirectToAction(nameof(Details), new { id= id, tab = "2"});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromRole(string id, string role)
        {
            var result = await this.UserService.RemoveFromRoleAsync(id, role);

            if (!result.HasError)
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel()
                {
                    Type = MessageType.Success,
                    Message = result.Message
                });
            }
            else
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = result.Message
                });
            }

            return this.RedirectToAction(nameof(Details), new { id = id, tab = "2" });
        }

    }
}