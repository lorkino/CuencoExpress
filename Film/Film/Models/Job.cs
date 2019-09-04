using Film.TypeOf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Film.Models
{
    public class Job
    {
       

        
        public string Id { get; set; }
        public virtual User UserCreator { get; set; }
        public virtual User UserWorker { get; set; }
        // public virtual List<JobPreWorker> UserPreWorker { get; set; }

        public virtual List <JobPreWorker> UserPreWorker { get; set; }

        //0:sin PreWork ,1:Con Prework, 2: Con UserWorker, 3: Finalizado
        public short Status { get; set; }

        public string Description { get; set; }
        public string Tittle { get; set; }
        public List<JobKnowledges> JobKnowledges { get; set; }
      //  [JsonConverter(typeof(Base64FileJsonConverter))]
        public List<Images> JobImages { get; set; }
        public DateTime CreatedDate { get; set; }
        public Job()
        {
            CreatedDate = DateTime.UtcNow;
            Status = 0;
        }
    }
}
