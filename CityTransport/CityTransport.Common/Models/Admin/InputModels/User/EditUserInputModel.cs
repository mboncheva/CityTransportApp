namespace CityTransport.Common.Models.Admin.InputModels.User
{
    using CityTransport.Common.Constants;
    using System.ComponentModel.DataAnnotations;

    public class EditUserInputModel
    {

        [Required]
        [Display(Name = "Username")]
        [MinLength(ValidationConstants.UserNameMinLength,
                ErrorMessage = ValidationConstants.UsernameMinLengthErrorMessage)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        [MinLength(ValidationConstants.UserFirstNameMinLength,
            ErrorMessage = ValidationConstants.UserFirstNameMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.UserFirstNameMaxLength,
            ErrorMessage = ValidationConstants.UserFirstNameMaxLengthErrorMessage)]
        public string FisrtName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        [MinLength(ValidationConstants.UserLastNameMinLength,
            ErrorMessage = ValidationConstants.UserLastNameMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.UserLastNameMaxLength,
            ErrorMessage = ValidationConstants.UserLastNameMaxLengthErrorMessage)]
        public string LastName { get; set; }
    }
}
