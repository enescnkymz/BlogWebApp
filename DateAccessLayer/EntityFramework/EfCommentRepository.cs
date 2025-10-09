using DateAccessLayer.Abstract;
using DateAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateAccessLayer.EntityFramework
{
	public class EfCommentRepository: GenericRepository<Comment> , ICommentDal
	{
	}
}
