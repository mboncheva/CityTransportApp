namespace CityTransport.Common.Models.Admin.ViewModels.SubscriptionCard
{
    using CityTransport.Models.Enums;

    public class SubscriptionCardsViewModel
    {
        public int Id { get; set; }

        public TypeCard TypeCard { get; set; }

        public ValidityCard ValidityCard { get; set; }

        public int CountTrips { get; set; }

        public decimal Price { get; set; }

        public decimal TicketPrice { get; set; }
    }
}
