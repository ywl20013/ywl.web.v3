using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ld.WX
{
    using Models;
    using System.Net;
    using System.Text.RegularExpressions;

    public class WXInterface
    {
        /// <summary>
        /// 网页授权access_token
        /// </summary>
        public OAuth_Token WebAuthToken { get; set; }

        /// <summary>
        /// 普通access_token
        /// </summary>
        public OAuth_Token BaseToken { get; set; }

        /// <summary>
        /// 获取每次操作微信API的Token访问令牌
        /// </summary>
        /// <param name="appid">应用ID</param>
        /// <param name="secret">开发者凭据</param>
        /// <returns></returns>
        public static Ld.WX.Models.OAuth_Token GetAccessToken(string appid, string secret)
        {
            var token = GetCache<OAuth_Token>("AccessToken");
            if (token != null && token.access_token != null)
            {
                if (token.CreateTime.AddSeconds(token.expires_in) > DateTime.Now)
                    return token;
            }
            // string access_token = "";
            //正常情况下access_token有效期为7200秒,这里使用缓存设置短于这个时间即可
            // string access_token = MemoryCacheHelper.GetCacheItem<string>("access_token", delegate ()
            // {
            string grant_type = "client_credential";
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}",
                                    grant_type, appid, secret);
            try
            {
                token = HttpHelper.Get<OAuth_Token>(url);
                token.CreateTime = DateTime.Now.AddSeconds(-10);
                SetCache("AccessToken", token);
                return token;
            }
            catch (Exception) { }

            return null;
        }
        public static T GetCache<T>(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            var obj = objCache[CacheKey];
            if (obj != null) return (T)obj;
            return default(T);
        }
        public static void SetCache(string cacheKey, object objObject)
        {
            if (objObject == null)
                return;
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            var obj = objCache[cacheKey];
            if (obj != null) objCache.Remove(cacheKey);
            objCache.Insert(cacheKey, objObject);
        }
        /*
        public static OAuth_Token GetAccessToken()
        {
            string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;//获取当前url

            var code = System.Web.HttpContext.Current.Request.QueryString["code"];
            //先要判断是否是获取code后跳转过来的
            if (String.IsNullOrEmpty(code))
            {
                //Code为空时，先获取Code
                string GetCodeUrls = GetCodeUrl(url);
                HttpContext.Current.Response.Redirect(GetCodeUrls);//先跳转到微信的服务器，取得code后会跳回来这页面的
            }
            else
            {
                //Code非空，已经获取了code后跳回来啦，现在重新获取openid

                var tokenUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + Config.AppID + "&secret=" + Config.AppSecret + "&code=" + code + "&grant_type=authorization_code";
                return HttpHelper.ConvertJson<OAuth_Token>(tokenUrl, null);
            }
            return null;
        }
        */
        public static Ticket GetTicket(string access_token, string type)
        {
            //"access_token=" + access_token + "&type=jsapi"
            /*
String url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket";
String jsonStrTicket = Tools.sendGet(url, "access_token=" + access_token + "&type=jsapi");
 * */
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type={1}",
                                    access_token, type);

            try
            {
                System.Net.WebRequest httpRquest = System.Net.HttpWebRequest.Create(url);
                httpRquest.Method = "GET";
                //这行代码很关键，不设置ContentType将导致后台参数获取不到值  
                //httpRquest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                System.Net.WebResponse response = httpRquest.GetResponse();
                System.IO.Stream responseStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, System.Text.Encoding.UTF8);
                string responseContent = reader.ReadToEnd();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Ticket>(responseContent);
            }
            catch (Exception) { }

            return null;
        }
        /// <summary>
        /// 获取关注用户列表
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="nextOpenId">第一个拉取的OPENID，不填默认从头开始拉取</param>
        /// <returns></returns>

        public static List<string> GetUserList(string accessToken, string nextOpenId = null)
        {
            List<string> list = new List<string>();

            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}", accessToken);
            if (!string.IsNullOrEmpty(nextOpenId))
            {
                url += "&next_openid=" + nextOpenId;
            }

            Models.UserListJsonResult result = JsonHelper.ConvertJson<Models.UserListJsonResult>(url);
            if (result != null && result.data != null)
            {
                list.AddRange(result.data.openid);
            }

            return list;

        }

        public static UserJson GetUserDetail(string accessToken, string openId, Language lang = Language.zh_CN)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang={2}",
                    accessToken, openId, lang.ToString());

            UserJson result = JsonHelper.ConvertJson<UserJson>(url);
            return result;
        }

        #region 重新获取Code的跳转链接(没有用户授权的，只能获取基本信息）
        /// <summary>重新获取Code,以后面实现带着Code重新跳回目标页面(没有用户授权的，只能获取基本信息（openid））</summary>
        /// <param name="url">目标页面</param>
        /// <returns></returns>
        public static string GetCodeUrl(string url)
        {
            string CodeUrl = "";
            //对url进行编码
            url = System.Web.HttpUtility.UrlEncode(url);
            CodeUrl = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid=" +
                Config.AppID + "&redirect_uri=" + url + "&response_type=code&scope=snsapi_base&state=1#wechat_redirect");

            return CodeUrl;

        }
        #endregion

        /// <summary>
        /// 获取使用当前网页的用户openid，保存在session中
        /// </summary>
        public static void ReGetOpenId()
        {
            string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;//获取当前url
            var openid = HttpContext.Current.Session["openid"];
            if (openid == null || openid.ToString() == "")
            {
                var code = System.Web.HttpContext.Current.Request.QueryString["code"];
                //先要判断是否是获取code后跳转过来的
                if (string.IsNullOrEmpty(code))
                {
                    //Code为空时，先获取Code
                    string GetCodeUrls = GetCodeUrl(url);
                    HttpContext.Current.Response.Redirect(GetCodeUrls);//先跳转到微信的服务器，取得code后会跳回来这页面的
                }
                else
                {
                    //Code非空，已经获取了code后跳回来啦，现在重新获取openid
                    //  Log log = new Log(AppDomain.CurrentDomain.BaseDirectory + @"/log/Log.txt");

                    openid = GetOauthAccessOpenId(System.Web.HttpContext.Current.Request.QueryString["Code"]);//重新取得用户的openid
                    System.Web.HttpContext.Current.Session["openid"] = openid;
                }
            }
        }
        public static void ReGetCode(string code)
        {
            string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;//获取当前url


            //清除url中过期的code
            if (Regex.Match(url, @"&code=(\w|\d)*").Success)
                url = Regex.Replace(url, @"&code=(\w|\d)*", "");
            if (Regex.Match(url, @"\?code=(\w|\d)*&").Success)
                url = Regex.Replace(url, @"\?code =(\w|\d)*&", "?");
            if (Regex.Match(url, @"\?code=(\w|\d)*").Success)
                url = Regex.Replace(url, @"\?code=(\w|\d)*", "");

            //url.replace(/&code=(\w|\d)*/,'').replace(/\?code=(\w|\d)*&/,'?').replace(/\?code=(\w|\d)*/,'')

            //先要判断是否是获取code后跳转过来的
            if (string.IsNullOrEmpty(code))
            {
                //Code为空时，先获取Code
                string GetCodeUrls = GetCodeUrl(url);
                HttpContext.Current.Response.Redirect(GetCodeUrls);//先跳转到微信的服务器，取得code后会跳回来这页面的
            }
        }
        #region 以Code换取用户的openid、access_token
        /// <summary>根据Code获取用户的openid、access_token</summary>
        public static string GetOauthAccessOpenId(string code)
        {
            Log log = new Log();
            string Openid = "";
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" +
                Config.AppID + "&secret=" + Config.AppSecret + "&code=" + code + "&grant_type=authorization_code";

            log.log("拿到的url是：" + url + "\n");
            OAuth_Token ac = JsonHelper.ConvertJson<OAuth_Token>(url);
            log.log("能否从html里拿到:" + ac.ToJsonString() + "\n");
            if (ac.access_token == null)
            {
                ReGetCode(null);
            }
            Openid = ac.openid;
            return Openid;
        }
        #endregion

        /// <summary>
        /// 获取菜单数据
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <returns></returns>
        public static MenuJson GetMenu(string accessToken)
        {
            MenuJson menu = null;

            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", accessToken);
            MenuListJson list = JsonHelper.ConvertJson<MenuListJson>(url);
            if (list != null)
            {
                menu = list.menu;
            }
            return menu;
        }
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="menuJson">菜单对象</param>
        /// <returns></returns>
        public static CommonResult CreateMenu(string accessToken, MenuJson menuJson)
        { 
            string postData = menuJson.ToJsonString();

            return CreateMenu(accessToken, postData);
        }
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="menuJson">菜单json数据</param>
        /// <returns></returns>
        public static CommonResult CreateMenu(string accessToken, string menuJson)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", accessToken);
     
            return JsonHelper.GetExecuteResult(url, menuJson);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <returns></returns>
        public static CommonResult DeleteMenu(string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", accessToken);

            return JsonHelper.GetExecuteResult(url);
        }

        /// <summary>
        /// 下载保存多媒体文件,返回多媒体保存路径
        /// </summary>
        /// <param name="ACCESS_TOKEN">
        /// <param name="MEDIA_ID">
        /// <returns></returns>
        public static string GetMultimedia(string ACCESS_TOKEN, string MEDIA_ID)
        {
            string file = string.Empty;
            string content = string.Empty;
            string strpath = string.Empty;
            string savepath = string.Empty;
            string url = "https://api.weixin.qq.com/cgi-bin/media/get?access_token=" + ACCESS_TOKEN + "&media_id=" + MEDIA_ID;

            System.Net.WebRequest req = System.Net.HttpWebRequest.Create(url);
            req.Method = "GET";// "POST";
            using (WebResponse wr = req.GetResponse())
            {
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();

                strpath = myResponse.ResponseUri.ToString();
                //  WriteLog("接收类别://" + myResponse.ContentType);
                WebClient mywebclient = new WebClient();
                savepath = HttpContext.Current.Server.MapPath("~\\uploads") + "\\";
                var filename = DateTime.Now.ToString("yyyyMMddHHmmssfff")
                    + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";
                System.IO.Directory.CreateDirectory(savepath);
                //   WriteLog("路径://" + savepath);
                try
                {
                    mywebclient.DownloadFile(strpath, savepath + filename);
                    file = savepath + filename;
                }
                catch (Exception ex)
                {
                    savepath = ex.ToString();
                }

            }
            return file;
        }
    }
}