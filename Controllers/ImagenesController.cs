using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Q2B_MVC.Models;

namespace Q2B_MVC.Controllers
{
    public class ImagenesController : Controller
    {
        private PRUEBA_Q2BEntities db = new PRUEBA_Q2BEntities();

        // GET: Imagenes
        public ActionResult Index()
        {
            return View(db.Imagenes.ToList());
        }

        // GET: Imagenes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imagenes imagenes = db.Imagenes.Find(id);
            if (imagenes == null)
            {
                return HttpNotFound();
            }
            return View(imagenes);
        }

        // GET: Imagenes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Imagenes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Descripcion,UrlImagen")] Imagenes imagenes)
        {
            if (ModelState.IsValid)
            {
                db.Imagenes.Add(imagenes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(imagenes);
        }

        // GET: Imagenes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imagenes imagenes = db.Imagenes.Find(id);
            if (imagenes == null)
            {
                return HttpNotFound();
            }
            return View(imagenes);
        }

        // POST: Imagenes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Descripcion,UrlImagen")] Imagenes imagenes)
        {


            if (ModelState.IsValid)
            {
                db.Entry(imagenes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(imagenes);
        }

        // GET: Imagenes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imagenes imagenes = db.Imagenes.Find(id);
            if (imagenes == null)
            {
                return HttpNotFound();
            }
            return View(imagenes);
        }

        // POST: Imagenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Imagenes imagenes = db.Imagenes.Find(id);
            db.Imagenes.Remove(imagenes);
            db.SaveChanges();
            return RedirectToAction("Index");
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
