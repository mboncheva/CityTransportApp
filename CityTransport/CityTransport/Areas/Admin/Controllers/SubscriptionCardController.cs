namespace CityTransport.Web.Areas.Admin.Controllers
{
    using CityTransport.Common.Constants;
    using CityTransport.Common.Models.Admin.InputModels.SubscriptionCard;
    using CityTransport.Models.Enums;
    using CityTransport.Services.Admin.Interfaces;
    using CityTransport.Web.Common.Extensions;
    using CityTransport.Web.Common.Helpers.Messages;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SubscriptionCardController : AdminController
    {
        private readonly ISubscriptionCardService SubscriptionCardService;

        public SubscriptionCardController(ISubscriptionCardService subscriptionCardService)
        {
            this.SubscriptionCardService = subscriptionCardService;
        }

        public IActionResult Index()
        {
            var model = this.SubscriptionCardService.SubscriptionCards().ToList();

            return this.View(model);
        }

        public IActionResult Create()
        {
            List<SelectListItem> cardTypes, validityCards;
            GetCardTypesAndValidityCards(out cardTypes, out validityCards);

            var model = new CreateSubscriptionCardInputModel
            {
                CardTypes = cardTypes,
                ValidityCards = validityCards
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateSubscriptionCardInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = this.SubscriptionCardService.CreateSubscriptionCard(model);
            if (!result.HasError)
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel
                {
                    Type = MessageType.Success,
                    Message = result.Message
                });
            }
            else
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel
                {
                    Type = MessageType.Danger,
                    Message = result.Message
                });

                return this.RedirectToAction(nameof(Create));

            }

            return this.RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var model = this.SubscriptionCardService.GetEditSubscriptionCardModel(id);

            if (model == null)
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel
                {
                    Type = MessageType.Danger,
                    Message = MessageConstants.InvalidCardId
                });

                return this.RedirectToAction(nameof(Index));
            }

            List<SelectListItem> cardTypes, validityCards;
            GetCardTypesAndValidityCards(out cardTypes, out validityCards);
            model.ValidityCards = validityCards;

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditSubscriptionCardInputModel model)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> cardTypes, validityCards;
                GetCardTypesAndValidityCards(out cardTypes, out validityCards);
                model.ValidityCards = validityCards;

                return this.View(model);
            }

            var result = this.SubscriptionCardService.EditSubscriptionCard(model);

            if (!result.HasError)
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel
                {
                    Type = MessageType.Success,
                    Message = result.Message
                });
            }
            else
            {
                this.TempData.Put(MessageConstants.Name, new MessageModel
                {
                    Type = MessageType.Danger,
                    Message = result.Message
                });

                return this.RedirectToAction(nameof(Edit), new { id= model.Id});

            }

            return this.RedirectToAction(nameof(Index));
        }

        private static void GetCardTypesAndValidityCards
            (out List<SelectListItem> cardTypes, out List<SelectListItem> validityCards)
        {
            cardTypes = Enum.GetValues(typeof(TypeCard)).Cast<TypeCard>()
                           .Select(x => new SelectListItem
                           {
                               Text = Enum.GetName(typeof(TypeCard), x),
                               Value = x.ToString()
                           }).ToList();
            validityCards = Enum.GetValues(typeof(ValidityCard)).Cast<ValidityCard>()
               .Select(x => new SelectListItem
               {
                   Text = Enum.GetName(typeof(ValidityCard), x),
                   Value = x.ToString()
               }).ToList();
        }
    }
}