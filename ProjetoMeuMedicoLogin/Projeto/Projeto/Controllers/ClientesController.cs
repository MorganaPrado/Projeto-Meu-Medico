using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projeto.DAO;
using Projeto.Utils;
using Projeto.Models;
using Projeto.ViewsModels;
using System.Security.Claims;

namespace Projeto.Controllers
{
    //[Authorize(Roles = "Administrador, Usuario")]
    public class ClientesController : Controller
    {
        private EFContext db = new EFContext();

        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.Clientes.ToList());
        }

        // GET: Clientes/Details/5 
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(CadastroViewsModels viewsModel)
        {
            //[Bind(Include = "ClienteId,Nome,Email,Senha")] Cliente cliente
            //if (ModelState.IsValid)
            //{
            //    db.Clientes.Add(cliente);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //return View(cliente);
            if (!ModelState.IsValid)
            {
                return View(viewsModel);
            }
            if (db.Clientes.Count(u => u.Email == viewsModel.Email) > 0)
            {
                ModelState.AddModelError("Email", "Esse login já esta em uso");
                return View(viewsModel);

            }
            Cliente novoUsuario = new Cliente
            {
                Nome = viewsModel.Nome,
                Email = viewsModel.Email,
                Senha = Hash.GerarHash(viewsModel.Senha)
            };

            db.Clientes.Add(novoUsuario);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClienteId,Nome,Email,Senha")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Login(string ReturnUrl)
        {
            var viewmodel = new LoginViewsModels
            {
                UrlRetorno = ReturnUrl
            };

            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewsModels viewmodel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }

            var usuario = db.Clientes.FirstOrDefault(m => m.Email == viewmodel.Email);

            if (usuario == null)
            {
                ModelState.AddModelError("Login", "Falha ao realizar o login, usuario ou senha incorreto");
                return View(viewmodel);
            }
            if (usuario.Senha != Hash.GerarHash(viewmodel.Senha))
            {
                ModelState.AddModelError("Senha", "Falha ao realizar o login, usuario ou senha incorreto");
                return View(viewmodel);
            }

            var identify = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim("Email", usuario.Email)
            }, "ApplicationCookie");

            Request.GetOwinContext().Authentication.SignIn(identify);

            if (!String.IsNullOrWhiteSpace(viewmodel.UrlRetorno))
            {
                Url.IsLocalUrl(viewmodel.UrlRetorno);
                return Redirect(viewmodel.UrlRetorno);
            }
            else
            {
                return RedirectToAction("Adm", "Home");
            }

        }

        public ActionResult Sair()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
