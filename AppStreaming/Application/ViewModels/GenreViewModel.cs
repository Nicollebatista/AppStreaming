﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class GenreViewModel
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre del Genero")]
        public string Name { get; set; }
    }
}
