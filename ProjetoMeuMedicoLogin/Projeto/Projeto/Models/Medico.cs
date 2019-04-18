using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projeto.Models
{
    public class Medico
    {
        public int MedicoId { get; set; }

        [Display(Name = "Médico")]
        public string Nome { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        public enum Sexo {M,F}
        public string Telefone { get; set; }
        public int EspecialidadeId { get; set; }
        [ForeignKey("EspecialidadeId")]
        public virtual Especialidade Especialidade { get; set; }
        public int CidadeId { get; set; }
        [ForeignKey("CidadeId")]
        public virtual Cidade Cidade { get; set; }


    }
}