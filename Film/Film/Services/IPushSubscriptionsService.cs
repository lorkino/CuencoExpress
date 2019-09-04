using Film.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Film.Services
{
    public  interface IPushSubscriptionsService
    {
        IEnumerable<Notifications> GetAll();

        void Insert(Notifications subscription);

        void Delete(string endpoint);
    }
}
