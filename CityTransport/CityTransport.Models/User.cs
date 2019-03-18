namespace CityTransport.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        public User()
        {
            this.Lines = new HashSet<LineUser>();
        }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegisteredOn { get; set; } = DateTime.UtcNow;

        public int? CustomerCardId { get; set; }
        public virtual CustomerCard CustomerCard { get; set; }

        public virtual ICollection<LineUser> Lines { get; set; }
    }
}
