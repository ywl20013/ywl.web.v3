using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ld.WX.Models
{
    using Ywl.Data.Entity;
    using Ywl.Data.Entity.Models;

    //using Ywl.Web.Mvc;

    [Description(Title = "安全审核附件", Description = "安全审核附件")]
    public class SafetyRecordAttachment : NamedEntity
    {

        /// <summary>
        /// 创建者; 创建者，创建者的Id
        /// </summary>
        [Display(Name = "创建者", Description = "创建者，创建者的Id")]

        public int? Creator { get; set; }

        /// <summary>
        /// 创建时间; 创建时间
        /// </summary>
        [Display(Name = "创建时间", Description = "创建时间")]

        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 说明; 说明
        /// </summary>
        [Display(Name = "说明", Description = "说明")]
        [MaxLength(256)]
        public string Description { get; set; }

        /// <summary>
        /// 排序号; 排序号
        /// </summary>
        [Display(Name = "排序号", Description = "排序号")]
        [MaxLength(10)]
        public string Orderno { get; set; }

        /// <summary>
        /// 单据Id; 单据Id
        /// </summary>
        [Display(Name = "单据Id", Description = "单据Id")]

        public int? RId { get; set; }

        /// <summary>
        /// 上传到服务器后的地址; 上传到服务器后的地址
        /// </summary>
        [Display(Name = "上传到服务器后的地址", Description = "上传到服务器后的地址")]
        [MaxLength(500)]
        public string Url { get; set; }

        /// <summary>
        /// 附件分类; 附件分类
        /// </summary>
        [Display(Name = "附件分类", Description = "附件分类")]
        [MaxLength(50)]
        public string Category { get; set; }

        /// <summary>
        /// 文件后缀; 文件后缀
        /// </summary>
        [Display(Name = "文件后缀", Description = "文件后缀")]
        [MaxLength(50)]
        public string FileExt { get; set; }

        /// <summary>
        /// 文件大小; 文件大小
        /// </summary>
        [Display(Name = "文件大小", Description = "文件大小")]

        public int? FileSize { get; set; }

        public SafetyRecordAttachment()
        {
            this.CreateTime = DateTime.Now;
        }

        public SafetyRecordAttachment(SafetyRecordAttachment source) { this.CopyFrom(source); }

        public void CopyFrom(SafetyRecordAttachment source)
        {
            base.CopyFrom(source);

            //this.Id = source.Id;
            //this.Status = source.Status;
            //this.Name = source.Name;
            this.Creator = source.Creator;
            this.CreateTime = source.CreateTime;
            this.Description = source.Description;
            this.Orderno = source.Orderno;
            this.RId = source.RId;
            this.Url = source.Url;
            this.Category = source.Category;
            this.FileExt = source.FileExt;
            this.FileSize = source.FileSize;
        }

    }

    public class SafetyRecordAttachmentOracleMap : EntityTypeConfiguration<SafetyRecordAttachment>
    {
        public SafetyRecordAttachmentOracleMap(string schemaName)
        {
            this.ToTable("tb_sc_attachments".ToUpper(), schemaName);
            this.HasKey(e => e.Id);
            this.Property(e => e.Id).HasColumnName("Id".ToUpper());
            this.Property(e => e.Status).HasColumnName("Status".ToUpper());
            this.Property(e => e.Name).HasColumnName("Name".ToUpper());
            this.Property(e => e.Creator).HasColumnName("Creator".ToUpper());
            this.Property(e => e.CreateTime).HasColumnName("CreateTime".ToUpper());
            this.Property(e => e.Description).HasColumnName("Description".ToUpper());
            this.Property(e => e.Orderno).HasColumnName("Orderno".ToUpper());
            this.Property(e => e.RId).HasColumnName("RId".ToUpper());
            this.Property(e => e.Url).HasColumnName("Url".ToUpper());
            this.Property(e => e.Category).HasColumnName("Category".ToUpper());
            this.Property(e => e.FileExt).HasColumnName("FileExt".ToUpper());
            this.Property(e => e.FileSize).HasColumnName("FileSize".ToUpper());
        }
    }
    public class SafetyRecordAttachmentMSSqlMap : EntityTypeConfiguration<SafetyRecordAttachment>
    {
        public SafetyRecordAttachmentMSSqlMap(string schemaName)
        {
            this.ToTable("tb_sc_attachments", schemaName);
            this.HasKey(e => e.Id);
            this.Property(e => e.Id).HasColumnName("Id");
            this.Property(e => e.Status).HasColumnName("Status");
            this.Property(e => e.Name).HasColumnName("Name");
            this.Property(e => e.Creator).HasColumnName("Creator");
            this.Property(e => e.CreateTime).HasColumnName("CreateTime");
            this.Property(e => e.Description).HasColumnName("Description");
            this.Property(e => e.Orderno).HasColumnName("Orderno");
            this.Property(e => e.RId).HasColumnName("RId");
            this.Property(e => e.Url).HasColumnName("Url");
            this.Property(e => e.Category).HasColumnName("Category");
            this.Property(e => e.FileExt).HasColumnName("FileExt");
            this.Property(e => e.FileSize).HasColumnName("FileSize");
        }
    }
}

/*

{
    "isLdEntity":false,
    "nameSpace":"Ld.WX.Models",
    "entityName":"SafetyRecordAttachment",
    "inheritedFrom":"NamedEntity",
    "entityTitle":"安全审核附件",
    "entityDesc":"安全审核附件",
    "mappedTable":"tb_sc_attachments",
    "mappedTableKey":"id",
    "fields":[
        {
            "id":0,
            "inherited":true,
            "mapped":true,
            "name":"Id",
            "mappedName":"Id",
            "title":"Id",
            "description":"主键，自增",
            "dataType":"int",
            "length":4,
            "scale":0
        },
        {
            "id":1,
            "name":"Status",
            "mapped":true,
            "mappedName":"Status",
            "title":"状态",
            "description":"数据状态，可用值：normal、deleted",
            "dataType":"string",
            "length":50,
            "scale":0,
            "inherited":true
        },
        {
            "id":2,
            "name":"Name",
            "mapped":true,
            "mappedName":"Name",
            "title":"名称",
            "description":"名称，数据行名称",
            "dataType":"string",
            "length":50,
            "scale":0,
            "inherited":true
        },
        {
            "id":3,
            "name":"Creator",
            "mapped":true,
            "mappedName":"Creator",
            "title":"创建者",
            "description":"创建者，创建者的Id",
            "dataType":"int",
            "length":4,
            "scale":0,
            "nullable":true
        },
        {
            "id":4,
            "name":"CreateTime",
            "mapped":true,
            "mappedName":"CreateTime",
            "title":"创建时间",
            "description":"创建时间",
            "dataType":"datetime",
            "length":8,
            "scale":4,
            "nullable":true
        },
        {
            "id":5,
            "name":"Description",
            "mapped":true,
            "mappedName":"Description",
            "title":"说明",
            "description":"说明",
            "dataType":"string",
            "nullable":true,
            "length":256,
            "scale":0
        },
        {
            "id":6,
            "name":"Orderno",
            "mapped":true,
            "mappedName":"Orderno",
            "title":"排序号",
            "description":"排序号",
            "dataType":"string",
            "length":10,
            "scale":0
        },
        {
            "dataType":"int",
            "length":4,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"RId",
            "title":"单据Id",
            "mappedName":"RId",
            "description":"单据Id"
        },
        {
            "dataType":"string",
            "length":"500",
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"Url",
            "title":"上传到服务器后的地址",
            "mappedName":"Url",
            "description":"上传到服务器后的地址"
        },
        {
            "dataType":"string",
            "length":50,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"Category",
            "title":"附件分类",
            "mappedName":"Category",
            "description":"附件分类"
        },
        {
            "dataType":"string",
            "length":50,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"FileExt",
            "title":"文件后缀",
            "mappedName":"FileExt",
            "description":"文件后缀"
        },
        {
            "dataType":"int",
            "length":4,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"FileSize",
            "title":"文件大小",
            "mappedName":"FileSize",
            "description":"文件大小"
        }
    ]
}


*/

/*
            #region ===== 安全审核附件 (tb_sc_attachments) =====

            tableName = "tb_sc_attachments";
            ret = await this.InternalCheckAddTable(tableName, "安全审核附件");
            result += "<br />" + "添加表 " + tableName + " " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Id", "int", "4", "0", "Id");
            result += "<br />" + "为表 " + tableName + " 添加字段 Id int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Status", "string", "50", "0", "状态");
            result += "<br />" + "为表 " + tableName + " 添加字段 Status string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Name", "string", "50", "0", "名称");
            result += "<br />" + "为表 " + tableName + " 添加字段 Name string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Creator", "int", "4", "0", "创建者");
            result += "<br />" + "为表 " + tableName + " 添加字段 Creator int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "CreateTime", "datetime", "8", "4", "创建时间");
            result += "<br />" + "为表 " + tableName + " 添加字段 CreateTime datetime " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Description", "string", "256", "0", "说明");
            result += "<br />" + "为表 " + tableName + " 添加字段 Description string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Orderno", "string", "10", "0", "排序号");
            result += "<br />" + "为表 " + tableName + " 添加字段 Orderno string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "RId", "int", "4", "0", "单据Id");
            result += "<br />" + "为表 " + tableName + " 添加字段 RId int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Url", "string", "500", "0", "上传到服务器后的地址");
            result += "<br />" + "为表 " + tableName + " 添加字段 Url string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Category", "string", "50", "0", "附件分类");
            result += "<br />" + "为表 " + tableName + " 添加字段 Category string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "FileExt", "string", "50", "0", "文件后缀");
            result += "<br />" + "为表 " + tableName + " 添加字段 FileExt string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "FileSize", "int", "4", "0", "文件大小");
            result += "<br />" + "为表 " + tableName + " 添加字段 FileSize int " + (ret == "" ? "OK." : ret);

            #endregion ===== 安全审核附件 (tb_sc_attachments) =====
*/

/*
            #region ===== 安全审核附件 (tb_sc_attachments) =====

            tableName = "tb_sc_attachments";
            ret = await this.InternalCheckAddTable(tableName, "安全审核附件");
            result.Add("添加表 " + tableName + " " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Id", "int", "4", "0", "Id");
            result.Add("为表 " + tableName + " 添加字段 Id int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Status", "string", "50", "0", "状态");
            result.Add("为表 " + tableName + " 添加字段 Status string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Name", "string", "50", "0", "名称");
            result.Add("为表 " + tableName + " 添加字段 Name string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Creator", "int", "4", "0", "创建者");
            result.Add("为表 " + tableName + " 添加字段 Creator int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "CreateTime", "datetime", "8", "4", "创建时间");
            result.Add("为表 " + tableName + " 添加字段 CreateTime datetime " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Description", "string", "256", "0", "说明");
            result.Add("为表 " + tableName + " 添加字段 Description string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Orderno", "string", "10", "0", "排序号");
            result.Add("为表 " + tableName + " 添加字段 Orderno string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "RId", "int", "4", "0", "单据Id");
            result.Add("为表 " + tableName + " 添加字段 RId int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Url", "string", "500", "0", "上传到服务器后的地址");
            result.Add("为表 " + tableName + " 添加字段 Url string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Category", "string", "50", "0", "附件分类");
            result.Add("为表 " + tableName + " 添加字段 Category string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "FileExt", "string", "50", "0", "文件后缀");
            result.Add("为表 " + tableName + " 添加字段 FileExt string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "FileSize", "int", "4", "0", "文件大小");
            result.Add("为表 " + tableName + " 添加字段 FileSize int " + (ret == "" ? "OK." : ret));

            #endregion ===== 安全审核附件 (tb_sc_attachments) =====
*/
