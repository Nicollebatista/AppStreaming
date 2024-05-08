using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class SaveSerieViewModel
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "Debe colocar el nombre de la serie")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Debe colocar la Description")]

        public string Description { get; set; }
        [Required(ErrorMessage = "Debe colocar el la imagen de la serie")]

        public string ImagenPort { get; set; }
        [Required(ErrorMessage = "Debe colocar el video de la serie")]

        public string VideoYout { get; set; }
        [Required(ErrorMessage = "Debe elegir la productora")]

        public int ProductoraId { get; set; }
        [Required(ErrorMessage = "Debe elegir el genero primario")]

        public int? PrimaryGenreId { get; set; }
        [Required(ErrorMessage = "Debe elegir el/los genero secundario")]

        public List<int>? SecondaryGenreIds { get; set; }
        public List<GenreViewModel> Generos { get; set; } = new List<GenreViewModel>();

        public List<ProducerViewModel> Productoras { get; set; } = new List<ProducerViewModel>();

    }
}
