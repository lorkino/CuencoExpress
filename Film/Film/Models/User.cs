using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Film.Models
{
    public class User : IdentityUser
    {
       
       
        public virtual UserDates UserDates { get; set; }
        public bool Admin { get; set; }
        public bool Status { get; set; }
        [NotMapped]
        [DefaultValue(false)]
        public bool RememberMe { get; set; }
        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string Token { get; set; }
        [NotMapped]
        public DateTime TokenExpiration { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public override string Email { get => base.Email; set => base.Email = value; }

        

        public List<UserKnowledges> UserKnowledges { get; set; }

    }
}
