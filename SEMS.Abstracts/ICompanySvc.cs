using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEMS.Abstracts
{
    /// <summary>
    /// 公司管理业务契约
    /// </summary>
    public interface ICompanySvc
    {
        /// <summary>
        /// 新增公司
        /// </summary>
        void AddCompany();
    }
}
