using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEMS.WebSite.Controllers
{
    public class DepartmentController : BaseController
    {
        //
        // GET: /Department/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form()
        {
            return View();
        }
	}
}