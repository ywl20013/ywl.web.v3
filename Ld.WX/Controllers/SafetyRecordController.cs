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

    [Ywl.Web.Mvc.HandleError]
    public class SafetyRecordController : Controller
    {
        //private String create_timestamp(DateTime time)
        //{
        //    //  return (DateTime.Now.Millisecond / 1000).ToString();
        //    long intResult = 0;
        //    System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        //    intResult = (long)(time - startTime).TotalSeconds;
        //    return intResult.ToString();
        //}
        //private String create_nonce_str(int length)
        //{
        //    string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        //    string result = "";
        //    var rand = new Random();
        //    for (int i = 0; i < length; i++)
        //    {
        //        result += chars.Substring(rand.Next(0, chars.Length - 1), 1);
        //    }
        //    return result;
        //    //return Guid.NewGuid().ToString();
        //    //return DateTime.Now.ToString("yyyyMMddHHmmss");
        //}
        //private string get_jsapi_ticket(string access_token)
        //{
        //    var ticket = WXInterface.GetTicket(access_token, "jsapi");
        //    if (ticket != null)
        //    {
        //        return ticket.ticket;
        //    }
        //    return "";

        //}

        private string create_signature(string jsapi_ticket, string nonce_str, string timestamp, string url)
        {
            string string1 = "jsapi_ticket=" + jsapi_ticket + "&noncestr=" + nonce_str + "&timestamp=" + timestamp + "&url=" + url;
            return Ywl.Data.Entity.Utils.Sha1Encrypt(string1);
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

        public void GetWXConfig()
        {
            var dtNow = DateTime.Now;
            //string nonceStr = "Wm3WZYTPz0wzccnW";// DateTime.Now.ToString("yyyyMMddHHmmss");
            string nonceStr = WxJSSDKConfig.create_nonce_str(16);
            long timestamp = WxJSSDKConfig.create_timestamp(dtNow);
            var token = WXInterface.GetAccessToken(Config.AppID, Config.AppSecret).access_token;
            if (token != null)
            {

                string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;//获取当前url
                var ticket = WXInterface.GetTicket(token, "jsapi");
                if (ticket != null)
                {
                    string signature = create_signature(ticket.ticket, nonceStr, timestamp.ToString(), url);
                    ViewBag.ticket = ticket.ticket;
                    ViewBag.signature = signature;
                }
                ViewBag.token = token;
                ViewBag.nonceStr = nonceStr;
                ViewBag.timestamp = timestamp;
                ViewBag.appId = Config.AppID;
                ViewBag.url = url;
            }
        }

        public ActionResult Create()
        {
            GetWXConfig();
            return View();
        }


        public class PostResult
        {
            public class FileResult
            {
                public bool Success { get; set; }
                public string Error { get; set; }
                public string FileName { get; set; }
                public string ContentType { get; set; }
                public int ContentLength { get; set; }
                public string Path { get; set; }
            }
            public int Id { get; set; }
            public bool Success { get; set; }
            public string Error { get; set; }
            public int FileCount { get; set; }
            public List<FileResult> Files { get; private set; }
            public PostResult()
            {
                Files = new List<FileResult>();
            }
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreateConfirm(string id)
        {
            var account = Request["account"];
            var openid = Request["openid"];
            var content = Request["content"];
            var cate = Request["cate"];
            var subcate = Request["subcate"];
            var breakRulesType = Request["breakRulesType"];
            var resp = Request["resp"];
            var isdanger = Request["isdanger"];

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("content", content);
            parameters.Add("SourceType", 0);
            parameters.Add("SouceSubType", subcate);
            parameters.Add("BreakRulesType", breakRulesType);

            parameters.Add("DangerType", isdanger == "1" ? "隐患" : (isdanger == "0" ? "" : isdanger));
            parameters.Add("ResponsiblePerson", resp);

            //string data = "";
            //foreach (KeyValuePair<string, object> item in parameters)
            //{
            //    data += string.Format("{0}={1}&", item.Key, item.Value);
            //}
            //HttpHelper.Post(Config.ApiServer + "/safetycheckrecords/", data.Substring(0, data.Length - 1));

            Log log = new Log();
            log.log2("POST " + Config.ApiServer + "/SafetyCheckRecord/\n");
            try
            {
                var user = (User)Session["current_user"];
                if (user != null)
                {
                    parameters.Add("creator", user.Id);
                    parameters.Add("finder", user.Id);
                    parameters.Add("findTime", DateTime.Now.Ticks);

                    log.log("current_user:" + user.Account + ", " + user.WxOpenId + "\n");
                }
                else
                {
                    log.log("current_user:null\n");
                }
                var serverid = "";
                var token = WXInterface.GetAccessToken(Config.AppID, Config.AppSecret);

                List<string> uploadFiles = new List<string>();

                for (int i = 0; i < 20; i++)
                {
                    serverid = Request["file_" + i.ToString()];
                    if (!string.IsNullOrEmpty(serverid))
                    {
                        var fullFileName = WXInterface.GetMultimedia(token.access_token, serverid);
                        uploadFiles.Add(fullFileName);
                    }
                }

                var output = HttpHelper.PostFile(Config.ApiServer + "/SafetyCheckRecord/", uploadFiles.ToArray(), parameters);
                log.log("output: " + output + "\n");

                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<PostResult>(output);
                if (!result.Success)
                {
                    throw new Exception(result.Error);
                }
            }
            catch (Exception ex)
            {
                log.log("exception: " + ex.Message + "\n");
                throw ex;
            }

            return RedirectToAction("Index", "Home");
            //return RedirectToAction("Edit", "SafetyRecords", new { id = 1 });
        }

        public ActionResult Edit(int? id)
        {
            GetWXConfig();

            if (id == null) return HttpNotFound();
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditConfirm(string id)
        {
            var account = Request["account"];
            var openid = Request["openid"];
            var content = Request["content"];
            var cate = Request["cate"];
            var subcate = Request["subcate"];
            var resp = Request["resp"];
            var isdanger = Request["isdanger"];

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("content", content);
            parameters.Add("SourceType", cate);
            parameters.Add("SouceSubType", subcate);
            parameters.Add("DangerType", isdanger == "1" ? "隐患" : "");
            parameters.Add("ResponsiblePerson", resp);

            //string data = "";
            //foreach (KeyValuePair<string, object> item in parameters)
            //{
            //    data += string.Format("{0}={1}&", item.Key, item.Value);
            //}
            //HttpHelper.Post(Config.ApiServer + "/safetycheckrecords/", data.Substring(0, data.Length - 1));

            Log log = new Log();
            log.log2("PUT " + Config.ApiServer + "/SafetyCheckRecord/\n");
            try
            {
                var user = (User)Session["current_user"];
                if (user != null)
                {
                    parameters.Add("creator", user.Account);
                    parameters.Add("findTime", DateTime.Now.Ticks);

                    log.log("current_user:" + user.Account + ", " + user.WxOpenId + "\n");
                }
                else
                {
                    log.log("current_user:null\n");
                }
                var serverid = "";
                var token = WXInterface.GetAccessToken(Config.AppID, Config.AppSecret);

                List<string> uploadFiles = new List<string>();

                for (int i = 0; i < 20; i++)
                {
                    serverid = Request["file_" + i.ToString()];
                    if (!string.IsNullOrEmpty(serverid))
                    {
                        var fullFileName = WXInterface.GetMultimedia(token.access_token, serverid);
                        uploadFiles.Add(fullFileName);
                    }
                }

                var output = HttpHelper.Reqest("PUT", Config.ApiServer + "/SafetyCheckRecord/", parameters, uploadFiles.ToArray());
                log.log("output: " + output + "\n");
            }
            catch (Exception ex)
            {
                log.log("exception: " + ex.Message + "\n");
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Details(int? id)
        {
            if (id == null) return HttpNotFound();
            ViewBag.Id = id;
            var current_user_openid = System.Web.HttpContext.Current.Session["openid"];
            if (current_user_openid != null)
            {
                var user = HttpHelper.ConvertJson<User>("GET", Config.ApiServer + "/users/" + current_user_openid.ToString(), "");
                if (user == null)
                {
                    ViewBag.CurrentUser = null;
                }
                else
                {
                    ViewBag.CurrentUser = user;
                }
            }
            return View();
        }
        public ActionResult Delete(int? id)
        {
            return View();
        }
        public ActionResult SelectPerson(string id)
        {
            ViewBag.Id = id;
            return View();
        }
        public ActionResult Page(string id)
        {
            return View(id);
        }
    }
}