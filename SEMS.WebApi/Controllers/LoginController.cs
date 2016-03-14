using SEMS.DataAccess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SEMS.WebApi.Controllers
{
    public class LoginController : BaseController
    {
        public IHttpActionResult Post([FromBody] LoginDto dto)
        {
            return Ok();
        }
    }
}
