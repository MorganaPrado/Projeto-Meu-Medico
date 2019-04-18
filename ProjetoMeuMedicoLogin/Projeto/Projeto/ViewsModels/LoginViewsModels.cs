using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.ViewsModels
{
    public class LoginViewsModels
    {
        [HiddenInput]
        public String UrlRetorno { get; set; }
        [Required(ErrorMessage = "Informe o Email ")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "No minimo 6 caracteres")]
        public String Senha { get; set; }
    }
}