using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEMS.DataAccess.Query
{
    /// <summary>
    /// 公司管理查询类
    /// </summary>
    public class CompanyQuery : BaseQuery
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
    }
}
