﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ywl.Web.Api.Handler
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();
            if (lastError != null)
            {
                var log = new Log("err", null);
                log.log2(lastError.Message + "\n" + lastError.StackTrace);
                return;

                //异常信息
                string strExceptionMessage = string.Empty;

                //对HTTP 404做额外处理，其他错误全部当成500服务器错误
                HttpException httpError = lastError as HttpException;
                if (httpError != null)
                {
                    ////获取错误代码
                    //int httpCode = httpError.GetHttpCode();
                    //strExceptionMessage = httpError.Message;
                    //if (httpCode == 400 || httpCode == 404)
                    //{
                    //    Response.StatusCode = 404;
                    //    //跳转到指定的静态404信息页面，根据需求自己更改URL
                    //    Response.WriteFile("~/HttpError/404.html");
                    //    Server.ClearError();
                    //    return;
                    //}
              
                    return;
                }

                strExceptionMessage = lastError.Message;

                /*-----------------------------------------------------
                 * 此处代码可根据需求进行日志记录，或者处理其他业务流程
                 * ---------------------------------------------------*/

                /*
                 * 跳转到指定的http 500错误信息页面
                 * 跳转到静态页面一定要用Response.WriteFile方法                 
                 */
                //Response.StatusCode = 500;
                //Response.WriteFile("~/HttpError/500.html");

                //一定要调用Server.ClearError()否则会触发错误详情页（就是黄页）
                //Server.ClearError();
                //Server.Transfer("~/HttpError/500.aspx");
            }
        }
    }
}
