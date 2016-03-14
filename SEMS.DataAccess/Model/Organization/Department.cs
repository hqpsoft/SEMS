using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEMS.DataAccess.Model.Organization
{
    public class Department:BaseModel
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        public String DepartmentNo { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
    }
}
