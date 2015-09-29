using SEMS.Abstracts;
using SEMS.DataAccess.Dto;
using SEMS.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SEMS.WebSite.ControllersAPI
{
    public class CompanyAPIController : BaseAPIController
    {
        #region 获取或设置 身份认证业务对象
        private ICompanySvc _companySvc { get; set; }
        public CompanyAPIController(ICompanySvc companySvc)
        {
            _companySvc = companySvc;
        }
        #endregion

        public IHttpActionResult Get(int id)
        {
            return Ok();
        }

        public IHttpActionResult Get([FromUri]CompanyQuery query)
        {
            var result = _companySvc.GetCompanyPage(query);
            return Ok(result);
        }

        public IHttpActionResult Post([FromBody] CompanyDto dto)
        {
            _companySvc.CreatCompany(dto);
            return Ok();
        }
    }
}
