using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Ywl.Web.Api.Handler.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public Task<Hashtable> Post()
        {
            // 检查是否是 multipart/form-data 
            if (!Request.Content.IsMimeMultipartContent("form-data"))
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            //文件保存目录路径 
            string SaveTempPath = "~/uploads/";
            string dirTempPath = HttpContext.Current.Server.MapPath(SaveTempPath);
            if (!System.IO.Directory.Exists(dirTempPath))
                System.IO.Directory.CreateDirectory(dirTempPath);

            // 设置上传目录 
            var provider = new MultipartFormDataStreamProvider(dirTempPath);
            //var queryp = Request.GetQueryNameValuePairs();//获得查询字符串的键值集合 
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<Hashtable>(o =>
                {
                    Hashtable hash = new Hashtable();
                    hash["error"] = 1;
                    hash["errmsg"] = "上传出错";
                    if (provider.FileData.Count == 0)
                    {
                        hash["error"] = 1;
                        hash["errmsg"] = "上传出错，服务器没有收到文件；可能是文件大小不合适。";
                        return hash;
                    }
                    for (int i = 0; i < provider.FileData.Count; i++)
                    {
                        var file = provider.FileData[i];//provider.FormData 
                        string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                        FileInfo fileinfo = new FileInfo(file.LocalFileName);
                        // 最大文件大小 
                        // 4096000 = 4M
                        // 10240000 = 10M
                        // 2048000000 = 2G
                        int maxSize = 4096000;
                        if (fileinfo.Length <= 0)
                        {
                            hash["error_" + i.ToString()] = 1;
                            hash["errmsg_" + i.ToString()] = "请选择上传文件。";
                            break;
                        }
                        else if (fileinfo.Length > maxSize)
                        {
                            hash["error_" + i.ToString()] = 1;
                            hash["errmsg_" + i.ToString()] = "上传文件大小超过" + maxSize.ToString() + "(字节)，当前文件大小" + fileinfo.Length.ToString() + "(字节)";
                            break;
                        }
                        else
                        {
                            string fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));
                            //定义允许上传的文件扩展名 
                            String fileTypes = "gif,jpg,jpeg,png,bmp,rar,doc,docx,xls,xlsx,ppt,pptx,pdf";
                            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
                            {
                                hash["error_" + i.ToString()] = 1;
                                hash["errmsg_" + i.ToString()] = "上传文件扩展名是不允许的扩展名。";
                                break;
                            }
                            else
                            {
                                //String ymd = DateTime.Now.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                                String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                                String newFullFileName = Path.Combine(dirTempPath, newFileName + fileExt);
                                fileinfo.CopyTo(newFullFileName, true);

                                hash["error"] = 0;
                                hash["errmsg"] = "上传成功";
                                hash["error_" + i.ToString()] = 0;
                                hash["errmsg_" + i.ToString()] = "上传成功";
                                hash["path_" + i.ToString()] = newFullFileName;
                            }
                        }
                        fileinfo.Delete();
                    }
                    return hash;
                });
            return task;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
