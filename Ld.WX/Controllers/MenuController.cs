using Ld.WX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Ld.WX.Controllers
{
    public class MenuController : Controller
    {
        public ActionResult Index()
        {
            var auth = WXInterface.GetAccessToken(Config.AppID, Config.AppSecret);
            var json = WXInterface.GetMenu(auth.access_token);
            var returnType = Request["r"];
            if (returnType == "json")
            {
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            return View(json);
        }
        public ActionResult Edit()
        {
            var auth = WXInterface.GetAccessToken(Config.AppID, Config.AppSecret);
            var json = WXInterface.GetMenu(auth.access_token);
            ViewBag.data = json.ToJsonString();
            return View();
        }
        [Authorize]
        [ActionName("Edit")]
        [HttpPost]
        public ActionResult EditConfirm()
        {

            var data = Request["data"];
            if (data == null) return Json(new CommonResult { Success = false, ErrorMessage = "参数data不能为空！" }, JsonRequestBehavior.AllowGet);

            var auth = WXInterface.GetAccessToken(Config.AppID, Config.AppSecret);
            var ret = WXInterface.CreateMenu(auth.access_token, data);

            return Json(ret, JsonRequestBehavior.AllowGet);
        }
        // GET: Menu
        public ActionResult CreateDefault()
        {
            MenuInfo productInfo = new MenuInfo("软件产品", new MenuInfo[] {
                new MenuInfo("移动办公平台", MenuInfo.ButtonType.click, "patient"),
                new MenuInfo("安全审核办公平台", MenuInfo.ButtonType.click, "aqscgl")
            });

            MenuInfo frameworkInfo = new MenuInfo("框架产品", new MenuInfo[] {
                new MenuInfo("Web开发框架", MenuInfo.ButtonType.click, "web"),
                new MenuInfo("代码生成工具", MenuInfo.ButtonType.click, "database2sharp")
            });

            MenuInfo relatedInfo = new MenuInfo("相关链接", new MenuInfo[] {
                new MenuInfo("公司介绍", MenuInfo.ButtonType.click, "Event_Company"),
                new MenuInfo("官方网站", MenuInfo.ButtonType.view, "http://www.bjdflld.com/wx"),
                //new MenuInfo("官方网站", ButtonType.click, "http://www.bjdflld.com"),
                new MenuInfo("提点建议", MenuInfo.ButtonType.click, "Event_Suggestion"),
                new MenuInfo("联系客服",MenuInfo. ButtonType.click, "Event_Contact"),
                //new MenuInfo("发邮件", ButtonType.view, "http://mail.qq.com/cgi-bin/qm_share?t=qm_mailme&email=S31yfX15fn8LOjplKCQm")
                new MenuInfo("发邮件", MenuInfo.ButtonType.click, "Event_Mail")
            });

            MenuJson menuJson = new MenuJson();
            menuJson.button.AddRange(new MenuInfo[] { productInfo, frameworkInfo, relatedInfo });

            var auth = WXInterface.GetAccessToken(Config.AppID, Config.AppSecret);
            if (WXInterface.DeleteMenu(auth.access_token).Success)
            {
                var ret = WXInterface.CreateMenu(auth.access_token, menuJson);
                if (!ret.Success)
                {
                    return Content("创建微信公众号菜单失败！\n" + ret.ErrorMessage);
                }
            }

            return View();
        }

        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext.ActionDescriptor.ActionName.ToLower() == "edit")
            {
                var unAuthResult = new ContentResult { Content = "<html><head></head><body><div><h1 style=\"text-align: center;vertical-align: middle;height: 100px;line-height: 100px;color: red;\">当前页面需要验证权限，您需要登录才能访问！</h1></div></body></html>" };

                var current_user = System.Web.HttpContext.Current.Session["current_user"];
                if (current_user != null)
                {
                    var user = (Ywl.Data.Entity.Models.User)current_user;
                    if (user == null) filterContext.Result = unAuthResult;
                    else
                    {
                        if (user.Account != "admin")
                        {
                            filterContext.Result = unAuthResult;
                        }
                        else
                        {
                            //  base.OnAuthentication(filterContext);

                            string[] roles = null;
                            var ticket = new System.Web.Security.FormsAuthenticationTicket(user.Id.ToString(), false, 1);
                            System.Security.Principal.IIdentity identity = new System.Web.Security.FormsIdentity(ticket);
                            System.Security.Principal.IPrincipal principal = new System.Security.Principal.GenericPrincipal(identity, roles);
                            //HttpContext.Current.User = principal;
                            filterContext.Principal = principal;
                        }
                    }
                }
                else
                {
                    filterContext.Result = unAuthResult;
                }
            }
            else
            {
                base.OnAuthentication(filterContext);
            }
        }
    }
}