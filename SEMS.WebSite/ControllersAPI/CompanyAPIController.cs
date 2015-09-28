using SEMS.Abstracts;
using SEMS.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SEMS.WebSite.ControllersAPI
{
    public class CompanyAPIController : ApiController
    {
        #region 获取或设置 身份认证业务对象
        private ICompanySvc _companySvc { get; set; }
        private IDepartmentSvc _departmentSvc { get; set; }

        public CompanyAPIController(ICompanySvc companySvc, IDepartmentSvc departmentSvc)
        {
            _companySvc = companySvc;
            _departmentSvc = departmentSvc;
        }
        #endregion

        public IHttpActionResult Get()
        {
            return Ok();
        }
        public IHttpActionResult Post([FromBody] CompanyVM vm)
        {
           // _companySvc.CreatCompany();
            //if (vm.Id != 222)
            //{
            //    ModelState.AddModelError("CompanyName", "公司名称不允许为空");
            //    return BadRequest(ModelState);
            //}
            return Ok();
        }
    }
}
