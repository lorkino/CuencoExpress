using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Film.Models
{
    public class UserDates
    {
        public string Id { get; set; }
        public string Direccion { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }
        [Range(1, 5)]
        public double  Puntuacion{ get; set; }
        [Required]
        public User User { get; set; }

    }
}
