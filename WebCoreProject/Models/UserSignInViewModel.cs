using System.ComponentModel.DataAnnotations;

namespace WebCoreProject.Models
{
    public class UserSignInViewModel
    {
        [Required(ErrorMessage ="Lütfen kullanıcı adını giriniz")]
        public string Username { get; set; }

        [Required(ErrorMessage ="lütfen şifrenizi giriniz")]
        public string Password { get; set; }
    }
}
