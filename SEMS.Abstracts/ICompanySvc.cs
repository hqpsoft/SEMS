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
        /// 获取公司分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        PageGridData<CompanyDto> GetCompanyPage(CompanyQuery query);
    }
}
