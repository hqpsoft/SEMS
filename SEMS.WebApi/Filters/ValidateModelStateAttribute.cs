using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SEMS.WebApi.Filters
{
    /// <summary>
    /// 处理 发生在具体 action 或 controller 中的未处理的异常,这个和默认的全局异常处理还不一样.
    /// 如果这一类异常中包含 TipForUI, 说明是我们主动抛出的,需要提示用户的.
    /// 这一类的异常返回 BadRequest(400) 等错误状态,以及友好提示信息还有错误信息.
    /// </summary>
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!string.IsNullOrEmpty(filterContext.Exception.Message))
            {
                filterContext.Controller.ViewData.ModelState.AddModelError("SvcException", filterContext.Exception.Message);
            }

            if (!filterContext.Controller.ViewData.ModelState.IsValid)
            {
                filterContext.Result = new ViewResult
                {
                    ViewData = filterContext.Controller.ViewData,
                    TempData = filterContext.Controller.TempData,
                };
            }
            filterContext.ExceptionHandled = true;
        }
    }
}
