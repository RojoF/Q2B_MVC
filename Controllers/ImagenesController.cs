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


        public ActionResult Index()
        {
            //Listar cards de tabla Imagenes
            var imagen = db.Imagenes.SqlQuery(
                "SELECT * FROM dbo.Imagenes").ToList();
            return View(imagen);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Listar detalles de card seleccionada
            var imagen = db.Imagenes.SqlQuery(
                "SELECT * FROM dbo.Imagenes where Id ={0}", id).FirstOrDefault();

            if (imagen == null)
            {
                return HttpNotFound();
            }
            return View(imagen);
        }


        public ActionResult Create()
        {
            return View();
        }

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
                //Crear nueva card en la tabla imagenes
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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Descripcion,UrlImagen")] Imagenes imagenes, FormCollection values, int? id)
        {

            var titulo = values["Titulo"];
            var descripcion = values["Descripcion"];
            var urlImagen = values["UrlImagen"];

            if (ModelState.IsValid)
            {
                // Editar Card seleccionada en tabla
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


