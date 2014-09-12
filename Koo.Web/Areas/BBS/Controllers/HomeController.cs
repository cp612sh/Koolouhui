using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Koo.Web.Areas.BBS.Models;
using Koo.Web.Models;
using Koo.Web.UI.Controls;
namespace Koo.Web.BBS.Controllers

{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public static int defaultPageSize = 2;

        //// 传入 page 参数 ( 透过 Model Binder 绑进来的 )     
        //public ActionResult Index(int? page)
        //{
        //    IQueryable<Post> posts =
        //        from post in db.Posts
        //        select post;

        //    // 计算出目前要显示第几页的资料 ( 因为 page 为 Nullable<int> 型别 )     
        //    int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

        //    // 透过 ToPagedList 这个 Extension Method 将原本的资料转成 IPagedList<T>     
        //    return View(posts.ToPagedList(currentPageIndex, defaultPageSize));
        //}    

        public ActionResult Index(int pageIndex = 1)
        {
            var model = db.Posts
                .OrderByDescending(a => a.CreateDate)
                .ToPagedList(pageIndex, defaultPageSize);
            return View(model);
        }


        //// GET: BBS/Home
        //public ActionResult Index()
        //{
        //    return View(db.Posts.ToList());
        //}

        // GET: BBS/Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            else 
            {
                post.BrowseNum += 1;
                db.SaveChanges();
            }
            return View(post);
        }

        // GET: BBS/Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Browser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            else
            {
                post.BrowseNum += 1;
                db.SaveChanges();
            }
            return View(post);
        }

        // POST: BBS/Posts/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.CreateDate = DateTime.Now;
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: BBS/Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: BBS/Posts/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: BBS/Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: BBS/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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
