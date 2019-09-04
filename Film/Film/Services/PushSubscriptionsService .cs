using Film.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
namespace Film.Services
{
    internal class PushSubscriptionsService :  IDisposable
    {
        private class LitePushSubscription : Notifications
        {
            public int Id { get; set; }

            public LitePushSubscription()
            { }

        }

        private readonly LiteDatabase _db;
        private readonly LiteCollection<LitePushSubscription> _collection;

        public PushSubscriptionsService()
        {
            _db = new LiteDatabase("PushSubscriptionsStore.db");
            _collection = _db.GetCollection<LitePushSubscription>("subscriptions");
        }

        public IEnumerable<Notifications> GetAll()
        {
            return _collection.FindAll();
        }


        //public void Delete(string endpoint)
        //{
        //    _collection.Delete(subscription => subscription.Endpoint == endpoint);
        //}

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
