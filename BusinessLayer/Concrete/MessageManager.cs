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
	public class MessageManager : IMessageService
	{
		IMessageDal _messageDal;

		public MessageManager(IMessageDal messageDal)
		{
			_messageDal = messageDal;
		}

		public void DeleteWriterMessagesByWriterId(int id)
		{
			_messageDal.DeleteWriterMessagesByWriterId(id);
		}

		public List<Message> GetAll()
		{
			throw new NotImplementedException();
		}

		public List<Message> GetAllMessagesWithWriters()
		{
			return _messageDal.GetAllMessagesWithWriters();
		}

		public Message GetById(int id)
		{
			return _messageDal.GetById(id);
		}

		public List<Message> GetChatList(int id)
		{
			return _messageDal.GetChatList(id);
		}

		public List<Message> GetLast4Messages(int id)
		{
			return _messageDal.GetLast4Messages(id);
		}

		public List<Message> GetMessagesBetweenUsers(int user1Id, int user2Id)
		{
			return _messageDal.GetMessagesBetweenUsers(user1Id, user2Id);
		}

		public List<Message> GetReceivedMessagesById(int id)
		{
			return _messageDal.GetReceivedMessagesById(id);
		}

		public List<Message> GetSentMessagesById(int id)
		{
			return _messageDal.GetSentMessagesById(id);
		}

		public void TAdd(Message t)
		{
			_messageDal.İnsert(t);
		}

		public void TDelete(Message t)
		{
			_messageDal.Delete(t);
		}

		public void TUpdate(Message t)
		{
			throw new NotImplementedException();
		}
	}
}
