namespace CityTransport.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Station
    {
        public Station()
        {
            this.Lines = new HashSet<LineStation>();
            this.TimeTables = new HashSet<TimeTable>();
        }

        public int Id { get; set; }

        [Required]
        public string StationName { get; set; }

        [Required]
        public string StationCode { get; set; }

        public virtual ICollection<LineStation> Lines { get; set; }

        public virtual ICollection<TimeTable> TimeTables { get; set; }
    }
}
