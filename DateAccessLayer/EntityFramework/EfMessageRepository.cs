using DateAccessLayer.Abstract;
using DateAccessLayer.Concrete;
using DateAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateAccessLayer.EntityFramework
{
	public class EfMessageRepository : GenericRepository<Message> , IMessageDal
	{
		
		public List<Message> GetLast4Messages(int id)
		{
			using (var c = new Context())
			{
				return c.Messages
				 .Include(x => x.Sender) // Gönderen bilgisi de gelsin
		         .Where(x => x.ReceiverID == id && x.IsRead == false) // sadece okunmamış olanlar
		         .OrderByDescending(x => x.MessageDate) // en yeniler önce
		         .Take(4) // sadece 4 tanesi
		         .ToList();
			}
		}

		public List<Message> GetChatList(int id)
		{
			using (var c = new Context())
			{
				// Önce son mesaj ID'lerini al
				var lastMessageIds = c.Messages
					.Where(m => m.SenderID == id || m.ReceiverID == id)
					.GroupBy(m => m.SenderID == id ? m.ReceiverID : m.SenderID)
					.Select(g => g.OrderByDescending(m => m.MessageDate).Select(m => m.MessageID).FirstOrDefault())
					.ToList();

				// Sonra bu mesajları Include ile getir
				var chatList = c.Messages
					.Include(m => m.Sender)
					.Include(m => m.Receiver)
					.Where(m => lastMessageIds.Contains(m.MessageID))
					.OrderByDescending(m => m.MessageDate)
					.ToList();

				return chatList;
			}

	    }
		public List<Message> GetMessagesBetweenUsers(int user1Id, int user2Id)
		{
			using (var context = new Context())
			{
				return context.Messages
					.Include(m => m.Sender)
					.Include(m => m.Receiver)
					.Where(m => (m.SenderID == user1Id && m.ReceiverID == user2Id) ||
							   (m.SenderID == user2Id && m.ReceiverID == user1Id))
					.OrderBy(m => m.MessageDate)
					.ToList();
			}
		}

		public List<Message> GetSentMessagesById(int id) 
		{
			using (var context = new Context())
			{
				return context.Messages
					.Where(m => m.SenderID == id)
					.Include (m => m.Receiver)
					.AsNoTracking()
					.OrderByDescending(m => m.MessageDate)
					.ToList();

			}
		
		}

		public List<Message> GetReceivedMessagesById(int id) 
		{
			using (var context = new Context())
			{
				return context.Messages
					.Where(m => m.ReceiverID == id)
			        .Include(m => m.Sender) 
			        .AsNoTracking() 
			        .OrderByDescending(m => m.MessageDate) 
			        .ToList();

			}
		}

		public List<Message> GetAllMessagesWithWriters() 
		{
			using (var context = new Context())
			{
				return context.Messages
					.Include(x => x.Sender)
					.Include(x => x.Receiver)
					.OrderByDescending (x => x.MessageDate)
					.ToList();

			}
		}

		public void DeleteWriterMessagesByWriterId(int id) 
		{

			using (var context = new Context())
			{

				context.Messages.Where(x => x.SenderID == id || x.ReceiverID == id).ExecuteDelete();
				
			}

		}



	}
	
}

