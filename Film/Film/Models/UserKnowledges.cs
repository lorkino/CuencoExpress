using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Film.Models
{
    public class UserKnowledges
    {
        public string KnowledgesId { get; set; }
        public Knowledges Knowledges { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
