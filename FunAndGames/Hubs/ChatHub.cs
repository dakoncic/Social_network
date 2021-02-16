using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunAndGames.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string username, string text, string chatwindowdata1, string chatwindowdata2)
        {
            await Clients.All.SendAsync("ReceiveMessage", username, text, chatwindowdata1, chatwindowdata2);
        }
        public async Task OpenChatWindow(string id1, string id2, string username)
        {
            await Clients.All.SendAsync("ReceiveOpenChatWindow", id1, id2, username);
        }
    }
}
