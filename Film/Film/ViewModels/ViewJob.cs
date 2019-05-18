using Film.Models;
using Film.TypeOf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Film.ViewModels
{
    public class ViewJob
    {

        public string Id { get; set; }
        public virtual User UserCreator { get; set; }
        public virtual User UserWorker { get; set; }
        public virtual List<User> UserPreWorker { get; set; }
        //0:sin PreWork ,1:Con Prework, 2: Con UserWorker, 3: Finalizado
        public short Status { get; set; }
        public string Description { get; set; }
        public string Tittle { get; set; }
        public List<ViewKnowledge> Knowledges { get; set; }
        [JsonConverter(typeof(Base64FileJsonConverter))]
        public byte[] JobImages { get; set; }
        public DateTime CreatedDate { get; set; }
        public static implicit operator ViewJob(Job v)
        {

            ViewJob job = new ViewJob
            {
                Id = v.Id,
                Status = v.Status,
                Description = v.Description,
                Tittle = v.Tittle,
                JobImages = v.JobImages,             
                Knowledges = v.JobKnowledges.Select(a => (ViewKnowledge)a.Knowledges)?.ToList()

            };
            return job;


        }
    }
}
