using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.Auth
{
    public class ForgetPasswordViewModel
    {
        [DataType(dataType: DataType.EmailAddress)]
        [Required(ErrorMessage = "Email Is Required")]

        public string Email { get; set; } = null!;
    }
}
