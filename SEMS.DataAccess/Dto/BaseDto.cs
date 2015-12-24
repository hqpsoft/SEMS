using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEMS.DataAccess.Dto
{
    public class BaseDto
    {

        public int Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
