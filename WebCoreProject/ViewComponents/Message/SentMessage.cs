using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace WebCoreProject.ViewComponents.Message
{
	public class SentMessage:ViewComponent
	{

		private readonly IMessageService _messageService;

		public SentMessage(IMessageService messageService)
		{
			_messageService = messageService;
		}

		public IViewComponentResult Invoke()
		{
			var currentUserId = int.Parse(UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));
			var values = _messageService.GetSentMessagesById(currentUserId);
			return View(values);
		}


	}
}
