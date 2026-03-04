using BusinessLayer.DTOs;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICommentService : IGenericService<Comment>
    {
		List<Comment> GetCommentsWithWriterByBlogId(int blogId);
		int GetCommentCountById(int id);
		void DeleteCommentsByPostId(int id);
		List<CommentAdminDto> GetCommentsForAdmin();
		void DeleteCommentsByWriterId(int id);
	}
}
