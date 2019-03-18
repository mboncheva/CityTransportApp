namespace CityTransport.Common.Models.Admin.ViewModels.Line
{
    using CityTransport.Models.Enums;

    public class LineViewModel
    {
        public int Id { get; set; }

        public string LineName { get; set; }

        public TypeTransport Type { get; set; }
    }
}
