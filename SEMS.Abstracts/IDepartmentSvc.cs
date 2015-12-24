using SEMS.DataAccess.Dto.Base;
using SEMS.DataAccess.Query;
using SEMS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEMS.Abstracts
{
    /// <summary>
    /// 部门管理业务契约
    /// </summary>
    public interface IDepartmentSvc
    {
        /// <summary>
        /// 新增部门
        /// </summary>
        void CreatDepartment(DepartmentDto dto);

        PageGridData<DepartmentDto> GetDepartmentPage(DepartmentQuery query);
    }
}
