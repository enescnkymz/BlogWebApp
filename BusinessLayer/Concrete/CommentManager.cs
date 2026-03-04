using BusinessLayer.Abstract;
using BusinessLayer.DTOs;
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

		public void DeleteCommentsByPostId(int id)
		{
			_commentDal.DeleteCommentsByPostId(id);
		}

		public List<Comment> GetAll()
		{
			throw new NotImplementedException();
		}

		public Comment GetById(int id)
		{
			 return _commentDal.GetById(id);
		}

		public int GetCommentCountById(int id)
		{
			return _commentDal.GetCommentCountById(id);
		}

		public List<Comment> GetCommentsWithWriterByBlogId(int blogId)
		{
			return _commentDal.GetCommentsWithWriterByBlogId(blogId);
		}

		public void TAdd(Comment t)
		{
			_commentDal.İnsert(t);
		}

		public void TDelete(Comment t)
		{
			_commentDal.Delete(t);
		}

		public void TUpdate(Comment t)
		{
			throw new NotImplementedException();
		}

		public List<CommentAdminDto> GetCommentsForAdmin()
		{
			var comments = _commentDal.GetCommentsForAdmin();
			var adminComments = comments.Select(c => new CommentAdminDto
			{

		 CommentId = c.CommentID,
		 CommentContent=c.CommentContent,
		 CommentSenderName =c.CommentSender.WriterName,
		 CommentCreateDate = c.CommentDate,
		 CommentPost = c.Description.DescriptionTitle,
		 CommentPostId = c.DescriptionID.Value,
		 CommentStatus = c.CommentStatus,
		 CommentSenderId =c.CommentSenderID.Value

	}).ToList();

			return adminComments;


		}

		public void DeleteCommentsByWriterId(int id)
		{
			_commentDal.DeleteCommentsByWriterId(id);
		}
	}
}
