using DateAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateAccessLayer.Abstract
{
	public interface ICommentDal : IGenericDal<Comment>
	{
		List<Comment> GetCommentsWithWriterByBlogId(int blogId);
		int GetCommentCountById(int id);
		void DeleteCommentsByPostId(int id);
		List<Comment> GetCommentsForAdmin();
        void DeleteCommentsByWriterId(int id);
	}
}
