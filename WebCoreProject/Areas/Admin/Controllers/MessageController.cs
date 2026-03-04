using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebCoreProject.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class MessageController : Controller
	{
		private readonly IMessageService _messageService;

		public MessageController(IMessageService messageService)
		{
			_messageService = messageService;
		}

		public IActionResult Index()
		{
			var messages = _messageService.GetAllMessagesWithWriters();
			return View(messages);
		}


		public IActionResult DeleteMessage(int id) 
		{
			var msg = _messageService.GetById(id);
			if (msg != null)
			{
				_messageService.TDelete(msg);
			}
			return RedirectToAction("Index");
		}

	}
}
