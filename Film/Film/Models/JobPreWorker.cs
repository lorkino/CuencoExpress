using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Film.Models
{
    public class JobPreWorker
    {
        public string JobId { get; set; }
        public Job Job { get; set; }
        public string UserPreWorkeId { get; set; }
    
        public User UserPreWorker { get; set; }
      

       

        
    }
}
