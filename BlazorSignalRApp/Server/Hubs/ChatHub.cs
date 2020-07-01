using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace BlazorSignalRApp.Server.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        /*
         * group case sensitivitive
         */

        //将消息发送到所有连接的客户端 
        [HubMethodName("SendMessageToAll")]  // 自定义razor页面调用方法名 而不是使用.net的方法名
        public async Task SendMessageToAll(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
        }

        //将消息发回给调用方
        [HubMethodName("SendMessageToCaller")]
        public async Task SendMessageToCaller(string user, string message)
        {
            await Clients.Caller.ReceiveMessage(user, message);
        }

        //向组中的所有客户端发送一条消息
        [HubMethodName("SendMessageToGroup")]
        public async Task SendMessageToGroup(string groupName, string message)
        {
            await Clients.Group(groupName).ReceiveMessage(message);
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendMessage($"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendMessage($"{Context.ConnectionId} has left the group {groupName}.");
        }

        public async Task SendPrivateMessage(string user, string message)
        {
            await Clients.User(user).ReceiveMessage(message);
        }

        // 处理连接事件

        public override async Task OnConnectedAsync()
        {
            // 客户端连接到server时，将其添加到SignalR User 组中
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR User");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // 当客户端断开连接时，将其从SignalR User 组中移除
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR User");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
