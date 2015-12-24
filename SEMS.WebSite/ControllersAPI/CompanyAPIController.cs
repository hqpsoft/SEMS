using SEMS.Abstracts;
using SEMS.DataAccess.Dto;
using SEMS.DataAccess.Dto.Base;
using SEMS.DataAccess.Query;
using SEMS.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SEMS.WebSite.ControllersAPI
{
    /// <summary>
    /// 公司管理API接口
    /// </summary>
    [RoutePrefix("api/CompanyAPI")]
    public class CompanyAPIController : BaseAPIController
    {
        #region 获取或设置 身份认证业务对象
        private ICompanySvc _companySvc { get; set; }
        public CompanyAPIController(ICompanySvc companySvc)
        {
            _companySvc = companySvc;
        }
        private static readonly ILogger _logger = LogManager.GetLogger(typeof(CompanyAPIController));
        #endregion

        public IHttpActionResult Get(int id)
        {
            return Ok();
        }

        /// <summary>
        /// 获取公司分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IHttpActionResult Get([FromUri]CompanyQuery query)
        {
            var result = _companySvc.GetCompanyPage(query);
            return Ok(result);
        }

        /// <summary>
        /// 根据公司Id获取公司信息详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}/ById")]
        public IHttpActionResult GetById(int id)
        {
            var result = _companySvc.GetCompanyById(id);
            return Ok(result);
        }

        /// <summary>
        /// 新增公司
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] CompanyDto dto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            _companySvc.CreatCompany(dto);
            return Ok();
        }

        /// <summary>
        /// 根据公司id编辑公司信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, [FromBody] CompanyDto dto)
        {
            //参数验证
            if (id == 0)
            {
                ModelState.AddModelError("id", "公司id不允许为空");
                return BadRequest(ModelState);
            }
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            dto.Id = id;
            _companySvc.EditCompany(dto);
            return Ok();
        }

        /// <summary>
        /// 根据公司id删除公司信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            //参数验证
            if (id == 0)
            {
                ModelState.AddModelError("id", "公司id不允许为空");
                return BadRequest(ModelState);
            }
            _companySvc.DeleteCompany(id);
            return Ok();
        }
    }
}
