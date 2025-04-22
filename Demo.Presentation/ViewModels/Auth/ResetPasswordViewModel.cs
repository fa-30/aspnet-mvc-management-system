using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.Auth
{
    public class ResetPasswordViewModel
    {
        [DataType(dataType: DataType.Password)]
        [Required(ErrorMessage = "Password Is required")]

        public string Password { get; set; }
        [DataType(dataType: DataType.Password)]
        [Required(ErrorMessage = "Confirm Password Is required")]
        [Compare(otherProperty: nameof(Password))]

        public string ConfirmPassword { get; set; }
    }
}
