﻿using SEMS.Abstracts;
using SEMS.WebSite.App_Start;
using SEMS.WebSite.Filters;
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
        private IDepartmentSvc _departmentSvc { get; set; }
        
        public CompanyController(ICompanySvc companySvc, IDepartmentSvc departmentSvc)
        {
            _companySvc = companySvc;
            _departmentSvc = departmentSvc;
        }
        #endregion
        // GET: Company
        public ActionResult Index()
        {
           
            _companySvc.CreatCompany();
            // _departmentSvc.CreatDepartment();

            ModelState.AddModelError("id", "不允许为空");
           
            return View();
        }

        public ActionResult Form()
        {
            return View();
        }
    }
}