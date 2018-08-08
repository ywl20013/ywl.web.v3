using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Ywl.Data.Entity.Models;

namespace Ld.WX.Controllers
{
    public class HomeController : Controller
    {
        private String create_timestamp()
        {
            return (DateTime.Now.Millisecond / 1000).ToString();
        }
        private string get_jsapi_ticket(string access_token)
        {
            var ticket = WXInterface.GetTicket(access_token, "jsapi");
            if (ticket != null)
            {
                return ticket.ticket;
            }
            return "";
            /*
if (null == apiticket) {
    String url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket";
    String jsonStrTicket = Tools.sendGet(url, "access_token=" + access_token + "&type=jsapi");

    logger.debug("[jsonStrTicket] = " + jsonStrTicket);

    JSONObject json = JSONObject.fromObject(jsonStrTicket);
    ticket = (String) json.get("ticket");

} else {
    ticket = (String) apiticket;
}
             * */
        }
        /// <summary>
        /// 对字符串SHA1加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>加密后的十六进制字符串</returns>
        public static string Sha1Encrypt(string source, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;

            // 第一种方式
            byte[] byteArray = encoding.GetBytes(source);
            using (HashAlgorithm hashAlgorithm = new SHA1CryptoServiceProvider())
            {
                byteArray = hashAlgorithm.ComputeHash(byteArray);
                StringBuilder stringBuilder = new StringBuilder(256);
                foreach (byte item in byteArray)
                {
                    stringBuilder.AppendFormat("{0:x2}", item);
                }
                hashAlgorithm.Clear();
                return stringBuilder.ToString();
            }

            //// 第二种方式
            //using (SHA1 sha1 = SHA1.Create())
            //{
            //    byte[] hash = sha1.ComputeHash(encoding.GetBytes(source));
            //    StringBuilder stringBuilder = new StringBuilder();
            //    for (int index = 0; index < hash.Length; ++index)
            //        stringBuilder.Append(hash[index].ToString("x2"));
            //    sha1.Clear();
            //    return stringBuilder.ToString();
            //}
        }
        private string create_signature(string jsapi_ticket, string nonce_str, string timestamp, string url)
        {
            string string1 = "jsapi_ticket=" + jsapi_ticket + "&noncestr=" + nonce_str + "&timestamp=" + timestamp + "&url=" + url;


            return Sha1Encrypt(string1);


            /*
            // 注意这里参数名必须全部小写，且必须有序
string1 = "jsapi_ticket=" + jsapi_ticket + "&noncestr=" + nonce_str
        + "&timestamp=" + timestamp + "&url=" + url;

logger.debug("[string1] = " + string1);

try {
    MessageDigest crypt = MessageDigest.getInstance("SHA-1");
    crypt.reset();
    crypt.update(string1.getBytes("UTF-8"));
    signature = byteToHex(crypt.digest());

    logger.debug("[signature] = " + signature);

} catch (NoSuchAlgorithmException e) {
    e.printStackTrace();
} catch (UnsupportedEncodingException e) {
    e.printStackTrace();
}
            
            */
        }

        public User GetCurrentUser()
        {
            //从url参数中获取用户openid
            var current_user_openid = Request["openid"];

            //从session中获取用户openid
            if (current_user_openid == null)
            {
                var current_user_openid_session = System.Web.HttpContext.Current.Session["openid"];
                if (current_user_openid_session != null)
                {
                    current_user_openid = current_user_openid_session.ToString();
                }
            }
            else
                System.Web.HttpContext.Current.Session["openid"] = current_user_openid;

            //重新从微信中获取用户openid
            if (current_user_openid == null)
            {
                WXInterface.ReGetOpenId();
                var current_user_openid_session = System.Web.HttpContext.Current.Session["openid"];
                if (current_user_openid_session != null)
                {
                    current_user_openid = current_user_openid_session.ToString();
                }
            }
            if (current_user_openid == null)
            {
                return null;
                //   return Content("无法获取用户信息，请确认微信客户端打开链接");
            }
            //if (user_openids != null)
            //{
            //    string content = current_user_openid + "<br /><br />";
            //    foreach (var id in user_openids)
            //    {
            //        content += id + "<br />";
            //    }
            //    return Content(content);
            //}
            //else
            //{

            var user = HttpHelper.ConvertJson<User>("GET", Config.ApiServer + "/users/" + current_user_openid.ToString(), "");

            if (user != null)
                System.Web.HttpContext.Current.Session["current_user"] = user;
            //return Content(Newtonsoft.Json.JsonConvert.SerializeObject(user));
            return user;
        }

        public ActionResult BaseActionResult()
        {
            var user = GetCurrentUser();
            if (user == null)
            {
                var current_user_openid = "";
                var current_user_openid_session = System.Web.HttpContext.Current.Session["openid"];
                if (current_user_openid_session != null)
                {
                    current_user_openid = current_user_openid_session.ToString();
                }
                //return Redirect("Users/Register?id=" + current_user_openid.ToString());
                return RedirectToAction("Register", "Users", new { id = current_user_openid.ToString() });
                //   HttpHelper.Reqest("POST", Config.ApiServer + "/users/", "wxopenid=" + current_user_openid.ToString());
                //   user = HttpHelper.ConvertJson<Models.User>(Config.ApiServer + "/users/" + current_user_openid.ToString(), "");
            }
            else if (user.Account == null)
                //return Redirect("Users/Register?id=" + user.WxOpenId.ToString());
                return RedirectToAction("Register", "Users", new { id = user.WxOpenId.ToString() });

            ViewBag.CurrentUser = user;
            return null;
        }

        public ActionResult Index()
        {
            var result = BaseActionResult();
            return result ?? View();
        }

        public ActionResult Links()
        {
            var result = BaseActionResult();
            return result ?? View();
        }

        public ActionResult Me()
        {
            var result = BaseActionResult();
            return result ?? View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.\n" + Request.Url.AbsoluteUri;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            Log log = new Log();
            log.log2("OnActionExecuting\n");
            log.log("\t" + Request.Url.ToString() + "\n");
            var current_user_openid = "";
            var current_user_openid_session = System.Web.HttpContext.Current.Session["openid"];
            if (current_user_openid_session != null)
            {
                current_user_openid = current_user_openid_session.ToString();
                log.log("\topenid = " + current_user_openid + "\n");
            }
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            Log log = new Log();
            log.log2("OnActionExecuted\n");
            log.log("\t" + Request.Url.ToString() + "\n");
            var current_user_openid = "";
            var current_user_openid_session = System.Web.HttpContext.Current.Session["openid"];
            if (current_user_openid_session != null)
            {
                current_user_openid = current_user_openid_session.ToString();
                log.log("\topenid = " + current_user_openid + "\n");
            }
        }
    }
}