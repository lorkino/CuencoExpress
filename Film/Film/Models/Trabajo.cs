using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Film.Models
{
    public enum Estado { Pendiente, Aceptado, EnProgreso, Finalizado, Rechazado };
    public class Trabajo
    {
        public string Id { get; set; }
       
        public double Puntuacion { get; set; }
        [Required]
        public Estado Estado  { get; set; }  
        public Cliente Cliente { get; set; }
        public Cliente Trabajador { get; set; }

    }
}
