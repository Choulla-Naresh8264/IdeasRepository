using System.ComponentModel.DataAnnotations;

namespace IdeasRepository.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords not match")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}