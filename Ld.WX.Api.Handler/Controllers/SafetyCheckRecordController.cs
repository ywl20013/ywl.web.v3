using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Ld.WX.Api.Handler.Controllers
{
    using Ywl.Web.Api.Controllers;
    using Ywl.Web.Api.Handler.Controllers;

    public class SafetyCheckRecordController : DbApiController<Models.DbContext>
    {
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
        // POST api/values
        public PostResult Post()
        {
            PostResult result = new PostResult();
            // 检查是否是 multipart/form-data
            if (!Request.Content.IsMimeMultipartContent("form-data"))
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            //文件保存目录路径
            string SaveTempPath = "~/uploads/";
            string dirTempPath = HttpContext.Current.Server.MapPath(SaveTempPath);
            if (!System.IO.Directory.Exists(dirTempPath))
                System.IO.Directory.CreateDirectory(dirTempPath);

            var files = HttpContext.Current.Request.Files;
            result.FileCount = files.Count;
            result.Success = false;
            result.Error = "";

            var creator = HttpContext.Current.Request["creator"];
            var _finder = HttpContext.Current.Request["finder"];
            var findTime = HttpContext.Current.Request["findTime"];
            var content = HttpContext.Current.Request["content"];
            var sourceType = HttpContext.Current.Request["sourceType"];
            sourceType = sourceType == null || sourceType == "undefine" ? HttpContext.Current.Request["cate"] : sourceType;
            var souceSubType = HttpContext.Current.Request["souceSubType"];
            souceSubType = souceSubType == null || souceSubType == "undefine" ? HttpContext.Current.Request["subcate"] : souceSubType;
            var dangerType = HttpContext.Current.Request["dangerType"];
            var isDanger = Ywl.Data.Entity.Utils.StrToBool(HttpContext.Current.Request["isDanger"], false);
            if (dangerType == null || dangerType == "undefine")
            {
                dangerType = "";
                if (isDanger.HasValue && isDanger.Value) dangerType = "隐患";
            }
            var responsiblePerson = HttpContext.Current.Request["responsiblePerson"];
            var breakRulesType = HttpContext.Current.Request["breakRulesType"];

            var trans = db.Database.BeginTransaction();
            try
            {
                //接收数据
                var entity = new Ld.WX.Models.SafetyRecord();
                entity.Synced = false;
                entity.CreateTime = DateTime.Now;
                entity.Creator = Ywl.Data.Entity.Utils.StrToInt(creator, null);
                entity.Finder = Ywl.Data.Entity.Utils.StrToInt(_finder, null);
                entity.BreakRulesType = breakRulesType;
                if (entity.Finder == null)
                {
                    entity.Finder = entity.Creator;
                }
                var uid = entity.Finder.ToString();
                if (uid != null)
                {
                    Ywl.Data.Entity.Models.User user = null;
                    if (uid.Length == 8 && uid.IndexOf("8") == 0)
                    {
                        user = db.Users.Where(e => e.Account == uid).FirstOrDefault();
                    }
                    else
                    {
                        var _uid = Ywl.Data.Entity.Utils.StrToInt(uid, null);
                        user = db.Users.Find(_uid);
                    }
                    if (user != null)
                    {
                        entity.Creator = user.Id;
                        entity.FindDepartment = user.DepId;
                    }
                }



                entity.FindTime = DateTime.FromBinary(Ywl.Data.Entity.Utils.StrToLong(findTime, DateTime.Now.Ticks).Value);
                entity.PlanFinishTime = entity.FindTime.Value.AddDays(15);
                entity.Content = content;
                entity.SourceType = Ywl.Data.Entity.Utils.StrToInt(sourceType, null);
                entity.SouceSubType = Ywl.Data.Entity.Utils.StrToInt(souceSubType, null);
                entity.DangerType = dangerType;
                entity.FlowStatus = "新创建";

                uid = responsiblePerson;
                if (uid != null)
                {
                    Ywl.Data.Entity.Models.User user = null;
                    if (uid.Length == 8 && uid.IndexOf("8") == 0)
                    {
                        user = db.Users.Where(e => e.Account == uid).FirstOrDefault();
                    }
                    else
                    {
                        var _uid = Ywl.Data.Entity.Utils.StrToInt(uid, null);
                        user = db.Users.Find(_uid);
                    }
                    if (user != null)
                    {
                        entity.ResponsiblePerson = user.Id;
                        entity.ResponsiblePersonName = user.Name;
                        entity.ResponsibleDepartment = user.DepId;
                        entity.ResponsibleClass = user.GroupId;
                    }
                }

                db.SafetyRecords.Add(entity);

                var ret = db.SaveChanges();

                result.Id = entity.Id;

                //接收附件
                for (int i = 0; i < files.Count; i++)
                {
                    var fileResult = new PostResult.FileResult();
                    fileResult.Success = false;
                    result.Files.Add(fileResult);

                    var file = files[i];
                    fileResult.FileName = file.FileName;
                    fileResult.ContentType = file.ContentType;
                    fileResult.ContentLength = file.ContentLength;

                    if (file.ContentLength <= 0)
                    {
                        fileResult.Error = "文件大小为0(字节)。";
                        continue;
                    }
                    // 最大文件大小 
                    // 4096000 = 4M
                    // 10240000 = 10M
                    // 2048000000 = 2G
                    int maxSize = 4096000;

                    if (file.ContentLength > maxSize)
                    {
                        result.Success = false;
                        fileResult.Error = "上传文件大小超过" + maxSize.ToString() + "(字节)，当前文件大小" + file.ContentLength.ToString() + "(字节)";
                        break;
                    }
                    else
                    {
                        string fileExt = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                        //定义允许上传的文件扩展名 
                        String fileTypes = "gif,jpg,jpeg,png,bmp,rar,doc,docx,xls,xlsx,ppt,pptx,pdf";
                        if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
                        {
                            result.Success = false;
                            fileResult.Error = "上传文件扩展名是不允许的扩展名。";
                            break;
                        }
                        else
                        {
                            //String ymd = DateTime.Now.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            String newFullFileName = Path.Combine(dirTempPath, newFileName + fileExt);

                            file.SaveAs(newFullFileName);

                            fileResult.Success = true;
                            fileResult.Error = "上传成功";
                            fileResult.Path = SaveTempPath + newFileName + fileExt;

                            result.Error = "上传成功";
                            result.Success = true;

                            //保存附件信息到数据库
                            var attach = new Ld.WX.Models.SafetyRecordAttachment();
                            attach.RId = result.Id;
                            attach.Name = file.FileName;
                            attach.FileExt = fileExt;
                            attach.FileSize = file.ContentLength;
                            attach.Url = SaveTempPath + newFileName + fileExt;
                            attach.Category = "改前";

                            attach.Creator = entity.Creator;

                            db.SafetyRecordAttachments.Add(attach);

                            System.Threading.Thread.Sleep(10);
                        }
                    }
                }

                ret = db.SaveChanges();

                trans.Commit();
                result.Success = true;
                result.Error = "";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                result.Success = false;
                result.Error = ex.Message + "\n" + ex.StackTrace;
                return result;
            }
            return result;
        }

        public IHttpActionResult Get()
        {
            List<Ld.WX.Models.SafetyRecord> list = null;
            var creator = Ywl.Data.Entity.Utils.StrToInt(HttpContext.Current.Request["creator"], null);
            if (creator.HasValue)
            {
                list = (from t1 in db.SafetyRecords
                        where t1.Status == Ywl.Data.Entity.Models.Status.Normal
                            && t1.Creator == creator
                            && t1.FlowStatus == "新创建"
                        orderby t1.CreateTime descending
                        select t1).ToList();
            }

            var submitted = HttpContext.Current.Request["submitted"];
            if (submitted != null)
            {
                list = (from t1 in db.SafetyRecords
                        where t1.Status == Ywl.Data.Entity.Models.Status.Normal
                            && t1.FlowStatus != "新创建"
                        orderby t1.CreateTime descending
                        select t1).ToList();
            }
            var received = HttpContext.Current.Request["received"];
            if (received != null)
            {
                list = (from t1 in db.SafetyRecords
                        where t1.Status == Ywl.Data.Entity.Models.Status.Normal
                            && t1.FlowStatus != "新创建"
                            && t1.FlowStatus != "已提交"
                            && t1.FlowStatus != "督导下达"
                            && t1.FlowStatus != "上交到部门"
                            && t1.FlowStatus != "部门已下达"
                            && t1.FlowStatus != "上交到安监"
                            && t1.FlowStatus != "安监已下达"
                        orderby t1.CreateTime descending
                        select t1).ToList();
            }
            var accepted = HttpContext.Current.Request["accepted"];
            if (accepted != null)
            {
                list = (from t1 in db.SafetyRecords
                        where t1.Status == Ywl.Data.Entity.Models.Status.Normal
                            && t1.FlowStatus == "已验收"
                        orderby t1.CreateTime descending
                        select t1).ToList();
            }
            if (list == null) return StatusCode(HttpStatusCode.BadRequest);

            foreach (var item in list)
            {
                //获取附件
                var query2 = from t1 in db.SafetyRecordAttachments
                             where t1.Status == Ywl.Data.Entity.Models.Status.Normal
                             && t1.RId == item.Id
                             orderby t1.CreateTime
                             select t1;
                item.Attachments = query2.ToList();

                //获取审核记录
                var query3 = from t1 in db.SafetyRecordCheckHistories
                             where t1.RId == item.SyncId
                             orderby t1.CreateTime
                             select t1;
                item.CheckHistories = query3.ToList();

                //获取发现人信息
                var finder = db.Users.Find(item.Finder);
                if (finder != null) item.FinderName = finder.Name;
            }

            //所有数据总数
            var recordsTotal = list.Count();
            //符合条件的数据
            var recordsFiltered = list.Count();
            return Ok(new { data = list, recordsTotal, recordsFiltered });
        }
        public IHttpActionResult Get(int id)
        {
            try
            {
                var entity = db.SafetyRecords.Find(id);
                if (entity.FindDepartment != null)
                {
                    var dep = db.Organizations.Find(entity.FindDepartment);
                    if (dep != null) entity.FindDepartmentName = dep.Name;
                }
                if (entity.Finder != null)
                {
                    //                    var acnt = entity.Finder.ToString();
                    var user = db.Users.Find(entity.Finder);
                    if (user != null) entity.FinderName = user.Name;
                }
                if (entity.ResponsibleClass != null)
                {
                    var dep = db.Organizations.Find(entity.ResponsibleClass);
                    if (dep != null) entity.ResponsibleClassName = dep.Name;
                }
                if (entity.ResponsibleDepartment != null)
                {
                    var dep = db.Organizations.Find(entity.ResponsibleDepartment);
                    if (dep != null) entity.ResponsibleDepartmentName = dep.Name;
                }
                if (entity.ResponsiblePerson != null)
                {
                    var user = db.Users.Find(entity.ResponsiblePerson);
                    if (user != null) entity.ResponsiblePersonName = user.Name;
                }
                entity.Attachments = db.SafetyRecordAttachments.Where(e => e.RId == id).OrderBy(e => e.CreateTime).ToList();
                entity.CheckHistories = db.SafetyRecordCheckHistories.Where(e => e.RId == entity.SyncId).OrderBy(e => e.CreateTime).ToList();
                return Ok(entity);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
