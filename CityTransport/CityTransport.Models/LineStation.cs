namespace CityTransport.Models
{
    public class LineStation
    {
        public int LineId { get; set; }

        public virtual Line Line { get; set; }

        public int StationId { get; set; }

        public virtual Station Station { get; set; }
    }
}
