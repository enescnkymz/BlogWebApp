using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Identity
{
	public class TurkishIdentityErrorDescriber : IdentityErrorDescriber
	{
		// "Passwords must be at least 6 characters." hatası için:
		public override IdentityError PasswordTooShort(int length)
		{
			return new IdentityError
			{
				Code = nameof(PasswordTooShort),
				Description = $"Parola en az {length} karakter olmalıdır."
			};
		}

		// "Passwords must have at least one non alphanumeric character." hatası için:
		public override IdentityError PasswordRequiresNonAlphanumeric()
		{
			return new IdentityError
			{
				Code = nameof(PasswordRequiresNonAlphanumeric),
				Description = "Parola en az bir adet sembol (*, !, ., vs.) içermelidir."
			};
		}

		// "Passwords must have at least one digit ('0'-'9')." hatası için:
		public override IdentityError PasswordRequiresDigit()
		{
			return new IdentityError
			{
				Code = nameof(PasswordRequiresDigit),
				Description = "Parola en az bir adet rakam ('0'-'9') içermelidir."
			};
		}

		// "Passwords must have at least one uppercase ('A'-'Z')." hatası için:
		public override IdentityError PasswordRequiresUpper()
		{
			return new IdentityError
			{
				Code = nameof(PasswordRequiresUpper),
				Description = "Parola en az bir adet büyük harf ('A'-'Z') içermelidir."
			};
		}

		// Ekstra: Küçük harf gereksinimi varsa bunu da ekleyebilirsiniz
		public override IdentityError PasswordRequiresLower()
		{
			return new IdentityError
			{
				Code = nameof(PasswordRequiresLower),
				Description = "Parola en az bir adet küçük harf ('a'-z') içermelidir."
			};
		}
	}
}
