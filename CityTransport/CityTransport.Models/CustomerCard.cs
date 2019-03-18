namespace CityTransport.Models
{
    using CityTransport.Models.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerCard
    {
        public int Id { get; set; }

        [Required]
        public string CustomerCardNumber { get; set; }

        public TypeCard Type { get; set; }

        [Required]
        public int CountTrips { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? ValidFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ValidTo { get; set; }

        [DataType(DataType.Date)]
        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}