using DateAccessLayer.Abstract;
using DateAccessLayer.Concrete;
using DateAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateAccessLayer.EntityFramework
{
	public class EfCommentRepository: GenericRepository<Comment> , ICommentDal
	{
		public List<Comment> GetCommentsWithWriterByBlogId(int blogId)
		{
			using(var c = new Context())
			{
				return c.Comments
					.Include(x=>x.CommentSender)
					.Where(x=>x.DescriptionID == blogId)
					.ToList();
					
			}
		
		}

		public int GetCommentCountById(int id)
		{
			using (var c = new Context())
			{
			    return c.Comments.Count(x=>x.CommentSenderID == id);
			}
		}

		public void DeleteCommentsByPostId(int id)
		{
			using (var c = new Context())
			{
				var relatedComments = c.Comments.Where(x => x.DescriptionID == id).ToList();
				c.Comments.RemoveRange(relatedComments);
				c.SaveChanges();
			}
		}

		public List<Comment> GetCommentsForAdmin()
		{

			using (var c = new Context())
			{
				return c.Comments.Include(x=>x.CommentSender).Include(x=>x.Description).OrderByDescending(x=>x.CommentDate).ToList();
			}

		}

		public void DeleteCommentsByWriterId(int id)
		{
			using (var c = new Context())
			{
				var relatedComments = c.Comments.Where(x => x.CommentSenderID == id).ToList();
				c.Comments.RemoveRange(relatedComments);
				c.SaveChanges();

			}


		}



	}
}
