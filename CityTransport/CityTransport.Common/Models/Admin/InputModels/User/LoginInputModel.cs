namespace CityTransport.Common.Models.Admin.InputModels.User
{
    using CityTransport.Common.Constants;
    using System.ComponentModel.DataAnnotations;

    public class LoginInputModel
    {
        [Required]
        [MinLength(ValidationConstants.UserNameMinLength,
               ErrorMessage = ValidationConstants.UsernameMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.UserNameMaxLength,
               ErrorMessage = ValidationConstants.UsernameMaxLengthErrorMessage)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(ValidationConstants.PasswordMinLength,
            ErrorMessage = ValidationConstants.PasswordMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.PasswordMaxLength,
            ErrorMessage = ValidationConstants.PasswordMaxLengthErrorMessage)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
