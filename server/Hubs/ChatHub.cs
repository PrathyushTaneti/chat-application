using Microsoft.AspNetCore.SignalR;
using server.Models;

namespace server.Hubs
{
    public class ChatHub(IDictionary<string, UserRoomConnection> connections) : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IDictionary<string, UserRoomConnection> connections = connections;

        public async Task JoinRoom(UserRoomConnection userRoomConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userRoomConnection.Room);
            this.connections.Add(Context.ConnectionId, userRoomConnection);
            await Clients.Group(userRoomConnection.Room).SendAsync("ReceiveMessage", "ProgramBot", $"{userRoomConnection.User} has joined the room.");
            await SendConnectedUser(userRoomConnection.Room);
        }

        public async Task SendMessage(string message)
        {
            if (connections.TryGetValue(Context.ConnectionId, out UserRoomConnection? userRoomConnection))
            {
                await Clients.Group(userRoomConnection.Room).SendAsync("ReceivedMessage", userRoomConnection.User, message, DateTime.Now);
            }
        }

        public Task SendConnectedUser(string room)
        {
            var users = this.connections.Values
                .Where(user => user.Room == room)
                .Select(s => s.User);
            return Clients.Group(room).SendAsync("ConnectionUser", users);
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (connections.TryGetValue(Context.ConnectionId, out UserRoomConnection? userRoomConnection))
            {
                return base.OnDisconnectedAsync(exception);
            }
            Clients.Group(userRoomConnection.Room).SendAsync("ReceiveMessage", "ProgramBot", $"{userRoomConnection.User} has left the room.");
            SendConnectedUser(userRoomConnection.Room);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
