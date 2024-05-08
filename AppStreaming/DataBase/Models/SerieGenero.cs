using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class SerieGenero
    {
        public int SerieId { get; set; }
        public int GeneroId { get; set; }
        public string Tipo { get; set; }
        public Serie? Serie { get; set; }
        public Genre? Genero { get; set; } 
    }
}
