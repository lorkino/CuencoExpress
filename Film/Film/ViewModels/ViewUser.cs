using Film.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Film.ViewModels
{
    public class ViewUser
    {
        public virtual ViewUserDates UserDates { get; set; }
        public bool Admin { get; set; }
        public bool Status { get; set; }
        [DefaultValue(false)]
        public bool RememberMe { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public int AccessFailedCount { get; set; }
        public List<ViewKnowledge> Knowledges { get; set; }

        public static implicit operator ViewUser(User v)
        {
            if (v == null)
                return null;
            ViewUser userDates = new ViewUser
                {
                    Admin = v.Admin,
                    Status = v.Status,
                    RememberMe = v.RememberMe,
                    Password = v.Password,
                    Token = v.Token,
                    TokenExpiration = v.TokenExpiration,
                    Email = v.Email,
                    EmailConfirmed = v.EmailConfirmed,
                    AccessFailedCount = v.AccessFailedCount,
                    UserDates = (ViewUserDates)v?.UserDates,
                    Knowledges = v.UserKnowledges?.Select(a => (ViewKnowledge)a.Knowledges)?.ToList()

            };
             return userDates;
                 

        }

    }
    
}
