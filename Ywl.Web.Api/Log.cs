using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Ywl.Web.Api
{
    public class Log
    {
        private string Path { get; set; }
        public string FileName { get; set; }
        public Log()
        {
            this.Path = System.Web.HttpContext.Current.Server.MapPath("~");
        }
        public Log(string filename, string path)
        {
            if (path != null) this.Path = path;
            else this.Path = System.Web.HttpContext.Current.Server.MapPath("~");
            this.FileName = filename;
        }
        public Log log(string format, params object[] args)
        {
            var filePath = this.Path + "\\logs\\" + DateTime.Now.ToString("yyyy-MM") + "\\";
            if (System.Web.HttpContext.Current == null) return this;
            //filePath = HttpContext.Current.Server.MapPath(filePath);
            if (CreateFolderIfNeeded(filePath))
            {
                var log_file = "";
                if (this.FileName == null)
                    log_file = filePath + "log." + DateTime.Now.ToString("yyyy.MM.dd HH") + ".log";
                else
                    log_file = filePath + this.FileName + "." + DateTime.Now.ToString("yyyy.MM.dd HH") + ".log";
                lock (this)
                {
                    //指定true表示追加
                    using (System.IO.TextWriter writer = new System.IO.StreamWriter(log_file, true))
                    {
                        writer.Write(format, args);
                    }
                }
            }

            return this;
        }
        public Log lograw(string value)
        {
            var filePath = this.Path + "\\logs\\" + DateTime.Now.ToString("yyyy-MM") + "\\";
            if (System.Web.HttpContext.Current == null) return this;
            //filePath = HttpContext.Current.Server.MapPath(filePath);
            if (CreateFolderIfNeeded(filePath))
            {
                this.FileName = filePath + "log." + DateTime.Now.ToString("yyyy.MM.dd HH") + ".log";

                lock (this)
                {
                    //指定true表示追加
                    using (System.IO.TextWriter writer = new System.IO.StreamWriter(this.FileName, true))
                    {
                        writer.Write(value);
                    }
                }
            }

            return this;
        }
        public Log log2(string format, params object[] args)
        {
            return this.log("[" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "] " + format, args);
        }
        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!System.IO.Directory.Exists(path))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                catch (Exception e)
                {
                    //TODO：处理异常
                    System.Diagnostics.Debug.WriteLine("Create Folder \"" + path + "\" Error: " + e.Message);
                    result = false;
                }
            }
            return result;
        }
    }
}