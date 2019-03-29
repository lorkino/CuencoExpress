using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Film.Models
{
    public class Job
    {
        [ForeignKey("User")]
        public string Id { get; set; }
        public virtual User UserCreator { get; set; }
        public virtual User UserWorker { get; set; }
        public string Description { get; set; }
        public string Tittle { get; set; }
        public List<JobKnowledges> JobKnowledges { get; set; }
        public List<Images> JobImages { get; set; }
        public DateTime CreatedDate { get; set; }
        public Job()
        {
            CreatedDate = DateTime.UtcNow;          
        }
    }
}
