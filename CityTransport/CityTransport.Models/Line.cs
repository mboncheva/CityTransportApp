namespace CityTransport.Models
{
    using CityTransport.Models.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Line
    {
        public Line()
        {
            this.Users = new HashSet<LineUser>();
            this.Stations = new HashSet<LineStation>();
            this.Routes = new HashSet<Route>();
        }

        public int Id { get; set; }

        [Required]
        public string LineName { get; set; }

        public TypeTransport Type { get; set; }

        public virtual ICollection<LineStation> Stations { get; set; }

        public virtual ICollection<LineUser> Users { get; set; }

        public virtual ICollection<Route> Routes { get; set; }
    }
}
