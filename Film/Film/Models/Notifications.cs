using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Film.Models
{
    public class Notifications
    {
        //0:new offer, 1:new message
        public short Type;

        public User User;
        public DateTime CreatedDate { get; set; }
        public Notifications()
        {
            CreatedDate = DateTime.UtcNow;
            Type = 0;
        }
    }
}
