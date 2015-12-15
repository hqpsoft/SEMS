using SEMS.Infrastructure.Data;
using SEMS.DataAccess.Dto;
using SEMS.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEMS.Abstracts
{
    /// <summary>
    /// 公司管理业务接口
    /// </summary>
    public interface ICompanySvc
    {
        /// <summary>
        /// 新增公司
        /// </summary>
        /// <param name="dto"></param>
        void CreatCompany(CompanyDto dto);

        /// <summary>
        /// 公司编辑
        /// </summary>
        /// <param name="dto"></param>
        void EditCompany(CompanyDto dto);

        /// <summary>
        /// 根据公司id删除公司信息
        /// </summary>
        /// <param name="dto"></param>
        void DeleteCompany(int companyId);

        /// <summary>
        /// 获取公司分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        PageGridData<CompanyDto> GetCompanyPage(CompanyQuery query);

        /// <summary>
        /// 根据公司Id获取公司信息详情
        /// </summary>
        /// <param name="companyId"></param>
        CompanyDto GetCompanyById(int companyId);
    }
}
