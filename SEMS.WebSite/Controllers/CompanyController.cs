using SEMS.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEMS.WebSite.Controllers
{
    public class CompanyController : Controller
    {
        #region 获取或设置 身份认证业务对象
        private ICompanySvc _companySvc { get; set; }

        public CompanyController(ICompanySvc companySvc)
        {
            _companySvc = companySvc;
        }
        #endregion


        // GET: Company
        public ActionResult Index()
        {
            _companySvc.AddCompany();
            return View();
        }
        public ActionResult Form()
        {
            return View();
        }
    }
}