using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebCoreProject.ViewComponents.Message
{
	public class ReceivedMessage:ViewComponent
	{
		private readonly IMessageService _messageService;

		public ReceivedMessage(IMessageService messageService)
		{
			_messageService = messageService;
		}

		public IViewComponentResult Invoke()
		{
			var currentUserId = int.Parse(UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));
			var values = _messageService.GetReceivedMessagesById(currentUserId);
			return View(values);

		}


	}
}
