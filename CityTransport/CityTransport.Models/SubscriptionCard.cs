namespace CityTransport.Models
{
    using CityTransport.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class SubscriptionCard
    {
        public int Id { get; set; }

        public TypeCard TypeCard { get; set; }

        public ValidityCard ValidityCard { get; set; }

        [Required]
        public int CountTrips { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
