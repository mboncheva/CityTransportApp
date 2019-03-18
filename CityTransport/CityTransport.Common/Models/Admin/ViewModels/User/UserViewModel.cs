namespace CityTransport.Common.Models.Admin.ViewModels.User
{
    using System;

    public class UserViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string CustomerCardNumber { get; set; }

        public DateTime RegisteredOn { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsUser { get; set; }
    }
}
