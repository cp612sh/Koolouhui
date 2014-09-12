using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Koo.Web.Models;

namespace Koo.Web
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Seeding()
        {
            return View();
        }

        public ActionResult Search(string keyWord)
        {

            ///it should invoke the search instance in the BLL
            ///currently considering the data volume is less, just use LINQ to make it work.
            ///
            var results = db.Projects.Where(p => p.Title.Contains(keyWord)
                || p.ShortDescription.Contains(keyWord)
                || p.Description.Contains(keyWord)
                );
            return View(@"index", results);
        }

        // GET: /Default1/
        public ActionResult Index()
        {
            return View(db.Projects.Take(10).ToList());
        }

        // GET: /Default1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            project.SupportAmounts = db.SupportAmounts.Where(s => s.ProjectId == project.Id).ToList();

            //project.SupportedInfo = db.OrderItems.Where(oi => (oi.SupportProject.ProjectId == project.Id)).ToList();
            
            return View(project);
        }
        [Authorize]
        // GET: /Default1/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admins")]
        // POST: /Default1/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ShortDescription,Description,CoverImageUrl,IsHighlighted,RatingValue,Status")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: /Default1/Edit/5
        [Authorize(Roles = "Admins")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Default1/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,ShortDescription,Description,CoverImageUrl,IsHighlighted,RatingValue,Status")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        [Authorize(Roles = "Admins")]

        // GET: /Default1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [Authorize(Roles = "Admins")]

        // POST: /Default1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
