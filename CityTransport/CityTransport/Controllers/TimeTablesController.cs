namespace CityTransport.Web.Controllers
{
    using CityTransport.Models.Enums;
    using CityTransport.Services.Main.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class TimeTablesController : Controller
    {
        private readonly ITimeTablesService TimeTablesService;

        public TimeTablesController(ITimeTablesService timeTablesService)
        {
            this.TimeTablesService = timeTablesService;
        }

        public IActionResult Lines()
        {
            var allLines = this.TimeTablesService.AllLines();
            return this.View(allLines);
        }

        public IActionResult TimeTableGo(int? id)
        {
            var model = this.TimeTablesService.TimeTable(id, Direction.Go);

            return this.View("TimeTable", model);
        }

        public IActionResult TimeTableReturn(int? id)
        {
            var model = this.TimeTablesService.TimeTable(id, Direction.Return);

            return this.View("TimeTable", model);
        }

    }
}