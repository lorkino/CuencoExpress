using Film.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Film.ViewModels
{
    public class ViewNotification
    {
        public short Type ;
        public DateTime CreatedDate;
        public bool Readed;

        public static explicit operator ViewNotification(Notifications v)
        {
            if (v == null)
                return null;
            ViewNotification notification = new ViewNotification
            {
                Type = v.Type,
                Readed = v.Readed,
                CreatedDate = v.CreatedDate,
                
            };
            return notification;

        }
    }
}
