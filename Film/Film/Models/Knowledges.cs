using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Film.Models
{
    public class Knowledges
    {
      

        public string Id { get; set; }
        public string Value { get; set; }
        public string Explanation { get; set; }
        public List<UserKnowledges> UserKnowledges { get; set; }
        public List<JobKnowledges> JobKnowledges { get; set; }
    }
}
