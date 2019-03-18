namespace CityTransport.Common.Models.Admin.ViewModels.User
{
    using CityTransport.Common.Models.Admin.InputModels.CustomerCard;
    using CityTransport.Common.Models.Admin.InputModels.User;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class UserDetailsViewModel
    {
        public UserDetailsViewModel()
        {
            this.UserRoles = new HashSet<string>();
            this.Roles = new HashSet<SelectListItem>();
            this.CardTypes = new HashSet<SelectListItem>();

        }

        public string Id { get; set; }

        public string Tab { get; set; }

        public EditUserInputModel UserEdit { get; set; }

        public ChangePasswordInputModel UserChangePassword { get; set; }

        public CreateCustomerCardInputModel CreateCustomerCard { get; set; }

        public EditCustomerCardInputModel EditCustomerCard { get; set; }

        public IEnumerable<string> UserRoles { get; set; }

        public IEnumerable<SelectListItem> CardTypes { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }

    }
}
