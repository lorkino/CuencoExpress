using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace Film.Models
{
    public class JobKnowledges
    {
       
        public string KnowledgesId { get; set; }
        public Knowledges Knowledges { get; set; }
        
        public string JobId { get; set; }
        public Job Job { get; set; }
    }
}