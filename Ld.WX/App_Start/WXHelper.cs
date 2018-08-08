using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ld.WX.App_Start
{
    /// <summary>
    /// 微信网页编程
    /// </summary>
    public class WXWebHelper
    {
        /// <summary>

        ///   返回使用微信JS-SDK接口的配置

        //    appId: @ViewBag.wx_appid, // 必填，企业号的唯一标识，此处填写企业号corpid

        //    timestamp: @ViewBag.wx_timestamp, // 必填，生成签名的时间戳

        //    nonceStr: @ViewBag.wx_noncestr, // 必填，生成签名的随机串

        //    signature: @ViewBag.wx_signature,// 必填，签名，见附录1

        /// </summary>

        /// <returns></returns>

        public WxJSSDKConfig GetJSSDKConfig()

        {

            string appid = Config.AppID;

            string secret = Config.AppSecret;

            string timestamp = GenerateTimeStamp(DateTime.Now).ToString();

            string noncestr = GenerateNonceStr();

            string signature = "";



            //签名算法  https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421141115

            //1. 获取AccessToken（有效期7200秒，开发者必须在自己的服务全局缓存access_token）

            string url1 = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={appid}&secret={secret}";

            string result = HttpService.Get(url1);

            JsonData jd = JsonMapper.ToObject(result);

            string access_token = (string)jd["access_token"];



            //2. 用第一步拿到的access_token 采用http GET方式请求获得jsapi_ticket（有效期7200秒，开发者必须在自己的服务全局缓存jsapi_ticket）

            string url2 = $"https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={access_token}&type=jsapi";

            string result2 = HttpService.Get(url2);

            JsonData jd2 = JsonMapper.ToObject(result2);

            string ticket = (string)jd2["ticket"];



            //3. 开始签名

            string now_url = HttpContext.Current.Request.Url.AbsoluteUri;

            string no_jiami = $"jsapi_ticket={ticket}&noncestr={noncestr}&timestamp={timestamp}&url={now_url}";

            //SHA1加密

            signature = FormsAuthentication.HashPasswordForStoringInConfigFile(no_jiami, "SHA1");



            WxPayData data = new WxPayData();

            data.SetValue("appId", appid);

            data.SetValue("timestamp", timestamp);

            data.SetValue("nonceStr", noncestr);

            data.SetValue("signature", signature);



            Log.Debug(this.GetType().ToString(), "使用微信JS-SDK接口的配置 : " + data.ToPrintStr());



            return data;

        }

        public long GenerateTimeStamp(DateTime time)
        {
            long intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (long)(time - startTime).TotalSeconds;
            return intResult;
        }
        public string GenerateNonceStr()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        public static string GetCodeUrl(string url)
        {
            /*
             * https://open.weixin.qq.com/connect/oauth2/authorize?appid=APPID&redirect_uri=REDIRECT_URI&response_type=code&scope=SCOPE&state=STATE#wechat_redirect 
             * 若提示“该链接无法访问”，请检查参数是否填写错误，是否拥有scope参数对应的授权作用域权限。
             * */

            //对url进行编码
            url = System.Web.HttpUtility.UrlEncode(url);
            return "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Config.AppID + "&redirect_uri=" + url + "&response_type=code&scope=snsapi_base&state=1#wechat_redirect";

        }

        public static string ReGetCode()
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
                return System.Web.HttpContext.Current.Request.QueryString["Code"];
            }
            return "";
        }
    }
}