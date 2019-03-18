namespace CityTransport.Common.Constants
{
    public static class ValidationConstants
    {
        public const int UserNameMinLength = 2;
        public const int UserNameMaxLength = 50;
        public const string UsernameMinLengthErrorMessage = "Username must contains at least 2 characters!";
        public const string UsernameMaxLengthErrorMessage = "Username must contains a maximum of 50 characters!";
    
        public const int UserFirstNameMinLength = 2;
        public const int UserFirstNameMaxLength = 50;
        public const string UserFirstNameMinLengthErrorMessage = "FirstName must contains at least 2 characters!";
        public const string UserFirstNameMaxLengthErrorMessage = "FisrtName must contains a maximum of 50 characters!";


        public const int UserLastNameMinLength = 2;
        public const int UserLastNameMaxLength = 50;
        public const string UserLastNameMinLengthErrorMessage = "LatsName must contains at least 2 characters!";
        public const string UserLastNameMaxLengthErrorMessage = "LastName must contains a maximum of 50 characters!";

        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 100;
        public const string PasswordMinLengthErrorMessage = "Password must contains at least 6 characters!";
        public const string PasswordMaxLengthErrorMessage = "Password must contains a maximum of 100 characters!";
        public const string PasswordConfirmErrorMessage = "The password and confirmation password do not match!";

        public const string TicketMinPrice = "0";
        public const string TicketMaxPrice = "10";
        public const string TicketPriceErrorMessage = "Ticket price can be at least 0.00 and maximum 10.00!";

        public const string SubscriptionCardMinPrice = "1";
        public const string SubscriptionCardMaxPrice = "1500";
        public const string SubscriprionCardPriceErrorMessage = "Subscription Card price can be at least 0.00 and maximum 1500.00!";

        public const int CountTripsMin = 0;
        public const int CountTripsMax = int.MaxValue;
        public const string CountTripsMinErrorMessage = "Count of trips can be at least 0!";

        public const int StationNameMinLength = 1;
        public const int StationNameMaxLength = 30;
        public const string StationNameMinLengthErrorMessage = "StationName must contains at least 1 characters!";
        public const string StationNameMaxLengthErrorMessage = "StationName must contains a maximum of 30 characters!";

        public const int StationCodeMinLength = 4;
        public const int StationCodeMaxLength = 6;
        public const string StationCodeMinLengthErrorMessage = "StationCode must contains at least 4 characters!";
        public const string StationCodeMaxLengthErrorMessage = "StationCode must contains a maximum of 6 characters!";
        public const string StationCodeRegexErrorMessage = "StationCode must contains digits!";

        public const int RouteNameMinLength = 1;
        public const int RouteNameMaxLength = 20;
        public const string RouteNameMinLengthErrorMessage = "RouteName must contains at least 1 characters!";
        public const string RouteNameMaxLengthErrorMessage = "RouteName must contains a maximum of 20 characters!";

        public const int LineNameMaxLength = 10;
        public const string LineNameMaxLengthErrorMessage = "LineName must contains a maximum of 10 characters!";
        public const string LineNameRegexErrorMessage = "LineName must contains at least 1 digit and may contain a capital letter!";

    }
}
