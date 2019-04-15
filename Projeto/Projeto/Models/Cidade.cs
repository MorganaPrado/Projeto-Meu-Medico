using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.Models
{
    public class Cidade
    {
        public int CidadeId { get; set; }

        [Display(Name = "Cidade")]
        public string Nome { get; set; }
    }
}