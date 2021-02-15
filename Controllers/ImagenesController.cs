using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Spatial;
using System.Data.SqlClient;
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
            var imagen = db.Imagenes.SqlQuery(
                "SELECT * FROM dbo.Imagenes").ToList();
            return View(imagen);
        }

        // GET: Imagenes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var imagen = db.Imagenes.SqlQuery(
                "SELECT * FROM dbo.Imagenes where Id ={0}", id).FirstOrDefault();

            if (imagen == null)
            {
                return HttpNotFound();
            }
            return View(imagen);
        }

        // GET: Imagenes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Imagenes/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Descripcion,UrlImagen")] FormCollection values, Imagenes imagenes)
        {
            var id = values["Id"];
            var titulo = values["Titulo"];
            var descripcion = values["Descripcion"];
            var urlImagen = values["UrlImagen"];

            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlCommand(
                                    "INSERT INTO dbo.Imagenes(Id, Titulo, Descripcion, UrlImagen) " +
                                    "VALUES(@Id,@Titulo,@Descripcion,@UrlImagen)",
                                    new SqlParameter("@Id", id),
                                    new SqlParameter("@Titulo", titulo),
                                    new SqlParameter("@Descripcion", descripcion),
                                    new SqlParameter("@UrlImagen", urlImagen)
                                    );


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
            var imagen = db.Imagenes.SqlQuery(
                "SELECT * FROM dbo.Imagenes where Id ={0}", id).FirstOrDefault();
            if (imagen == null)
            {
                return HttpNotFound();
            }
            return View(imagen);
        }

        // POST: Imagenes/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Descripcion,UrlImagen")] Imagenes imagenes, FormCollection values, int? id)
        {

            var titulo = values["Titulo"];
            var descripcion = values["Descripcion"];
            var urlImagen = values["UrlImagen"];

            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlCommand(
                    "UPDATE dbo.Imagenes " +
                    "SET Titulo = @Titulo, Descripcion = @Descripcion, UrlImagen = @UrlImagen " +
                    "WHERE Id = @Id",
                   new SqlParameter("@Id", id),
                   new SqlParameter("@Titulo", titulo),
                   new SqlParameter("@Descripcion", descripcion),
                   new SqlParameter("@UrlImagen", urlImagen));
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
            var imagen = db.Imagenes.SqlQuery(
               "SELECT * FROM dbo.Imagenes where Id ={0}", id).FirstOrDefault();
            if (imagen == null)
            {
                return HttpNotFound();
            }
            return View(imagen);

        }

        // POST: Imagenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]


        public ActionResult DeleteConfirmed(int? id)
        {
            if (id != null)
            {
                //Eliminar card seleccionada
                db.Database.ExecuteSqlCommand("DELETE from dbo.Imagenes WHERE Id = @Id",
                    new SqlParameter("@Id", id));

            }
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


