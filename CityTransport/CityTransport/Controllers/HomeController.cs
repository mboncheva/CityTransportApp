namespace CityTransport.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using CityTransport.Models;
    using CityTransport.Web.Controllers;
    using CityTransport.Common.Constants;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using CityTransport.Web.Common.Extensions;
    using CityTransport.Web.Common.Helpers.Messages;
    using CityTransport.Common.Models.Admin.InputModels.User;
    using CityTransport.Services.Admin.Interfaces;

    public class HomeController : BaseController
    {
        public readonly IUserService UserService;

        public HomeController(IUserService userService)
        {
            this.UserService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("login")]
        public IActionResult LoginUser()
        {
            HttpContext.SignOutAsync(IdentityConstants.ExternalScheme).GetAwaiter().GetResult();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnLogin(LoginInputModel model, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await this.UserService.LoginUser(model);
                if (!result.HasError)
                {
                    //  _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    this.TempData.Put(MessageConstants.Name, new MessageModel
                    {
                        Type = MessageType.Danger,
                        Message = "Invalid login attempt."
                    });

                    return this.RedirectToAction(nameof(LoginUser));
                }
            }
            // If we got this far, something failed, redisplay form
            return this.RedirectToAction(nameof(LoginUser));
        }

    }
}
