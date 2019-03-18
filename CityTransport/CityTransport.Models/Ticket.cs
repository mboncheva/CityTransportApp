namespace CityTransport.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Ticket
    {
        public Ticket()
        {
            this.CustomerCards = new HashSet<CustomerCard>();
            this.SubscriptionCards = new HashSet<SubscriptionCard>();

        }

        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual ICollection<CustomerCard> CustomerCards { get; set; }

        public virtual ICollection<SubscriptionCard> SubscriptionCards { get; set; }

    }
}
