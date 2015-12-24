using SEMS.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEMS.DataAccess.Organization.Model
{
    public class Company: BaseModel
    {
        ///<summary>
        /// 公司名称
        ///</summary>
        public string CompanyName { get; set; }

        ///<summary>
        /// 父级Id
        ///</summary>
        public int? ParentId { get; set; }
      
    }
}
