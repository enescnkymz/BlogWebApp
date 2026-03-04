using System.ComponentModel.DataAnnotations;

namespace WebCoreProject.Models
{
	public class UserSignUpViewModel
	{

		[Required(ErrorMessage ="Ad Soyad boş geçilemez")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Mail adresi boş geçilemez")]
		[EmailAddress(ErrorMessage = "Geçerli bir mail giriniz")]
		public string Mail { get; set; }

		[Required(ErrorMessage = "Şifre boş geçilemez")]
		public string Password { get; set; }

		[Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
		public string ConfirmPassword { get; set; }


	}
}
