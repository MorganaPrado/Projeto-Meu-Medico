using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.Models
{
    public class Especialidade
    {
        public int EspecialidadeId { get; set; }

        [Display(Name = "Especialidade")]
        public string Nome { get; set; }

    }
}