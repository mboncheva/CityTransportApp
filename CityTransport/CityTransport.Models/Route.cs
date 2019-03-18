namespace CityTransport.Models
{
    using CityTransport.Models.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Route
    {
        public Route()
        {
            this.TimeTables = new HashSet<TimeTable>();
        }

        public int Id { get; set; }

        [Required]
        public string RouteName { get; set; }

        public Direction Direction { get; set; }

        public DayType DayType { get; set; }

        public int LineId { get; set; }
        public virtual Line Line { get; set; }

        public virtual ICollection<TimeTable> TimeTables { get; set; }
    }
}