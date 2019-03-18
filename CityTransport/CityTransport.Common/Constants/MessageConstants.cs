namespace CityTransport.Common.Constants
{
    public static class MessageConstants
    {
        public const string Name = "__Message";

        //Invalid Types and id
        public const string InvalidUserId = "Invalid User Id!";

        public const string InvalidCardId = "Invalid Card Id!";

        public const string InvalidStationId = "Invalid Station Id!";

        public const string InvalidLineId = "Invalid Line Id!";

        public const string InvalidRouteId = "Invalid Route Id!";

        public const string InvalidTypeTransport = "Invalid transport type";

        public const string InvalidCardType = "Invalid card type";

        public const string InvalidValidityType = "Invalid validity card type";

        public const string InvalidDirectionType = "Invalid direction type";

        public const string InvalidDayType = "Invalid day type";

        //Create and edit cards
        public const string CreateCustomerCard = "Customer card created successfully!";

        public const string NoCreateCustomerCard = "Customer Card is not created successfully!";

        public const string EditCustomerCard = "Customer card edited successfully!";

        public const string NoEditCustomerCard = "Customer Card isn't edited successfully!";

        public const string NoCustomerCard = "{0} hasn't customer card!";

        public const string HaveCustomerCardNumber = "There is a card with {0} number!";

        public const string NoSubCard = "There isn't {0} subscription card with {1} count of trips! Look at Subscription Cards";

        public const string HaveSubscriptionCard = "There is such a subscription card!";

        public const string CreateSubscriptionCard = "Subscription card created successfully!";

        public const string NoCreateSubscriptionCard = "Subscription Card isn't created successfully!";

        public const string EditSubscriptionCard = "Subscription card edited successfully!";

        public const string NoEditSubscriptionCard = "Subscription Card isn't edited successfully!";


        //Stations

        public const string HaveStationName = "There is a station with {0} name!";

        public const string HaveStationCode = "There is a station with {0} code!";

        public const string CreateStation= "Station created successfully!";

        public const string NoCreateStation = "Station isn't created successfully!";

        public const string EditStation = "Station edited successfully!";

        public const string NoEditStation = "Station isn't edited successfully!";


        //Lines

        public const string HaveLineName = "There is a line with {0} name!";

        public const string CreateLine = "Line created successfully!";

        public const string NoCreateLine = "Line isn't created successfully!";

        public const string EditLine = "Line edited successfully!";

        public const string NoEditLine = "Line isn't edited successfully!";

        //Routes

        public const string HaveRouteName = "There is a route with {0} name!";

        public const string CreateRoute = "Route created successfully!";

        public const string NoCreateRoute = "Route isn't created successfully!";


        //TimeTables
        public const string HaveStopName = "This route have a stop with {0} name!";

        public const string HaveStopHour = "This route have a stop with {0} hour!";

        public const string CreateStop = "Stop created successfully!";

        public const string NoCreateStop = "Stop isn't created successfully!";

        public const string EditStop = "Stop edited successfully!";

        public const string NoEditStop = "Stop isn't edited successfully!";


        //User
        public const string CreateUser = "User created successfully!";

        public const string EditUser = "User edited successfully!";

        public const string NoEditUser = "User was not edited successfully!";

        public const string ChangePassword = "Password change successfully!";

        public const string NoChangePassword = "Password change is unsuccessful!";

        public const string DeleteUser = "User deleted successfully!";

        public const string RoleDoesntExist = "Role doesn't exist";

        public const string AddUserToRole = "User added to role successfully!";

        public const string NoAddUserToRole = "User doesn't add to role!";

        public const string RemoveUserFromRole = "User remove from role successfully!";

        public const string NoRemoveUserFromRole = "User doesn't remove from role!";

      
    }
}
