
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ywl.Data.Entity;

namespace Ywl.Web.Api.Models
{ 

    [Description(Title = "系统模块", Description = "系统模块,基础权限")]
    public class Moudle : Ywl.Data.Entity.Models.BaseMoudle
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
        /// 类型; 类型
        /// </summary>
        [Display(Name = "类型", Description = "类型")]
        [MaxLength(50)]
        public string MoudleType { get; set; }

        public Moudle() { }

        public Moudle(Moudle source) { this.CopyFrom(source); }

        public void CopyFrom(Moudle source)
        {
            base.CopyFrom(source);

            //this.Id = source.Id;
            //this.Status = source.Status;
            //this.Name = source.Name;
            this.Creator = source.Creator;
            this.CreateTime = source.CreateTime;
            this.Description = source.Description;
            this.Orderno = source.Orderno;
            //this.ParentId = source.ParentId;
            //this.Url = source.Url;
            this.MoudleType = source.MoudleType;
            //this.Category = source.Category;
            //this.NeedPower = source.NeedPower;
            //this.NameSpace = source.NameSpace;
            //this.Parent = source.Parent;
            //this.Children = source.Children;
            //this.HierarchicalPath = source.HierarchicalPath;            
        }

    }

    public class MoudleOracleMap : EntityTypeConfiguration<Moudle>
    {
        public MoudleOracleMap(string schemaName)
        {
            this.ToTable("tb_sys_Moudles".ToUpper(), schemaName);
            this.HasKey(e => e.Id);
            this.Property(e => e.Id).HasColumnName("Id".ToUpper());
            this.Property(e => e.Status).HasColumnName("Status".ToUpper());
            this.Property(e => e.Name).HasColumnName("Name".ToUpper());
            this.Property(e => e.Creator).HasColumnName("Creator".ToUpper());
            this.Property(e => e.CreateTime).HasColumnName("CreateTime".ToUpper());
            this.Property(e => e.Description).HasColumnName("Description".ToUpper());
            this.Property(e => e.Orderno).HasColumnName("Orderno".ToUpper());
            this.Property(e => e.ParentId).HasColumnName("PId".ToUpper());
            this.Property(e => e.Url).HasColumnName("Url".ToUpper());
            this.Property(e => e.MoudleType).HasColumnName("MType".ToUpper());
            this.Property(e => e.Category).HasColumnName("Category".ToUpper());
            this.Property(e => e.NeedPower).HasColumnName("NeedPower".ToUpper());
            this.Property(e => e.NameSpace).HasColumnName("NameSpace".ToUpper());
            this.Ignore(e => e.Parent);
            this.Ignore(e => e.Children);
            this.Property(e => e.HierarchicalPath).HasColumnName("Path".ToUpper());
        }
    }
    public class MoudleMSSqlMap : EntityTypeConfiguration<Moudle>
    {
        public MoudleMSSqlMap(string schemaName)
        {
            this.ToTable("tb_sys_Moudles", schemaName);
            this.HasKey(e => e.Id);
            this.Property(e => e.Id).HasColumnName("Id");
            this.Property(e => e.Status).HasColumnName("Status");
            this.Property(e => e.Name).HasColumnName("Name");
            this.Property(e => e.Creator).HasColumnName("Creator");
            this.Property(e => e.CreateTime).HasColumnName("CreateTime");
            this.Property(e => e.Description).HasColumnName("Description");
            this.Property(e => e.Orderno).HasColumnName("Orderno");
            this.Property(e => e.ParentId).HasColumnName("PId");
            this.Property(e => e.Url).HasColumnName("Url");
            this.Property(e => e.MoudleType).HasColumnName("MType");
            this.Property(e => e.Category).HasColumnName("Category");
            this.Property(e => e.NeedPower).HasColumnName("NeedPower");
            this.Property(e => e.NameSpace).HasColumnName("NameSpace");
            this.Ignore(e => e.Parent);
            this.Ignore(e => e.Children);
            this.Property(e => e.HierarchicalPath).HasColumnName("Path");
        }
    }
}

/*

{
    "isLdEntity":false,
    "nameSpace":"Ywl.Web.Mvc.Models",
    "entityName":"Moudle",
    "inheritedFrom":"Ywl.Web.Mvc.Models.BaseMoudle",
    "entityTitle":"系统模块",
    "entityDesc":"系统模块,
    基础权限",
    "mappedTable":"tb_sys_Moudles",
    "mappedTableKey":"id",
    "fields":[
        {
            "id":0,
            "name":"Id",
            "mapped":true,
            "mappedName":"Id",
            "title":"Id",
            "description":"主键，自增",
            "dataType":"int",
            "length":4,
            "scale":0,
            "inherited":true
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
            "name":"ParentId",
            "title":"上一级",
            "dataType":"int",
            "mappedName":"PId",
            "description":"上一级",
            "mapped":true,
            "nullable":true,
            "inherited":true
        },
        {
            "name":"Url",
            "title":"链接",
            "dataType":"string",
            "length":"256",
            "mappedName":"Url",
            "description":"链接",
            "mapped":true,
            "inherited":true
        },
        {
            "name":"MoudleType",
            "mappedName":"MType",
            "title":"类型",
            "dataType":"string",
            "length":"50",
            "description":"类型",
            "mapped":true
        },
        {
            "name":"Category",
            "title":"分类",
            "dataType":"string",
            "length":"50",
            "mappedName":"Category",
            "description":"分类",
            "mapped":true,
            "inherited":true
        },
        {
            "name":"NeedPower",
            "title":"是否需要权限",
            "dataType":"bool",
            "length":"1",
            "scale":"0",
            "mappedName":"NeedPower",
            "description":"是否需要权限",
            "mapped":true,
            "nullable":true,
            "inherited":true
        },
        {
            "name":"NameSpace",
            "title":"命名空间",
            "dataType":"string",
            "length":"256",
            "mappedName":"NameSpace",
            "description":"命名空间",
            "mapped":true,
            "inherited":true
        },
        {
            "dataType":"ParentChildEntity",
            "length":50,
            "scale":0,
            "name":"Parent",
            "mappedName":"",
            "inherited":true,
            "description":"父文档",
            "title":"父文档"
        },
        {
            "dataType":"List<ParentChildEntity>",
            "length":50,
            "scale":0,
            "name":"Children",
            "inherited":true,
            "mappedName":"",
            "description":"子文档集",
            "title":"子文档集"
        },
        {
            "dataType":"string",
            "length":50,
            "scale":0,
            "name":"HierarchicalPath",
            "inherited":true,
            "title":"层次路径",
            "mappedName":"Path",
            "description":"层次路径",
            "mapped":true
        }
    ]
}


*/

/*
            #region ===== 系统模块,基础权限 (tb_sys_Moudles) =====

            tableName = "tb_sys_Moudles";
            ret = await this.InternalCheckAddTable(tableName, "系统模块,基础权限");
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

            ret = await this.InternalCheckAddTableField(tableName, "PId", "int", "undefined", "undefined", "上一级");
            result += "<br />" + "为表 " + tableName + " 添加字段 PId int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Url", "string", "256", "undefined", "链接");
            result += "<br />" + "为表 " + tableName + " 添加字段 Url string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "MType", "string", "50", "undefined", "类型");
            result += "<br />" + "为表 " + tableName + " 添加字段 MType string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Category", "string", "50", "undefined", "分类");
            result += "<br />" + "为表 " + tableName + " 添加字段 Category string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "NeedPower", "bool", "1", "0", "是否需要权限");
            result += "<br />" + "为表 " + tableName + " 添加字段 NeedPower bool " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "NameSpace", "string", "256", "undefined", "命名空间");
            result += "<br />" + "为表 " + tableName + " 添加字段 NameSpace string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Path", "string", "50", "0", "层次路径");
            result += "<br />" + "为表 " + tableName + " 添加字段 Path string " + (ret == "" ? "OK." : ret);

            #endregion ===== 系统模块,基础权限 (tb_sys_Moudles) =====
*/
