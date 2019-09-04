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
        public virtual ViewUser UserCreator { get; set; }
        public virtual ViewUser UserWorker { get; set; }
       // public virtual List<ViewUser> UserPreWorker { get; set; }
        //0:sin PreWork ,1:Con Prework, 2: Con UserWorker, 3: Finalizado
        public short Status { get; set; }
        public string Description { get; set; }
        public string Tittle { get; set; }
        public List<ViewKnowledge> Knowledges { get; set; }
        [JsonConverter(typeof(Base64FileJsonConverter))]
        public List<Images> JobImages { get; set; }
        public DateTime CreatedDate { get; set; }
        public static explicit operator ViewJob(Job v)
        {
           
            if (v == null)
                return null;
            ViewJob job = new ViewJob
            {
                Id = v.Id,
                Status = v.Status,
                Description = v.Description,
                Tittle = v.Tittle,
                JobImages = v.JobImages,
                CreatedDate = v.CreatedDate,
                UserCreator = v.UserCreator,
                UserWorker = v.UserWorker,
                //UserPreWorker = b,
                Knowledges = v.JobKnowledges?.Select(a => (ViewKnowledge)a.Knowledges)?.ToList()

            };
            return job;


        }
    }
}
