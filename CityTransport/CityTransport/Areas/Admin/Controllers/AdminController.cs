namespace CityTransport.Web.Areas.Admin.Controllers
{
    using CityTransport.Common.Constants;
    using CityTransport.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(SeedDataConstants.AdminArea)]
    [Authorize(Roles = SeedDataConstants.AdminRole)]
    public class AdminController : BaseController
    {
    }
}