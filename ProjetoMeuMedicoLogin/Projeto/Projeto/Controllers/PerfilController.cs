using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projeto.Utils;
using Projeto.ViewsModels;
using System.Web.Mvc;
using System.Security.Claims;
using Projeto.DAO;

namespace Projeto.Controllers
{
    public class PerfilController : Controller
    {
        private EFContext db = new EFContext();
        // GET: Perfil

        [Authorize]
        public ActionResult AlterarSenha()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenhaViewsModels viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identity = User.Identity as ClaimsIdentity;
            var email = identity.Claims.FirstOrDefault(
                c => c.Type == "Email").Value;
            var cliente = db.Clientes.FirstOrDefault(u => u.Email == email);
            if (Hash.GerarHash(viewmodel.SenhaAtual) != cliente.Senha)
            {
                ModelState.AddModelError("SenhaAtual", "Senha incorreta");
                return View();
            }

            cliente.Senha = Hash.GerarHash(viewmodel.NovaSenha);
            db.Entry(cliente).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Adm", "Home");
        }
    }
}