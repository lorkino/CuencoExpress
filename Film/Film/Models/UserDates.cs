using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Film;
using Film.TypeOf;
using Nest;

namespace Film.Models
{
    //[NotMapped]
    //public class GeoLocation {
    //    public double Lat { get; set; }
    //    public double Lon { get; set; }
    //    public GeoLocation(double Latitud, double Longitud)
    //    {
    //        Lat = Latitud;
    //        Lon = Longitud;
    //    }
    //}
    public class UserDates
    {
         public UserDates() {
            Location = new GeoLocation(Lat,Lon);
         }
        [ForeignKey("User")]
        public string Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Surname { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Range(0, 5)]
        public double Score { get; set; } = 0;

        
        [NotMapped]
        public GeoLocation Location { get; set; }

        public double Lat { get; set; }
        public double Lon { get; set; }

        [JsonConverter(typeof(Base64FileJsonConverter))]
        public byte[] ProfileImg { get; set; }

        public bool Suscribed { get; set; } = true;
        public string PersonalInfo { get; set; }
        public virtual User User { get; set; }

    }
}
