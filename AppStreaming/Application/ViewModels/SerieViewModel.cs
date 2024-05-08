using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class SerieViewModel
    {
        public virtual int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string ImagenPort { get; set; }
        public string VideoYout { get; set; }
        public int ProductoraId { get; set; }
        public string ProductoraName { get; set; }
        public List<SerieGenreViewModel> Generos { get; set; } = new List<SerieGenreViewModel>();
        public List<SaveGenreViewModel>? nombresgeneros { get; set; } = new List<SaveGenreViewModel>();
        

    }
}
