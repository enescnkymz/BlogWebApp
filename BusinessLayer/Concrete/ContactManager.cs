using BusinessLayer.Abstract;
using DateAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class ContactManager : IContactService
	{
		IContactDal _IContactDal;

		public ContactManager(IContactDal contactDal)
		{
			_IContactDal = contactDal;
		}

		public void AddContact(Contact contact)
		{
			_IContactDal.İnsert(contact);
		}
	
	
	}
}
