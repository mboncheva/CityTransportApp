namespace CityTransport.Common.Models.Main.ViewModels.Line
{
    using System.Collections.Generic;

    public class AllLinesViewModel
    {
        public AllLinesViewModel()
        {
            this.BusLines = new HashSet<LineViewModel>();
            this.TramLines = new HashSet<LineViewModel>();
            this.TrolleiLines = new HashSet<LineViewModel>();
        }

        public IEnumerable<LineViewModel> BusLines { get; set; }

        public IEnumerable<LineViewModel> TramLines { get; set; }

        public IEnumerable<LineViewModel> TrolleiLines { get; set; }

    }
}
