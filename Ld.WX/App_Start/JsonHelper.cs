using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ld.WX
{
    public class JsonHelper
    {
        internal static T ConvertJson<T>(string url)
        {
            //try
            //{
            //    System.Net.WebRequest httpRquest = System.Net.HttpWebRequest.Create(url);
            //    httpRquest.Method = "GET";
            //    //这行代码很关键，不设置ContentType将导致后台参数获取不到值  
            //    //httpRquest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            //    System.Net.WebResponse response = httpRquest.GetResponse();
            //    System.IO.Stream responseStream = response.GetResponseStream();
            //    System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, System.Text.Encoding.UTF8);
            //    string responseContent = reader.ReadToEnd();
            //    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseContent);
            //}
            //catch (Exception) { }

            //return default(T);
            return ConvertJson<T>(url, null);
        }
        /// <summary>
        /// 转换Json字符串到具体的对象
        /// </summary>
        /// <param name="url">返回Json数据的链接地址</param>
        /// <param name="postData">POST提交的数据</param>
        /// <returns></returns>
        internal static T ConvertJson<T>(string url, string postData)
        {
            return HttpHelper.ConvertJson<T>(url, postData);
        }
        /// <summary>
        /// 通用的操作结果
        /// </summary>
        /// <param name="url">网页地址</param>
        /// <param name="postData">提交的数据内容</param>
        /// <returns></returns>
        internal static CommonResult GetExecuteResult(string url, string postData = null)
        {
            CommonResult success = new CommonResult();
            try
            {
                ErrorJsonResult result;
                if (postData != null)
                {
                    result = ConvertJson<ErrorJsonResult>(url, postData);
                }
                else
                {
                    result = ConvertJson<ErrorJsonResult>(url);
                }

                if (result != null)
                {
                    success.Success = (result.errcode == "0");
                    success.ErrorMessage = result.errmsg;
                }
            }
            catch (Exception ex)
            {
                success.ErrorMessage = ex.Message;
            }

            return success;
        }
    }
}