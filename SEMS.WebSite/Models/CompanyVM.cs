using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEMS.WebSite.Models
{
    /// <summary>
    /// 公司管理ViewModel
    /// </summary>
    public class CompanyVM
    {
        public int Id { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        [Required(ErrorMessage = "公司名称不能为空")]
        [MaxLength(50)]
        public string CompanyName { get; set; }
    }
}
