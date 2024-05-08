using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Producer : BaseModel
    {

        public ICollection<Serie>? Series { get; set; }

    }
}
