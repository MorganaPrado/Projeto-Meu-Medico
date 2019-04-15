using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projeto.DAO;
using Projeto.Models;

namespace Projeto.Controllers
{
    public class MedicosController : Controller
    {
        private EFContext db = new EFContext();

        // GET: Medicos
        public ActionResult Index()
        {
            var medicos = db.Medicos.Include(m => m.Cidade).Include(m => m.Especialidade);
            return View(medicos.ToList());
        }

        // GET: Medicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medicos.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // GET: Medicos/Create
        public ActionResult Create()
        {
            ViewBag.CidadeId = new SelectList(db.Cidades, "CidadeId", "Nome");
            ViewBag.EspecialidadeId = new SelectList(db.Especialidades, "EspecialidadeId", "Nome");
            return View();
        }

        // POST: Medicos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MedicoId,Nome,Endereco,Telefone,EspecialidadeId,CidadeId")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Medicos.Add(medico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CidadeId = new SelectList(db.Cidades, "CidadeId", "Nome", medico.CidadeId);
            ViewBag.EspecialidadeId = new SelectList(db.Especialidades, "EspecialidadeId", "Nome", medico.EspecialidadeId);
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medicos.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            ViewBag.CidadeId = new SelectList(db.Cidades, "CidadeId", "Nome", medico.CidadeId);
            ViewBag.EspecialidadeId = new SelectList(db.Especialidades, "EspecialidadeId", "Nome", medico.EspecialidadeId);
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MedicoId,Nome,Endereco,Telefone,EspecialidadeId,CidadeId")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CidadeId = new SelectList(db.Cidades, "CidadeId", "Nome", medico.CidadeId);
            ViewBag.EspecialidadeId = new SelectList(db.Especialidades, "EspecialidadeId", "Nome", medico.EspecialidadeId);
            return View(medico);
        }

        // GET: Medicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medicos.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medico medico = db.Medicos.Find(id);
            db.Medicos.Remove(medico);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Busca()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Busca(string texto)
        {
            var consulta = (from C in db.Medicos
                            where  C.Nome.Contains(texto)
                            orderby C.MedicoId ascending
                            select C);

            //var medicos = db.Medicos.Select(p => new { N = p.Nome }).OrderBy(p => p.N);
            var medicos = db.Medicos.Include(m => m.Cidade).Include(m => m.Especialidade);
            return View(consulta.ToList());
            //return View(medicos.ToList());
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
