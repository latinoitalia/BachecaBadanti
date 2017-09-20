using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BachecaBadanti;

namespace BachecaBadanti.Controllers
{
    public class AnnuncisController : Controller
    {
        private bakecaEntities db = new bakecaEntities();

        // GET: Annuncis
        public async Task<ActionResult> Index(string selezione)
        {

            var query = await (from p in db.Annunci
                               where p.Phone != null && p.City.Contains(selezione)
                               orderby p.DataOra descending
                               select p).Take(200).ToListAsync();

            ViewBag.Total = query.Count();

            //return View(await db.Annunci.OrderByDescending(p => p.DataOra).ToListAsync());
            return View(query);
        }

       




        // GET: Annuncis/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annunci annunci = await db.Annunci.FindAsync(id);
            if (annunci == null)
            {
                return HttpNotFound();
            }
            return View(annunci);
        }

        // GET: Annuncis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Annuncis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Titolo,Descrizione,DataOra,Location,City,Photo,Link,Phone,Email")] Annunci annunci)
        {
            if (ModelState.IsValid)
            {
                db.Annunci.Add(annunci);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(annunci);
        }

        // GET: Annuncis/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annunci annunci = await db.Annunci.FindAsync(id);
            if (annunci == null)
            {
                return HttpNotFound();
            }
            return View(annunci);
        }

        // POST: Annuncis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Titolo,Descrizione,DataOra,Location,City,Photo,Link,Phone,Email")] Annunci annunci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(annunci).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(annunci);
        }

        // GET: Annuncis/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annunci annunci = await db.Annunci.FindAsync(id);
            if (annunci == null)
            {
                return HttpNotFound();
            }
            return View(annunci);
        }

        // POST: Annuncis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Annunci annunci = await db.Annunci.FindAsync(id);
            db.Annunci.Remove(annunci);
            await db.SaveChangesAsync();
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
