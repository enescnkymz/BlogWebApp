using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface IMessageService : IGenericService<Message>
	{
		 List<Message> GetLast4Messages(int id);
		 List<Message> GetChatList(int id);
		 List<Message> GetMessagesBetweenUsers(int user1Id, int user2Id);
         List<Message> GetSentMessagesById(int id);
		 List<Message> GetReceivedMessagesById(int id);
		List<Message> GetAllMessagesWithWriters();
		void DeleteWriterMessagesByWriterId(int id);

	}
}
