using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.ViewsModels
{
    public class CadastroViewsModels
    {
        [Required(ErrorMessage = "Informe o nome")]
        [MaxLength(100, ErrorMessage = "Maximo 100")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        [MaxLength(100, ErrorMessage = "Maximo 100")]
        public String Email { get; set; }

        [MaxLength(100, ErrorMessage = "Maximo 100")]
        [MinLength(6, ErrorMessage = "Minimo 6 caracteres")]
        [Required(ErrorMessage = "Informe o nome")]
        [DataType(DataType.Password)]
        public String Senha { get; set; }

        [Required(ErrorMessage = "Confirme sua senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [MinLength(6, ErrorMessage = "Maximo 6 caracteres")]
        [Compare(nameof(Senha), ErrorMessage = "A senha e a confirmaçao nao batem")]
        public String ConfirmacaoSenha { get; set; }
    }
}