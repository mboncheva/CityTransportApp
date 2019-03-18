namespace CityTransport.Services.Admin
{
    using CityTransport.Common.Constants;
    using CityTransport.Common.Models;
    using CityTransport.Common.Models.Admin.InputModels.CustomerCard;
    using CityTransport.Data;
    using CityTransport.Models;
    using CityTransport.Models.Enums;
    using CityTransport.Services.Admin.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class CustomerCardService : BaseService, ICustomerCardService
    {
        private readonly UserManager<User> UserManager;
        private readonly BaseModel BaseModel;

        public CustomerCardService(CityTransprtDbContext Db, UserManager<User> userManager)
            :base(Db)
        {
            this.UserManager = userManager;
            this.BaseModel = new BaseModel();
        }

        public async Task<BaseModel> CreateCustomerCard(CreateCustomerCardInputModel model, string id)
        {
            var user = await this.UserManager.FindByIdAsync(id);

            if (user == null)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidUserId;
                return this.BaseModel;
            }
            if (!Enum.TryParse(model.TypeCard, out TypeCard type))
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidCardType;
                return this.BaseModel;
            }

            var subscriptionCard = this.Db.SubscriptionCards
              .Where(x => x.TypeCard == type && x.CountTrips == model.CountTrips)
              .Include(x => x.Ticket)
              .FirstOrDefault();
 
            if (subscriptionCard == null)
            {                 
                this.BaseModel.HasError = true;
                this.BaseModel.Message = string.Format(MessageConstants.NoSubCard, model.TypeCard, model.CountTrips);

                return this.BaseModel;
            }

            var customerCards = this.Db.CustomerCards.ToList();
            var cardNumber = string.Empty;
            if (!customerCards.Any())
            {
                var yearNow = DateTime.Now.Year.ToString().Substring(2, 2);
                cardNumber = $"{yearNow}001";
            }
            else
            {
                var max = customerCards.Where(x => x.CustomerCardNumber != null)
                    .Select(x => int.Parse(x.CustomerCardNumber))
                    .Max();
                cardNumber = (max + 1).ToString();
            }

            var customerCard = new CustomerCard
            {
                CountTrips = model.CountTrips,
                ValidFrom = model.ValidateFrom,
                UserId = user.Id,
                TicketId = subscriptionCard.Id,
                Type = type,
                CustomerCardNumber = cardNumber
            };
            var validityCard = subscriptionCard.ValidityCard;
            switch (validityCard)
            {
                case ValidityCard.Day:
                    customerCard.ValidTo = model.ValidateFrom.AddDays(1);
                    break;
                case ValidityCard.Month:
                    customerCard.ValidTo = model.ValidateFrom.AddMonths(1);
                    break;
                case ValidityCard.Year:
                    customerCard.ValidTo = model.ValidateFrom.AddYears(1);
                    break;
                case ValidityCard.Indefinitely:
                    customerCard.ValidTo = DateTime.MaxValue;
                    break;
            }
            try
            {
                this.Db.CustomerCards.Add(customerCard);
                this.Db.SaveChanges();

                var cardId = this.Db.CustomerCards
                .FirstOrDefault(x => x.CustomerCardNumber == customerCard.CustomerCardNumber).Id;
                user.CustomerCardId = cardId;

                this.Db.Update(user);
                this.Db.SaveChanges();

                this.BaseModel.HasError = false;
                this.BaseModel.Message = MessageConstants.CreateCustomerCard;
            }
            catch (Exception)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.NoCreateCustomerCard;
            }

            return this.BaseModel;
        }

        public async Task<BaseModel> EditCustomerCard(EditCustomerCardInputModel model, string id)
        {
            var user = await this.UserManager.FindByIdAsync(id);

            if (user == null)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidUserId;
                return this.BaseModel;
            }
            if (!Enum.TryParse(model.TypeCard, out TypeCard type))
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.InvalidCardType;
                return this.BaseModel;
            }

            var card = this.Db.CustomerCards.FirstOrDefault(x => x.Id == model.Id && x.UserId == user.Id);
            if (card == null)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = string.Format(MessageConstants.NoCustomerCard, user.UserName);
                return this.BaseModel;
            }

            var subscriptionCard = this.Db.SubscriptionCards
             .Where(x => x.TypeCard == type && x.CountTrips == model.CountTrips)
             .Include(x => x.Ticket)
             .FirstOrDefault();

            if (subscriptionCard == null)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = string.Format(MessageConstants.NoSubCard, model.TypeCard, model.CountTrips);

                return this.BaseModel;
            }

            var customerCards = this.Db.CustomerCards.Include(x => x.User)
                .Any(x => x.CustomerCardNumber == model.CustomerCardNumber && x.UserId != id);
            if (customerCards)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = string.Format(MessageConstants.HaveCustomerCardNumber, model.CustomerCardNumber);
                return this.BaseModel;
            }

            card.CustomerCardNumber = model.CustomerCardNumber;
            card.CountTrips = model.CountTrips;
            card.Type = type;
            card.ValidFrom = model.ValidateFrom;
            card.TicketId = subscriptionCard.TicketId;

            var validityCard = subscriptionCard.ValidityCard;
            switch (validityCard)
            {
                case ValidityCard.Day:
                    card.ValidTo = model.ValidateFrom.AddDays(1);
                    break;
                case ValidityCard.Month:
                    card.ValidTo = model.ValidateFrom.AddMonths(1);
                    break;
                case ValidityCard.Year:
                    card.ValidTo = model.ValidateFrom.AddYears(1);
                    break;
                case ValidityCard.Indefinitely:
                    card.ValidTo = DateTime.MaxValue;
                    break;
            }

            try
            {
                this.Db.CustomerCards.Update(card);
                this.Db.SaveChanges();

                this.BaseModel.HasError = false;
                this.BaseModel.Message = MessageConstants.EditCustomerCard;
            }
            catch (Exception)
            {
                this.BaseModel.HasError = true;
                this.BaseModel.Message = MessageConstants.NoEditCustomerCard;
            }

            return this.BaseModel;
        }
    }
}
