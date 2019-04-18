using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.ViewsModels
{
    public class AlterarSenhaViewsModels
    {
        [Required(ErrorMessage = "Informe senha atual")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        public String SenhaAtual { get; set; }

        [Required(ErrorMessage = "Informe nova senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public String NovaSenha { get; set; }

        [Required(ErrorMessage = "Repita a nova senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Confime a senha")]
        [Compare(nameof(NovaSenha), ErrorMessage = "As senhas nao confere")]
        public String ConfirmacaoSenha { get; set; }
    }
}