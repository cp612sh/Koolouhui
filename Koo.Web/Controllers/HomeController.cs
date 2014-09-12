using Koo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Koo.Web.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        /// <summary>
        /// 提供推荐项目。
        /// 目前不用。
        /// </summary>
        /// <returns></returns>
        public ActionResult OnFireProjects()
        {
            var projects = db.Projects.Where<Project>(p => p.IsHighlighted == true);
            if (projects.Count() > 0)
            {
                return (ActionResult)PartialView("_OnFireProjects", projects);
            }
            return View();

        }

        public ActionResult Index()
        {
            return View();
        }
    }
}