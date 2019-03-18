namespace CityTransport.Common.Models.Admin.InputModels.User
{
    using CityTransport.Common.Constants;
    using System.ComponentModel.DataAnnotations;

    public class ChangePasswordInputModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MinLength(ValidationConstants.PasswordMinLength,
                ErrorMessage = ValidationConstants.PasswordMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.PasswordMaxLength,
                ErrorMessage = ValidationConstants.PasswordMaxLengthErrorMessage)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(nameof(Password), ErrorMessage = ValidationConstants.PasswordConfirmErrorMessage)]
        public string ConfirmPassword { get; set; }
    }
}
