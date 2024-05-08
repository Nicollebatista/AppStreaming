using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Serie : BaseModel
    {
    
        public string Description { get; set; }
        public string ImagenPort { get; set; }
        public string VideoYout { get; set; }
        public int ProductoraId { get; set; } //Fk Productora
        public Producer? Producer { get; set; }
        public ICollection<SerieGenero>? SerieGeneros { get; set; }


    }
}
