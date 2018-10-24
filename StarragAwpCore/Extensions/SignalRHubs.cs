
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace  StarragAwpCore
{
    public class ClientHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
