using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEMS.WebApi.Filters
{
    /// <summary>
    /// 排除 ModelStateEnsureValidFilter
    /// </summary>
    public sealed class ExcludeModelStateEnsureValidFilterAttribute : Attribute
    {
    }
}
