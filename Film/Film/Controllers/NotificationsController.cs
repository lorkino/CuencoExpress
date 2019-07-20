using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Film.Models;
using Film.SignalR;
using Film.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebPush;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

namespace Film.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public static  VapidDetails _vapidDetails;
        private readonly IHubContext<NotificationsHub> _hubContext;
        // GET: Notifications
        public NotificationsController(ApplicationDbContext context, IHubContext<NotificationsHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
           
        }

      
        

        public static List<Suscription> Subscriptions { get; set; } = new List<Suscription>();


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public void Subscribe([FromBody] Suscription sub)
        {


            var userName = User.Identity.Name;
            User user = _context.Users.Where(a => a.Email == userName).Include(a => a.UserDates).FirstOrDefault();
            user.UserDates.Suscribed = true;
            user.Suscription = sub;
            _context.SaveChanges();

        }


        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpPost]
        public void Unsubscribe([FromBody] Suscription sub)
        {
            
                var userName = User.Identity.Name;
                User user = _context.Users.Where(a => a.Email == userName).Include(a => a.UserDates).FirstOrDefault();
                user.UserDates.Suscribed = false;
                user.Suscription = null;
                _context.SaveChanges();
            
        }

       
        public  static void Broadcast( NotificationModel message, List<User> Users)
        {
            var client = new WebPushClient();
            var serializedMessage = JsonConvert.SerializeObject(message);
            List<Suscription> UserSuscriptions = Users.Where(a => a.UserDates.Suscribed == true).Select(a=>a.Suscription).ToList();
            foreach (Suscription pushSubscription in UserSuscriptions)
            {
                client.SendNotification(pushSubscription, serializedMessage, _vapidDetails);
            }

        }

        public  async Task AddNotifications(List<User> Users, int ?type, ApplicationDbContext context) {
            Notifications notification = new Notifications()
            {
                Type = 0
            };
            List<Notifications> notifications = new List<Notifications>
            {
                notification
            };
            foreach (var users in Users)
            {
                users.Notifications = notifications;
                if(NotificationsHub.UsersConnected.ContainsKey(users.Email))
                 await _hubContext.Clients.Client(NotificationsHub.UsersConnected[users.Email]).SendAsync("ReceiveMessage", "juasjas");
            }
            context.SaveChanges();
        }

        







    }
}