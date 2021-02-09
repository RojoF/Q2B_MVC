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
            return View(db.Imagenes.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}