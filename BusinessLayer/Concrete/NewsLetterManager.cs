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
	public class NewsLetterManager : INewsLetterService
	{
		INewsLetterDal _newsLetter;

		public NewsLetterManager(INewsLetterDal newsLetter)
		{
			_newsLetter = newsLetter;
		}

		public void addNewsLetter(NewsLetter newsLetter)
		{
			_newsLetter.İnsert(newsLetter);
		}
	}
}
