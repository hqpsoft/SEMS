using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SEMS.WebSite.Filters
{

    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Controller.ViewData["ErrorMessage"] = filterContext.Exception.Message;
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
