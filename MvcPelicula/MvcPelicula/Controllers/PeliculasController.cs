using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcPelicula.Models;

namespace MvcPelicula.Controllers
{
    public class PeliculasController : Controller
    {
        private PeliculaDBContext db = new PeliculaDBContext();

        public ActionResult Index(string buscarString, string generoPelicula,decimal precio = 0)
        {
            var GeneroLst = new List<string>();
            var GeneroQry = from d in db.Peliculas
                            orderby d.Genero
                            select d.Genero;
            GeneroLst.AddRange(GeneroQry.Distinct());
            ViewBag.generoPelicula = new SelectList(GeneroLst, "accion");
            var peliculas = from p in db.Peliculas
                            select p;
            if (!String.IsNullOrEmpty(buscarString))
            {
                peliculas = peliculas.Where(s => s.Titulo.Contains(buscarString));
            }
            if (!string.IsNullOrEmpty(generoPelicula))
            {
                peliculas = peliculas.Where(x => x.Genero == generoPelicula);
            }
            if(precio > 0)
            {
                peliculas = peliculas.Where(x=> x.Precio == precio);
            }
            return View(peliculas);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pelicula pelicula = db.Peliculas.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            return View(pelicula);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Titulo,FechaLanzamiento,Genero,Precio")] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                db.Peliculas.Add(pelicula);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pelicula);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pelicula pelicula = db.Peliculas.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            return View(pelicula);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Titulo,FechaLanzamiento,Genero,Precio")] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pelicula).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pelicula);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pelicula pelicula = db.Peliculas.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            return View(pelicula);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pelicula pelicula = db.Peliculas.Find(id);
            db.Peliculas.Remove(pelicula);
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
