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

namespace Film.Models
{
    public class UserDates
    {
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
        
        [JsonConverter(typeof(Base64FileJsonConverter))]
        public byte[] ProfileImg { get; set; }

        [NotMapped]
        public string ProfileImgString { get; set; }
        public string PersonalInfo { get; set; }
        public virtual User User { get; set; }

    }
}
