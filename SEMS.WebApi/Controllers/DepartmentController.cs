using SEMS.Abstracts;
using SEMS.DataAccess.Dto.Base;
using SEMS.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SEMS.WebApi.Controllers
{
    [RoutePrefix("api/Department")]
    public class DepartmentController : BaseController
    {
        private IDepartmentSvc _departmentSvc { get; set; }

        public DepartmentController(IDepartmentSvc _departmentSvc)
        {
            this._departmentSvc = _departmentSvc;
        }

        public IHttpActionResult Post([FromBody] DepartmentDto dto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            this._departmentSvc.CreatDepartment(dto);
            return Ok();
        }

        public IHttpActionResult Get([FromUri]DepartmentQuery query)
        {
            var result = _departmentSvc.GetDepartmentPage(query);
            return Ok(result);
        }
    }
}
