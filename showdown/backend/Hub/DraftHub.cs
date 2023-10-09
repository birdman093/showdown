using Microsoft.AspNetCore.SignalR;

namespace backend.Hubs;

public class DraftHub : Hub
{
    public async Task NewMessage(string username, string groupname, string message) =>
        await Clients.Group(groupname).SendAsync("MessageReceived", username, message);

    public async Task JoinGroup(string username, string groupname, string grouppassword)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
    }

    public async Task LeaveGroup(string username, string groupname)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupname);
    }
}

