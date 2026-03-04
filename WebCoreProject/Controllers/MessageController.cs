using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DateAccessLayer.Concrete;
using DateAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Claims;
using WebCoreProject.Models;

namespace WebCoreProject.Controllers
{
	public class MessageController : Controller
	{

		private readonly IMessageService _messageService;
		private readonly IWriterService _writerService;

		public MessageController(IMessageService messageService, IWriterService writerService)
		{
			_messageService = messageService;
			_writerService = writerService;
		}

		[HttpGet]
		public IActionResult Chat()
		{

			return View();
		}

		[HttpPost]
		public IActionResult SendMessageAjax(SendMessageViewModel m)
		{
			if (!ModelState.IsValid)
			{
				return Json(new { success = false, message = "Lütfen alanları doldurun." });
			}

			int rID = _writerService.FindUserIdByMail(m.ReceiverMail);
			if (rID <= 0)
			{
				return Json(new { success = false, message = "Alıcı bulunamadı." });
			}

			var newMessage = new Message
			{
				SenderID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
				ReceiverID = rID,
				MessageDetails = m.MessageContent,
				MessageDate = DateTime.Now,
				IsRead = false
			};
			_messageService.TAdd(newMessage);

			return Json(new { success = true, message = "Mesajınız iletildi!" });



		}

		public IActionResult DeleteMessage(int id , string folder)
		{
			int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			var msg = _messageService.GetById(id);
			if (msg != null && (msg.SenderID ==currentUserId ||msg.ReceiverID==currentUserId))
			{
				_messageService.TDelete(msg);
			}			
			return RedirectToAction("Chat" , new {activeTab = folder} );

		}














		//MessageManager newMessageService = new MessageManager(new EfMessageRepository());
		//public IActionResult Chat()
		//{

		//	int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));		
		//	var messages = newMessageService.GetChatList(currentUserId);

		//	ViewBag.CurrentUserId = currentUserId;
		//	return View(messages);
		//}

		//// Belirli bir kullanıcıyla olan mesajları getir
		//[HttpGet]
		//public IActionResult GetMessages(int receiverId)
		//{

		//	int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
		//	var messages = _messageService.GetMessagesBetweenUsers(currentUserId, receiverId);

		//	return Json(messages.Select(m => new {
		//		id = m.MessageID,
		//		senderId = m.SenderID,
		//		receiverId = m.ReceiverID,
		//		text = m.MessageDetails,
		//		date = m.MessageDate.ToString("HH:mm"),
		//		type = m.SenderID == currentUserId ? "sent" : "received"
		//	}));
		//}

		//// Yeni mesaj kaydet
		//[HttpPost]
		//public IActionResult SendMessage(int receiverId, string message)
		//{
		//	int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));


		//	var newMessage = new Message
		//	{
		//		SenderID = currentUserId,
		//		ReceiverID = receiverId,
		//		MessageDetails = message,
		//		MessageDate = DateTime.Now,

		//	};

		//	_messageService.TAdd(newMessage);

		//	return Json(new
		//	{
		//		success = true,
		//		messageId = newMessage.MessageID,
		//		time = newMessage.MessageDate.ToString("HH:mm")
		//	});
		//}
	}

}

