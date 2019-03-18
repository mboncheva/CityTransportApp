namespace CityTransport.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TimeTable
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan DepartureTime { get; set; }

        public int StationId { get; set; }
        public virtual Station Station { get; set; }

        public int RouteId { get; set; }
        public virtual Route Route { get; set; }
    }
}
