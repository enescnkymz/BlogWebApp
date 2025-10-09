using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface IWriterService
	{

		void AddWriter(Writer writer);
		void DeleteWriter(Writer writer);
		void UpdateWriter(Writer writer);
		List<Writer> GetAllWriters();
		Category GetWriterById(int id);



	}
}
