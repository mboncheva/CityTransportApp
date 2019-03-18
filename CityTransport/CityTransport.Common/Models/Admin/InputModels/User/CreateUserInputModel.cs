namespace CityTransport.Common.Models.Admin.InputModels.User
{
    using CityTransport.Common.Constants;
    using System.ComponentModel.DataAnnotations;

    public class CreateUserInputModel
    {
        [Required]
        [Display(Name = "Username")]
        [MinLength(ValidationConstants.UserNameMinLength,
                ErrorMessage = ValidationConstants.UsernameMinLengthErrorMessage)]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        [MinLength(ValidationConstants.UserFirstNameMinLength,
            ErrorMessage = ValidationConstants.UserFirstNameMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.UserFirstNameMaxLength,
            ErrorMessage = ValidationConstants.UserFirstNameMaxLengthErrorMessage)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        [MinLength(ValidationConstants.UserLastNameMinLength,
            ErrorMessage = ValidationConstants.UserLastNameMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.UserLastNameMaxLength,
            ErrorMessage = ValidationConstants.UserLastNameMaxLengthErrorMessage)]
        public string LastName { get; set; }

    }
}
