using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Genre : BaseModel
    {

        public ICollection<SerieGenero>? SerieGeneros { get; set; }
    }
}
