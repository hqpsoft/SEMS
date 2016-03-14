using SEMS.SSOServer.Config.Config;
using SEMS.SSOServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEMS.SSOServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var name = User.Identity.Name;
            var appList = Clients.Get().Where(x => x.Enabled)
                                 .Select(r => new AppVM
                                 {
                                     Name = r.ClientName,
                                     Url = r.ClientUri,
                                     Description = r.Description
                                 }).ToList();
            ViewBag.AppList = appList;
            return View();
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

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}