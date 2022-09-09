using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcPelicula.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: HelloWorld
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Welcome(String nombre, String apellido, int numVeces = 1)
        {
            ViewBag.Mensaje = "Hola " + nombre +" "+apellido;
            ViewBag.NumVeces = numVeces;

            return View();
        }
    }
}