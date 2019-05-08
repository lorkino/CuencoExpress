using System;
using System.Collections.Generic;
using Film.Models;
using Film.TypeOf;
using Newtonsoft.Json;

namespace Film.ViewModels
{
    public class ViewUserDates
    {


        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public double Score { get; set; } = 0;
        [JsonConverter(typeof(Base64FileJsonConverter))]
        public byte[] ProfileImg { get; set; }
        public string PersonalInfo { get; set; }


        public static explicit operator ViewUserDates(UserDates v)
        {
            ViewUserDates userDates = new ViewUserDates
            {
                Address1 = v.Address1,
                Address2 = v.Address2,
                City = v.City,
                Country = v.Country,
                Name = v.Name,
                PostalCode = v.PostalCode,
                State = v.State,
                Surname = v.Surname,
                Phone = v.Phone,
                Score = v.Score,
                ProfileImg = v.ProfileImg,
                PersonalInfo = v.PersonalInfo              
                             
            };
            return userDates;
        }

    }
}