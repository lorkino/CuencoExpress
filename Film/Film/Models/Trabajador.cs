using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Film.Models
{
    public class Trabajador
    {
        public string Id { get; set; }
        public User User { get; set; }
        public Trabajo Trabajo { get; set; }
        
    }
}
