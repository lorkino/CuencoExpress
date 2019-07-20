using EntityFrameworkCore.Triggers;
using Film.Controllers;
using Film.Models;
using Film.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Film.Triggers
{
    public class NotificationsTrigger
    {
        static NotificationsTrigger()
        {
            //Triggers<Notifications>.Inserted += async e => {
            //    NotificationsHub NH = new NotificationsHub();
            //    await NH.SendMessageUser("yeyeye",e.Entity.User.Email);
            //   } ;
       
            //Triggers<Notifications>.Updated += e => Console.WriteLine("Updated " + e.Entity.FirstName);
            //Triggers<Notifications>.Deleted += e => Console.WriteLine("Deleted " + e.Entity.FirstName);
        }
    }
}
