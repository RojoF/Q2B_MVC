using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Q2B_MVC.Models;

namespace Q2B_MVC.Controllers
{
    public class HomeController : Controller
    {
        private PRUEBA_Q2BEntities db = new PRUEBA_Q2BEntities();
        public ActionResult Index()
        {
            // Listar cards de tabla Imagenes
            var imagen = db.Imagenes.SqlQuery("SELECT * FROM dbo.Imagenes").ToList();
            return View(imagen);
        }
    }
}