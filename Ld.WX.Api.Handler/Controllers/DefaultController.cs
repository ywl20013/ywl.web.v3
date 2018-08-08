using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Xml;
using Ywl.Web.Api;
using Ywl.Web.Api.Handler;

namespace Ld.WX.Api.Handler.Controllers
{
    public class DefaultController : System.Web.Http.ApiController
    {
        /// <summary>
        /// 签名
        /// </summary>
        public class Signature
        {
            public class MsgTypes
            {
                public static string evt = "event";
                public static string text = "text";
                public static string voice = "voice";
                public static string image = "image";
                public static string video = "video";
                public static string shortvideo = "shortvideo";
                public static string location = "location";
            }
            /// <summary>
            /// 微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数。
            /// </summary>
            public string signature { get; set; }
            /// <summary>
            /// 时间戳
            /// </summary>
            public int timestamp { get; set; }
            /// <summary>
            /// 随机数
            /// </summary>
            public int nonce { get; set; }
            /// <summary>
            /// 随机字符串
            /// </summary>
            public string echostr { get; set; }

            public string ToUserName { get; set; }
            public string FromUserName { get; set; }
            /// <summary>
            /// 微信公众平台记录粉丝发送该消息的具体时间
            /// </summary>
            public int CreateTime { get; set; }

            /// <summary>
            /// text: 用于标记该xml 是文本消息，一般用于区别判断
            /// </summary>
            public string MsgType { get; set; }

            /// <summary>
            /// 粉丝发给公众号的具体内容是欢迎开启公众号开发者模式
            /// </summary>
            public string Content { get; set; }

            /// <summary>
            /// 公众平台为记录识别该消息的一个标记数值, 微信后台系统自动产生
            /// </summary>
            public int MsgId { get; set; }

            public string openid { get; set; }

            public string ToJsonString() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        // GET: api/Home/5
        public string Get(int id)
        {
            return "Default " + id;
        }

        // GET: api/Default
        public HttpResponseMessage Get(string signature, string timestamp, string nonce, string echostr)
        {
            Log log = new Log();
            string url = HttpContext.Current.Request.Url.ToString();
            log.log2("url = {0}\n", url);
            log.log("\tsignature={0},timestamp={1},nonce={2},echostr={3}\n", signature, timestamp, nonce, echostr);
            string Token = "wx_service_token_bjdfld";
            //string signature = Request.QueryString["signature"];
            //string timestamp = Request.QueryString["timestamp"];
            //string nonce = Request.QueryString["nonce"];
            //string echostr = Request.QueryString["echostr"];

            string[] ArrTmp = { Token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string hashcode = string.Join("", ArrTmp);
            //  hashcode = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(hashcode, "SHA1");

            hashcode = Ywl.Data.Entity.Utils.Sha1Encrypt(hashcode).ToLower();

            //SHA1 algorithm = SHA1.Create();
            //byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(hashcode));
            //string sh1 = "";
            //for (int i = 0; i < data.Length; i++)
            //{
            //    sh1 += data[i].ToString("x2").ToUpperInvariant();
            //}
            //hashcode = sh1;
            //hashcode = hashcode.ToLower();

            log.log("\n\t signature: {0}", signature);
            log.log("\n\t  hashcode: {0}", hashcode);
            log.log("\n\t    return: {0}", echostr);

            var resp = new HttpResponseMessage(signature == hashcode ? HttpStatusCode.OK : HttpStatusCode.Forbidden);
            resp.Content = new StringContent(signature == hashcode ? echostr : "Forbidden", System.Text.Encoding.UTF8, "text/plain");
            return resp;
        }

        // POST: api/Home
        public HttpResponseMessage Post(string signature, string timestamp, string nonce, string openid)
        {
            Log log = new Log();
            string url = HttpContext.Current.Request.Url.ToString();
            log.log2("post url = {0}\n", url);

            var resp = new HttpResponseMessage(HttpStatusCode.OK);


            //string signature = value.signature;// Request.QueryString["signature"];
            //int timestamp = value.timestamp;// Request.QueryString["timestamp"];
            //int nonce = value.nonce;// Request.QueryString["nonce"];
            //string openid = value.openid;// Request.QueryString["openid"];
            string postString = "";
            try
            {
                //   log.log("\t data = {0}\n", value.ToJsonString());
                log.log("\t xml = {0}\n", "before init XmlDocument");
                XmlDocument xmldoc = new XmlDocument();
                // xmldoc.Load(Request.InputStream);

                log.log("\t xml = {0}\n", "before init stream");
                Stream stream = HttpContext.Current.Request.InputStream;// Request.InputStream;

                log.log("\t stream.Length = {0}\n", stream.Length);
                string result = "";
                if (stream.Length > 0)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    postString = Encoding.UTF8.GetString(postBytes);
                    MessageTranser mt = new MessageTranser(postString);
                    result = mt.DealMessage();

                    xmldoc.LoadXml(postString);
                    XmlNode MsgType = xmldoc.SelectSingleNode("/xml/MsgType");
                    XmlNode xFromUserName = xmldoc.SelectSingleNode("/xml/FromUserName");
                    XmlNode xContent = xmldoc.SelectSingleNode("/xml/Content");
                    XmlNode xEvent = xmldoc.SelectSingleNode("/xml/Event");
                    XmlNode xEventKey = xmldoc.SelectSingleNode("/xml/EventKey");
                    log.log("\t xml = {0}\n", postString);
                    log.log("\t MsgType = {0}\n", MsgType == null ? "" : MsgType.InnerText.ToLower());
                    log.log("\t FromUserName = {0}\n", xFromUserName == null ? "" : xFromUserName.InnerText.ToLower());
                    log.log("\t Event = {0}\n", xEvent == null ? "" : xEvent.InnerText.ToLower());
                    log.log("\t EventKey = {0}\n", xEventKey == null ? "" : xEventKey.InnerText.ToLower());
                    log.log("\t Content = {0}\n", xContent == null ? "" : xContent.InnerText.ToLower());

                }
                resp.Content = new StringContent(result, System.Text.Encoding.UTF8, "text/xml");
                return resp;
            }
            catch (Exception e)
            {
                log.log("\t Exception = {0}\n{1}\n", e.Message, e.StackTrace);
                resp.Content = new StringContent(e.Message, System.Text.Encoding.UTF8, "text/xml");
                return resp;
            }
        }

        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            Log log = new Log();
            log.log2("controllerContext = \n{0}\n",
                Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    controllerContext.ControllerDescriptor.ControllerName,
                    controllerContext.Request.RequestUri.AbsoluteUri,
                    RouteValues = controllerContext.RouteData.Values,
                    controllerContext.Request.Content
                },
                    new JsonSerializerSettings
                    {
                        MaxDepth = 1,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }
                ));
            return base.ExecuteAsync(controllerContext, cancellationToken);
        }
    }
}
