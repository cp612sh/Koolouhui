using Koo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Koo.Web.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /Cart/
        public ActionResult Index()
        {
            List<CartItem> items = new List<CartItem>();

            if (HttpContext.Session["CurrentCart"] != null)
            {
                items = (List<CartItem>)HttpContext.Session["CurrentCart"];
            }

            foreach (var item in items)
            {
                item.Project = db.Projects.Find(item.ProjectId);
            }

            return View(items);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Remove(string cartItemViewId, string Id, string amount)
        {
            if (ModelState.IsValid)
            {
                List<CartItem> cartItems = new List<CartItem>();
                int count = 0;
                float totalAmount = 0;

                try
                {
                    if (HttpContext.Session["CurrentCart"] != null)
                    {
                        cartItems = (List<CartItem>)HttpContext.Session["CurrentCart"];
                    }
                    CartItem tobeRemoved = cartItems.First(c => (c.ProjectId == int.Parse(Id) && c.Amount == amount));
                    if (tobeRemoved != null)
                    {
                        cartItems.Remove(tobeRemoved);
                        HttpContext.Session["CurrentCart"] = cartItems;
                        count = cartItems.Count;
                        foreach (var item in cartItems)
                        {
                            totalAmount += float.Parse(item.Amount);
                        }
                    }
                }
                catch (Exception)
                {
                    return Json(new
                    {
                        Success = false,
                        //Message = "ok",
                        //Count = count,
                        Amount = amount
                    });
                }

                return Json(new
                {
                    Success = true,
                    Message = "ok",
                    Count = count,
                    CartItemId = cartItemViewId,
                    Amount = amount,
                    TotalAmount = totalAmount
                });
            }
            return Json(new
            {
                Success = false,
                //Message = "ok",
                //Count = count,
                Amount = amount
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Add(string Id, string amount)
        {
            if (ModelState.IsValid)
            {

                bool hasError = false;
                List<CartItem> cartItems = new List<CartItem>();
                int count = 0;
                try
                {
                    if (HttpContext.Session["CurrentCart"] != null)
                    {
                        cartItems = (List<CartItem>)HttpContext.Session["CurrentCart"];
                    }

                    cartItems.Add(new CartItem { ProjectId = int.Parse(Id), Amount = amount });
                    count = cartItems.Count;
                    HttpContext.Session["CurrentCart"] = cartItems;
                }
                catch
                {
                    hasError = true;
                }

                if (hasError)
                {
                    return Json(new { Success = false });
                }

                return Json(new
                {
                    Success = true,
                    Message = "ok",
                    Count = count,
                    Amount = amount
                });
            }
            return Json(new { Success = false });
            
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