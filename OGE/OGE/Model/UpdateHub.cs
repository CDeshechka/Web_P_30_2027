using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace OGE.Hubs
{
    public class UpdateHub : Hub
    {
        public async Task NotifyUpdate(string entityType)
        {
            await Clients.Others.SendAsync("ReceiveUpdate", entityType);
        }
    }
}