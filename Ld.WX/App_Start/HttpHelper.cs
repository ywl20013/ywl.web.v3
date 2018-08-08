using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Ld.WX
{
    public class HttpHelper
    {
        internal static T ConvertJson<T>(string method, string url, string postData)
        {
            try
            {
                /*
                //设定安全协议为安全套接字层(SSL)3.0协议     
                if (url.ToLower().IndexOf("https://") >= 0)
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                */
                System.Net.WebRequest httpRquest = System.Net.HttpWebRequest.Create(url);
                httpRquest.Method = method;// "POST";
                //这行代码很关键，不设置ContentType将导致后台参数获取不到值
                //httpRquest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";

                if (!string.IsNullOrEmpty(postData))
                {
                    httpRquest.ContentType = "application/x-www-form-urlencoded";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(postData);
                    httpRquest.ContentLength = buffer.Length;
                    System.IO.Stream requestStream = httpRquest.GetRequestStream();
                    requestStream.Write(buffer, 0, buffer.Length);
                }
                System.Net.WebResponse response = httpRquest.GetResponse();
                System.IO.Stream responseStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, System.Text.Encoding.UTF8);
                string responseContent = reader.ReadToEnd();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseContent);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(" Error: " + ex.Message);
            }

            return default(T);
        }
        internal static T ConvertJson<T>(string url, string postData)
        {
            return ConvertJson<T>("POST", url, postData);
        }

        internal static string Reqest(string method, string url, Dictionary<string, object> parameters, string[] files)
        {
            string output = "";

            if (files.Length == 0) return output;

            //1>创建请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //2>Cookie容器
            //  request.CookieContainer = cookieContainer;
            request.Method = method;
            request.Timeout = 20000;
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;
            request.KeepAlive = true;

            string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");//分界线
            byte[] boundaryBytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");

            request.ContentType = "multipart/form-data; boundary=" + boundary;//内容类型

            //3>表单数据模板
            string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";
            string filedataTemplate = "Content-Disposition:form-data; name=\"{0}\";filename=\"{1}\"\r\nContent-Type:{2}\r\n\r\n";

            try
            {
                using (Stream stream = request.GetRequestStream())
                {
                    //把参数写入请求流
                    if (null != parameters)
                    {
                        foreach (KeyValuePair<string, object> item in parameters)
                        {
                            stream.Write(boundaryBytes, 0, boundaryBytes.Length);//写入分界线
                            byte[] formBytes = System.Text.Encoding.UTF8.GetBytes(string.Format(formdataTemplate, item.Key, item.Value));
                            stream.Write(formBytes, 0, formBytes.Length);
                        }
                    }
                    //把文件下入请求流
                    if (files != null)
                    {
                        for (int i = 0; i < files.Length; i++)
                        {
                            var filePath = files[i];
                            string contentType = MimeMapping.GetMimeMapping(filePath);
                            string fileName = System.IO.Path.GetFileName(filePath);
                            if (string.IsNullOrWhiteSpace(filePath)) continue;
                            if (!File.Exists(filePath)) continue;
                            //分界线============================================注意：缺少次步骤，可能导致远程服务器无法获取Request.Files集合
                            stream.Write(boundaryBytes, 0, boundaryBytes.Length);
                            //6.1>请求头
                            var header = string.Format(filedataTemplate, "file", fileName, contentType);
                            byte[] byteHeader = System.Text.Encoding.UTF8.GetBytes(header);
                            stream.Write(byteHeader, 0, byteHeader.Length);
                            //6.2>把文件流写入请求流
                            FileStream fs = new FileStream(files[i], FileMode.Open, FileAccess.Read);
                            byte[] byteFile = new byte[fs.Length];
                            fs.Read(byteFile, 0, byteFile.Length);
                            fs.Close();
                            stream.Write(byteFile, 0, byteFile.Length);
                        }
                    }
                    //6.3>写入分隔流
                    byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                    stream.Write(trailer, 0, trailer.Length);
                    //6.4>关闭流
                    stream.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    output = reader.ReadToEnd();
                }
                response.Close();
                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal static string Reqest(string method, string url, Dictionary<string, object> parameters)
        {
            try
            {
                /*
                //设定安全协议为安全套接字层(SSL)3.0协议     
                if (url.ToLower().IndexOf("https://") >= 0)
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                */
                System.Net.WebRequest httpRquest = System.Net.HttpWebRequest.Create(url);
                httpRquest.Method = method;// "POST";
                                           //这行代码很关键，不设置ContentType将导致后台参数获取不到值
                                           //httpRquest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";


                //写入请求流
                if (null != parameters)
                {
                    httpRquest.ContentType = "application/x-www-form-urlencoded";
                    using (Stream stream = httpRquest.GetRequestStream())
                    {
                        foreach (KeyValuePair<string, object> item in parameters)
                        {

                            byte[] formBytes = System.Text.Encoding.UTF8.GetBytes(string.Format("{0}={1}", item.Key, item.Value));
                            stream.Write(formBytes, 0, formBytes.Length);
                        }
                        stream.Close();
                    }
                }
                System.Net.WebResponse response = httpRquest.GetResponse();
                System.IO.Stream responseStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, System.Text.Encoding.UTF8);
                string responseContent = reader.ReadToEnd();
                return responseContent;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(" Error: " + ex.Message);
            }
            return "";
        }
        internal static string Reqest(string method, string url, string postData)
        {
            try
            {
                /*
                //设定安全协议为安全套接字层(SSL)3.0协议     
                if (url.ToLower().IndexOf("https://") >= 0)
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                */
                System.Net.WebRequest httpRquest = System.Net.HttpWebRequest.Create(url);
                httpRquest.Method = method;// "POST";
                //这行代码很关键，不设置ContentType将导致后台参数获取不到值
                //httpRquest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";

                if (!string.IsNullOrEmpty(postData))
                {
                    httpRquest.ContentType = "application/x-www-form-urlencoded";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(postData);
                    httpRquest.ContentLength = buffer.Length;
                    System.IO.Stream requestStream = httpRquest.GetRequestStream();
                    requestStream.Write(buffer, 0, buffer.Length);
                }
                System.Net.WebResponse response = httpRquest.GetResponse();
                System.IO.Stream responseStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, System.Text.Encoding.UTF8);
                string responseContent = reader.ReadToEnd();
                return responseContent;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(" Error: " + ex.Message);
            }
            return "";
        }
        internal static string Reqest(string method, string url)
        {
            return Reqest(method, url, "");
        }
        internal static string Get(string url)
        {
            return Reqest("GET", url, "");
        }
        internal static string Post(string url, string data)
        {
            return Reqest("POST", url, data);
        }
        internal static string Put(string url, string data)
        {
            return Reqest("PUT", url, data);
        }
        internal static T Get<T>(string url)
        {
            string responseContent = Reqest("GET", url, "");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseContent);
        }


        /// <summary>
        /// 文件上传至远程服务器
        /// </summary>
        /// <param name="url">远程服务地址</param>
        /// <param name="postedFile">上传文件</param>
        /// <param name="parameters">POST参数</param>
        /// <returns>远程服务器响应字符串</returns>
        public static string PostFile(string url, string[] files, Dictionary<string, object> parameters)
        {
            return Reqest("POST", url, parameters, files);
        }


        //private void UploadFile(string strRequestUri, 
        //    string strCookie, 
        //    string filename)
        //{
        //    // 初始化HttpWebRequest
        //    HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(strRequestUri);

        //    // 封装Cookie
        //    Uri uri = new Uri(strRequestUri);
        //    Cookie cookie = new Cookie("Name", strCookie);
        //    CookieContainer cookies = new CookieContainer();
        //    cookies.Add(uri, cookie);
        //    httpRequest.CookieContainer = cookies;

        //    // 生成时间戳
        //    string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");
        //    byte[] boundaryBytes = Encoding.ASCII.GetBytes(string.Format("\r\n--{0}--\r\n", strBoundary));

        //    // 填报文类型
        //    httpRequest.Method = "Post";
        //    httpRequest.Timeout = 1000 * 120;
        //    httpRequest.ContentType = "multipart/form-data; boundary=" + strBoundary;

        //    // 封装HTTP报文头的流
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("--");
        //    sb.Append(strBoundary);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("Content-Disposition: form-data; name=\"");
        //    sb.Append("file");
        //    sb.Append("\"; filename=\"");
        //    sb.Append(filename);
        //    sb.Append("\"");
        //    sb.Append(Environment.NewLine);
        //    sb.Append("Content-Type: ");
        //    sb.Append("multipart/form-data;");
        //    sb.Append(Environment.NewLine);
        //    sb.Append(Environment.NewLine);
        //    byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sb.ToString());

        //    // 计算报文长度
        //    long length = postHeaderBytes.Length + this.FileUpload1.PostedFile.InputStream.Length + boundaryBytes.Length;
        //    httpRequest.ContentLength = length;

        //    // 将报文头写入流
        //    Stream requestStream = httpRequest.GetRequestStream();
        //    requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

        //    // 将上传文件内容写入流 //每次上传4k  1024*4 
        //    byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)this.FileUpload1.PostedFile.InputStream.Length))];
        //    int bytesRead = 0;

        //    while ((bytesRead = this.FileUpload1.PostedFile.InputStream.Read(buffer, 0, buffer.Length)) != 0)
        //    {
        //        requestStream.Write(buffer, 0, bytesRead);
        //    }

        //    // 将报文尾部写入流
        //    requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
        //    // 关闭流
        //    requestStream.Close();
        //}
    }
}