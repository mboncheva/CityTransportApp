namespace CityTransport.Services.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CityTransport.Common.Constants;
    using CityTransport.Common.Models;
    using CityTransport.Common.Models.Admin.InputModels.SubscriptionCard;
    using CityTransport.Common.Models.Admin.ViewModels.SubscriptionCard;
    using CityTransport.Data;
    using CityTransport.Models;
    using CityTransport.Models.Enums;
    using CityTransport.Services.Admin.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class SubscriptionCardService : BaseService, ISubscriptionCardService
    {
        private readonly BaseModel BaseModel;

        public SubscriptionCardService(CityTransprtDbContext Db)
            :base(Db)
        {
            this.BaseModel = new BaseModel();
        }

        public ICollection<SubscriptionCardsViewModel> SubscriptionCards()
        {
            var model = this.Db.SubscriptionCards
                .Include(x => x.Ticket)
                .Select(x => new SubscriptionCardsViewModel
                {
                    Id = x.Id,
                    TypeCard = x.TypeCard,
                    ValidityCard = x.ValidityCard,
                    CountTrips = x.CountTrips,
                    Price = x.Price,
                    TicketPrice = x.Ticket.Price
                }).ToList();

            return model;
        }

        public BaseModel CreateSubscriptionCard(CreateSubscriptionCardInputModel model)
        {
            var ticketId = GetTicketId(model.Price, model.CountTrips);


            if (!Enum.TryParse(model.TypeCard, out TypeCard typeCard))
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidCardType;
                return this.BaseModel;
            }

            if (!Enum.TryParse(model.ValidityCard, out ValidityCard validityCard))
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidValidityType;
                return this.BaseModel;
            }

            var card = this.Db.SubscriptionCards
                .Include(x => x.Ticket)
                .Where(x => x.CountTrips == model.CountTrips
                       && x.Price == model.Price
                       && x.TypeCard == typeCard
                       && x.ValidityCard == validityCard
                       && x.TicketId == ticketId)
                .FirstOrDefault();

            if (card == null)
            {
                card = new SubscriptionCard
                {
                    TypeCard = typeCard,
                    ValidityCard = validityCard,
                    CountTrips = model.CountTrips,
                    Price = model.Price,
                    TicketId = ticketId
                };

                try
                {
                    this.Db.SubscriptionCards.Add(card);
                    this.Db.SaveChanges();

                    this.BaseModel.HasError = false;
                    this.BaseModel.Message = MessageConstants.CreateSubscriptionCard;
                }
                catch (Exception)
                {
                    this.BaseModel.HasError = true;
                    this.BaseModel.Message = MessageConstants.NoCreateSubscriptionCard;
                }
            }
            else
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.HaveSubscriptionCard;
            }

            return this.BaseModel;

        }

        public EditSubscriptionCardInputModel GetEditSubscriptionCardModel(int id)
        {
            var model = this.Db.SubscriptionCards
                .Where(x => x.Id == id)
                .Select(x => new EditSubscriptionCardInputModel
                {
                    Id = x.Id,
                    ValidityCard = x.ValidityCard.ToString(),
                    Price = x.Price
                }).FirstOrDefault();

            if (model == null)
            {
                return null;
            }

            return model;
        }

        public BaseModel EditSubscriptionCard(EditSubscriptionCardInputModel model)
        {
            var subscritionCard = this.Db.SubscriptionCards.FirstOrDefault(x => x.Id == model.Id);

            if (subscritionCard == null)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidCardId;
                return this.BaseModel;
            }
            if (!Enum.TryParse(model.ValidityCard, out ValidityCard validityCard))
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidValidityType;
                return this.BaseModel;
            }

            var ticketId = GetTicketId(model.Price, subscritionCard.CountTrips);

            var customerCards = this.Db.CustomerCards
                .Where(x => x.Type == subscritionCard.TypeCard
                       && x.CountTrips == subscritionCard.CountTrips)
                       .ToList();
           
            subscritionCard.Price = model.Price;
            subscritionCard.ValidityCard = validityCard;
            subscritionCard.TicketId = ticketId;
           
            try
            {
                this.Db.SubscriptionCards.Update(subscritionCard);
                this.Db.SaveChanges();

                this.BaseModel.HasError = false;
                this.BaseModel.Message = MessageConstants.EditSubscriptionCard;
            }
            catch (Exception)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.NoEditSubscriptionCard;
                return this.BaseModel;
            }

            if (customerCards.Any())
            {
                foreach (var customerCard in customerCards)
                {
                    customerCard.TicketId = ticketId;
                    switch (validityCard)
                    {
                        case ValidityCard.Day:
                            customerCard.ValidTo = customerCard.ValidFrom?.AddDays(1);
                            break;
                        case ValidityCard.Month:
                            customerCard.ValidTo = customerCard.ValidFrom?.AddMonths(1);
                            break;
                        case ValidityCard.Year:
                            customerCard.ValidTo = customerCard.ValidFrom?.AddYears(1);
                            break;
                        case ValidityCard.Indefinitely:
                            customerCard.ValidTo = DateTime.MaxValue;
                            break;
                    }

                    this.Db.CustomerCards.Update(customerCard);
                    this.Db.SaveChanges();
                }
            }
            return this.BaseModel;
        }

        private int GetTicketId(decimal price, int countTrips)
        {
            var ticketId = default(int);

            var priceTicket = Math.Round((price / countTrips), 2);
            var ticket = this.Db.Tickets.FirstOrDefault(x => x.Price == priceTicket);
            if (ticket == null)
            {
                ticket = new Ticket
                {
                    Price = priceTicket
                };

                this.Db.Tickets.Add(ticket);
                this.Db.SaveChanges();
                ticketId = this.Db.Tickets.FirstOrDefault(x => x.Price == ticket.Price).Id;
            }
            else
            {
                ticketId = ticket.Id;
            }

            return ticketId;
        }

    }
}
