using Film.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace Film.SignalR
{
    //[Authorize]
     public class NotificationsHub
    : Hub
    {
        public NotificationsHub() { }
        public static Dictionary<string,string>UsersConnected = new Dictionary<string, string>();
        public override async Task OnConnectedAsync()
        {
            string a = Context.User.Identity.Name;
            UsersConnected.Add(a, Context.ConnectionId);
            await base.OnConnectedAsync();
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", "juasjas");

        }
        public override async Task OnDisconnectedAsync(Exception e)
        {
            foreach (var item in UsersConnected.Where(kvp => kvp.Value == Context.ConnectionId).ToList())
            {
                 UsersConnected.Remove(item.Key);
            }
            await base.OnDisconnectedAsync(e);
        }
        public async Task SendMessageAllUsers(ChatMessage message)
        {
            
            await Clients.All.SendAsync("ReceiveMessage", message);
            
        }

        public  async Task SendMessageUser(string message, string name)
        {
            if(UsersConnected.ContainsKey(name))
             await Clients.Client(UsersConnected[name]).SendAsync("ReceiveMessage", message);

        }

        public void  SetUser(string name)
        {
            //string a = Context.User.Identity.Name;
             //Clients.Client(a).SendAsync("ReceiveMessage", name);
            //UsersConnected.Add(name,Context.UserIdentifier);
        }
    }

}
