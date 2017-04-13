using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Application.API.Chat
{
    [HubName("chat")]
    public class ChatHub : Hub
    {
        public void SendMessage(string message)
        {
            Clients.All.newMessage(message);
        }

        public void SendMessageData(SendData data)
        {
            Clients.All.newData(data);
        }

    }

    public class SendData
    {
        public int Id { get; set; }
        public string Data { get; set; }
    }
}