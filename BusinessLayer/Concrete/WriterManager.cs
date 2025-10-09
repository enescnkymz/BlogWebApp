using DateAccessLayer.Abstract;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class WriterManager : IWriterService
	{
		IWriterDal _writerDal;
		public WriterManager(IWriterDal writerDal) 
		{ 
			_writerDal = writerDal;
		}
		public void AddWriter(Writer writer)
		{
			_writerDal.İnsert(writer);
		}

		public void DeleteWriter(Writer writer)
		{
			throw new NotImplementedException();
		}

		public List<Writer> GetAllWriters()
		{
			throw new NotImplementedException();
		}

		public Category GetWriterById(int id)
		{
			throw new NotImplementedException();
		}

		public void UpdateWriter(Writer writer)
		{
			throw new NotImplementedException();
		}
	}
}
