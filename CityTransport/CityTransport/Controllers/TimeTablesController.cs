namespace CityTransport.Web.Controllers
{
    using CityTransport.Services.Main.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class TimeTablesController : Controller
    {
        private readonly ILinesService LineService;

        public TimeTablesController(ILinesService lineService)
        {
            this.LineService = lineService;
        }

        public IActionResult Lines()
        {
            var allLines = this.LineService.AllLines();
            return this.View(allLines);
        }

        public IActionResult TimeTable(int id)
        {
            return this.View();
        }
    }
}