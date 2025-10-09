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
	public class CommentManager : ICommentService
	{
		ICommentDal _commentDal;
		
		public CommentManager(ICommentDal commentDal)
		{
			_commentDal = commentDal;
		}
  
		public void AddComment(Comment comment)
		{
			_commentDal.İnsert(comment);
		}

		public List<Comment> GetAllComments(int id)
		{
			return _commentDal.GetListAll(x=>x.DescriptionID == id);
	
		}



		
	}
}
