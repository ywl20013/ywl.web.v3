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
    public class UsersController : Controller
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
        public ActionResult Index()
        {
            var token = WXInterface.GetAccessToken(Config.AppID, Config.AppSecret).access_token;

            string state = Request["state"];
            List<string> user_openids = null;


            var current_user_openid = System.Web.HttpContext.Current.Session["openid"];
            if (state == "1" && current_user_openid == null)
            {
                user_openids = WXInterface.GetUserList(token);
            }
            WXInterface.ReGetOpenId();
            current_user_openid = System.Web.HttpContext.Current.Session["openid"];
            if (current_user_openid == null)
            {
                return Content("无法获取用户信息，请确认微信客户端打开链接");
            }
            if (user_openids != null)
            {
                string content = current_user_openid + "<br /><br />";
                foreach (var id in user_openids)
                {
                    content += id + "<br />";
                }
                return Content(content);
            }
            else
            {
                if (current_user_openid == null) return View();
                var user = HttpHelper.ConvertJson<User>("GET", Config.ApiServer + "/users/" + current_user_openid.ToString(), "");
                if (user == null)
                {
                    HttpHelper.Post(Config.ApiServer + "/users/", "wxopenid=" + current_user_openid.ToString());
                    user = HttpHelper.ConvertJson<User>(Config.ApiServer + "/users/" + current_user_openid.ToString(), "");
                }
                return Content(Newtonsoft.Json.JsonConvert.SerializeObject(user));
            }
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
        public ActionResult Register(string id, string msg)
        {
            if (id == null) return HttpNotFound("请求必须包含参数id！");
            ViewBag.OpenId = id;
            ViewBag.ErrorMsg = msg;
            return View();
        }
        public class Users
        {
            public List<User> Data { get; set; }
        }

        [HttpPost]
        [ActionName("Register")]
        public ActionResult CreateConfirm(string id)
        {
            var account = Request["account"];

            if (!string.IsNullOrEmpty(account))
            {
                //使用800工号，获取用户数据，
                var users = HttpHelper.ConvertJson<Users>("GET", Config.ApiServer + "/users/?columns[0][data]=Account&columns[0][search][value]=" + account, "");
                if (users != null && users.Data.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(users.Data[0].WxOpenId) && users.Data[0].WxOpenId != id)
                    {
                        return RedirectToAction("Register", "Users", new { id, msg = "此工号已被 " + users.Data[0].OrganizationPath + " " + users.Data[0].Name + " 绑定！" });
                    }
                    //更新用户的微信OpenId
                    HttpHelper.Put(Config.ApiServer + "/users/" + users.Data[0].Id.ToString(), "wxopenid=" + id);
                }
                else
                {
                    ViewBag.ErrorMsg = "无法检测到工号为“" + account + "”用户信息！";
                    //提醒输入的800工号不正确，无法检测到此人信息
                    ModelState.AddModelError("", "无法检测到工号为“" + account + "”用户信息！");
                    return RedirectToAction("Register", "Users", new { id, errmsg = "无法检测到工号为“" + account + "”用户信息！" });
                    ////创建用户
                    //HttpHelper.Reqest("POST", Config.ApiServer + "/users/", "wxopenid=" + id + "&account=" + account);
                }
                return RedirectToAction("index", "home");

                //   HttpHelper.Reqest("POST", Config.ApiServer + "/users/", "wxopenid=" + current_user_openid.ToString());
                //   user = HttpHelper.ConvertJson<Models.User>(Config.ApiServer + "/users/" + current_user_openid.ToString(), "");
            }
            return RedirectToAction("Register", "Users", new { id });
        }

        public ActionResult UnRegister()
        {
            var current_user_openid = System.Web.HttpContext.Current.Session["openid"];
            if (current_user_openid != null)
            {
                //使用 wxopenid，获取用户数据，
                var users = HttpHelper.ConvertJson<Users>("GET", Config.ApiServer + "/users/?columns[0][data]=wxopenid&columns[0][search][value]=" + current_user_openid, "");
                if (users != null && users.Data.Count() > 0)
                {
                    //更新用户的微信OpenId
                    HttpHelper.Put(Config.ApiServer + "/users/" + users.Data[0].Id.ToString(), "wxopenid=null");
                }
                else
                {
                    var ErrorMsg = "无法检测到OpenId为“" + current_user_openid + "”用户信息！";
                    ViewBag.ErrorMsg = ErrorMsg;
                    //提醒输入的800工号不正确，无法检测到此人信息
                    ModelState.AddModelError("", ErrorMsg);
                    return RedirectToAction("Index", "Home");
                    ////创建用户
                    //HttpHelper.Reqest("POST", Config.ApiServer + "/users/", "wxopenid=" + id + "&account=" + account);
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}