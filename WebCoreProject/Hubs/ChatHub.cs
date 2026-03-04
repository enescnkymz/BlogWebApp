using Microsoft.AspNetCore.SignalR;

namespace WebCoreProject.Hubs
{
	public class ChatHub : Hub
	{
		// Mesaj gönderme
		public async Task SendMessage(int receiverId, string message)
		{
			// Mesajı hem gönderene hem alıcıya gönder
			await Clients.User(receiverId.ToString())
				.SendAsync("ReceiveMessage", Context.User.Identity.Name, message);
			await Clients.Caller.SendAsync("MessageSent", receiverId, message);
		}

		// Kullanıcı çevrimiçi olduğunda
		public override async Task OnConnectedAsync()
		{
			await Clients.All.SendAsync("UserOnline", Context.ConnectionId);
			await base.OnConnectedAsync();
		}

		// Kullanıcı çevrimdışı olduğunda
		public override async Task OnDisconnectedAsync(Exception exception)
		{
			await Clients.All.SendAsync("UserOffline", Context.ConnectionId);
			await base.OnDisconnectedAsync(exception);
		}
	}
}
