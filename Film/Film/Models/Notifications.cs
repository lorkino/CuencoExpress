using EntityFrameworkCore.Triggers;
using Film.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Film.Models
{
    public class Notifications
    {


        private Notifications(IHubContext<NotificationsHub> context)
        {
            HubContext = context;
        }
        public string Id { get; set; }
        //0:new offer, 1:new message
        public short Type { get; set; }
        public bool Readed { get; set; } = false;
        public User User { get; set; }
        public DateTime CreatedDate { get; set; }
        public Notifications()
        {
            CreatedDate = DateTime.UtcNow;
            Type = 0;
        }
        [NotMapped]
        public static IHubContext<NotificationsHub> HubContext;
        static Notifications() {
            

            Triggers<Notifications>.Inserted += async e => {
                //NotificationsHub NH = new NotificationsHub();
                //var hubContext = context.RequestServices
                //                       .GetRequiredService<IHubContext<NotificationsHub>>();
                //var a = Startup.hubContext.Clients;
                //var b = _context;
               // var hub = app.ApplicationServices.GetRequiredService<IHubContext<UserInterfaceHub>>();

                //if (NotificationsHub.UsersConnected.ContainsKey(e.Entity.User.Email))
                   // await HubContext.Clients.Client(NotificationsHub.UsersConnected[e.Entity.User.Email]).SendAsync("ReceiveMessage", "asdasdsad");
               // await NH.SendMessageUser("yeyeye", e.Entity.User.Email);
            };
        }

    }
}
