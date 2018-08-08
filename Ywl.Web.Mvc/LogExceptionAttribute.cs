using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ywl.Web.Mvc
{
    /// <summary>
    /// 错误日志记录
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class HandleErrorAttribute : System.Web.Mvc.HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                string controllerName = (string)filterContext.RouteData.Values["controller"];
                string actionName = (string)filterContext.RouteData.Values["action"];
                string msgTemplate = string.Format("在执行 controller[{0}] 的 action[{1}] 时产生异常:{2}", controllerName, actionName, filterContext.Exception.Message);

                //通知MVC框架，现在这个异常已经被我处理掉，你不需要将黄页显示给用户
                filterContext.ExceptionHandled = true;
                //跳转到错误提醒页面         
                var view = new ViewResult();
                view.ViewName = "error";
                view.ViewBag.Message = msgTemplate;
                filterContext.Result = view;
            }


            if (filterContext.Result is JsonResult)
            {
                //当结果为json时，设置异常已处理，此时要在action上调用JsonException属性
                filterContext.ExceptionHandled = true;
            }
            else
            {
                //否则调用原始设置
                base.OnException(filterContext);
            }
        }
    }

}
