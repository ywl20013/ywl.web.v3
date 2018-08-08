using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ld.WX
{
    public class Log
    {
        private string Path { get; set; }
        public string FileName { get; set; }
        public Log()
        {
            this.Path = HttpContext.Current.Server.MapPath("~");
        }
        public Log(string path)
        {
            this.Path = path;
        }
        public Log log(string format, params object[] args)
        {
            var filePath = this.Path + "\\logs\\" + DateTime.Now.ToString("yyyy\\MM") + "\\";
            if (HttpContext.Current == null) return this;
            //filePath = HttpContext.Current.Server.MapPath(filePath);
            if (CreateFolderIfNeeded(filePath))
            {
                this.FileName = filePath + "log." + DateTime.Now.ToString("yyyy.MM.dd HH") + ".log";

                lock (this)
                {
                    //指定true表示追加
                    using (System.IO.TextWriter writer = new System.IO.StreamWriter(this.FileName, true))
                    {
                        if (args.Count() > 0)
                            writer.Write(format, args);
                        else
                            writer.Write(format);
                    }
                }
            }

            return this;
        }
        public Log lograw(string value)
        {
            var filePath = this.Path + "\\logs\\" + DateTime.Now.ToString("yyyy-MM") + "\\";
            if (HttpContext.Current == null) return this;
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