using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ywl.Data.Entity.Models
{
    using Ywl.Data.Entity;
    using Ywl.Data.Entity.Models;
    //using Ywl.Web.Mvc;

    [Description(Title = "系统模块动作", Description = "系统模块,基础权限,基础功能")]
    public class Action : Ywl.Data.Entity.Models.NamedEntity
    {
        /// <summary>
        /// 创建者; 创建者，创建者的Id
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [Display(Name = "创建者", Description = "创建者，创建者的Id")]

        public int? Creator { get; set; }

        /// <summary>
        /// 创建时间; 创建时间
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
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
        /// 模块Id; 模块Id
        /// </summary>
        [Display(Name = "模块Id", Description = "模块Id")]

        public int? MoudleId { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        [NotMapped]
        public Models.Moudle Moudle { get; set; }

        /// <summary>
        /// 控制器; 控制器
        /// </summary>
        [Display(Name = "控制器", Description = "控制器")]
        [MaxLength(50)]
        public string Controller { get; set; }

        /// <summary>
        /// 控制器命名空间; 控制器命名空间
        /// </summary>
        [Display(Name = "控制器命名空间", Description = "控制器命名空间")]
        [MaxLength(50)]
        public string CtrlNamespace { get; set; }

        /// <summary>
        /// 控制器描述; 控制器描述
        /// </summary>
        [Display(Name = "控制器描述", Description = "控制器描述")]
        [MaxLength(50)]
        public string ControllerDescription { get; set; }

        /// <summary>
        /// 标题; 标题
        /// </summary>
        [Display(Name = "标题", Description = "标题")]
        [MaxLength(50)]
        public string Title { get; set; }

        public Action()
        {
            this.CreateTime = DateTime.Now;
        }

        public Action(Action source) { this.CopyFrom(source); }

        public void CopyFrom(Action source)
        {
            base.CopyFrom(source);

            //this.Id = source.Id;
            //this.Status = source.Status;
            //this.Name = source.Name;
            this.Creator = source.Creator;
            this.CreateTime = source.CreateTime;
            this.Description = source.Description;
            this.Orderno = source.Orderno;
            this.MoudleId = source.MoudleId;
            this.Controller = source.Controller;
            this.CtrlNamespace = source.CtrlNamespace;
            this.ControllerDescription = source.ControllerDescription;
            this.Title = source.Title;
        }

    }

    public class ActionOracleMap : EntityTypeConfiguration<Action>
    {
        public ActionOracleMap(string schemaName)
        {
            this.ToTable("tb_sys_Actions".ToUpper(), schemaName);
            this.HasKey(e => e.Id);
            this.Property(e => e.Id).HasColumnName("Id".ToUpper());
            this.Property(e => e.Status).HasColumnName("Status".ToUpper());
            this.Property(e => e.Name).HasColumnName("Name".ToUpper());
            this.Ignore(e => e.Creator);
            this.Ignore(e => e.CreateTime);
            this.Property(e => e.Description).HasColumnName("Description".ToUpper());
            this.Property(e => e.Orderno).HasColumnName("Orderno".ToUpper());
            this.Property(e => e.MoudleId).HasColumnName("MoudleId".ToUpper());
            this.Property(e => e.Controller).HasColumnName("Controller".ToUpper());
            this.Property(e => e.CtrlNamespace).HasColumnName("CtrlNamespace".ToUpper());
            this.Property(e => e.ControllerDescription).HasColumnName("ControllerDescription".ToUpper());
            this.Property(e => e.Title).HasColumnName("Title".ToUpper());
        }
    }
    public class ActionMSSqlMap : EntityTypeConfiguration<Action>
    {
        public ActionMSSqlMap(string schemaName)
        {
            this.ToTable("tb_sys_Actions", schemaName);
            this.HasKey(e => e.Id);
            this.Property(e => e.Id).HasColumnName("Id");
            this.Property(e => e.Status).HasColumnName("Status");
            this.Property(e => e.Name).HasColumnName("Name");
            this.Ignore(e => e.Creator);
            this.Ignore(e => e.CreateTime);
            this.Property(e => e.Description).HasColumnName("Description");
            this.Property(e => e.Orderno).HasColumnName("Orderno");
            this.Property(e => e.MoudleId).HasColumnName("MoudleId");
            this.Property(e => e.Controller).HasColumnName("Controller");
            this.Property(e => e.CtrlNamespace).HasColumnName("CtrlNamespace");
            this.Property(e => e.ControllerDescription).HasColumnName("ControllerDescription");
            this.Property(e => e.Title).HasColumnName("Title");
        }
    }
}

/*

{
    "isLdEntity":false,
    "nameSpace":"Ywl.Data.Entity.Models",
    "entityName":"Action",
    "inheritedFrom":"Ywl.Data.Entity.Models.NamedEntity",
    "entityTitle":"系统模块动作",
    "entityDesc":"系统模块,
    基础权限,
    基础功能",
    "mappedTable":"tb_sys_Actions",
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
            "inherited":false
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
            "mapped":false,
            "mappedName":"Creator",
            "title":"创建者",
            "description":"创建者，创建者的Id",
            "dataType":"int",
            "length":4,
            "scale":0,
            "nullable":true,
            "inherited":false
        },
        {
            "id":4,
            "name":"CreateTime",
            "mapped":false,
            "mappedName":"CreateTime",
            "title":"创建时间",
            "description":"创建时间",
            "dataType":"datetime",
            "length":8,
            "scale":4,
            "nullable":true,
            "inherited":false
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
            "dataType":"int",
            "length":4,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"MoudleId",
            "title":"模块Id",
            "mappedName":"MoudleId",
            "description":"模块Id"
        },
        {
            "dataType":"string",
            "length":50,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"Controller",
            "title":"控制器",
            "mappedName":"Controller",
            "description":"控制器"
        },
        {
            "dataType":"string",
            "length":50,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"CtrlNamespace",
            "title":"控制器命名空间",
            "mappedName":"CtrlNamespace",
            "description":"控制器命名空间"
        },
        {
            "dataType":"string",
            "length":50,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"ControllerDescription",
            "title":"控制器描述",
            "mappedName":"ControllerDescription",
            "description":"控制器描述"
        },
        {
            "dataType":"string",
            "length":50,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"Title",
            "title":"标题",
            "mappedName":"Title",
            "description":"标题"
        }
    ]
}


*/

/*
            #region ===== 系统模块,基础权限,基础功能 (tb_sys_Actions) =====

            tableName = "tb_sys_Actions";
            ret = await this.InternalCheckAddTable(tableName, "系统模块,基础权限,基础功能");
            result += "<br />" + "添加表 " + tableName + " " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Id", "int", "4", "0", "Id");
            result += "<br />" + "为表 " + tableName + " 添加字段 Id int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Status", "string", "50", "0", "状态");
            result += "<br />" + "为表 " + tableName + " 添加字段 Status string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Name", "string", "50", "0", "名称");
            result += "<br />" + "为表 " + tableName + " 添加字段 Name string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Description", "string", "256", "0", "说明");
            result += "<br />" + "为表 " + tableName + " 添加字段 Description string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Orderno", "string", "10", "0", "排序号");
            result += "<br />" + "为表 " + tableName + " 添加字段 Orderno string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "MoudleId", "int", "4", "0", "模块Id");
            result += "<br />" + "为表 " + tableName + " 添加字段 MoudleId int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Controller", "string", "50", "0", "控制器");
            result += "<br />" + "为表 " + tableName + " 添加字段 Controller string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "CtrlNamespace", "string", "50", "0", "控制器命名空间");
            result += "<br />" + "为表 " + tableName + " 添加字段 CtrlNamespace string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "ControllerDescription", "string", "50", "0", "控制器描述");
            result += "<br />" + "为表 " + tableName + " 添加字段 ControllerDescription string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Title", "string", "50", "0", "标题");
            result += "<br />" + "为表 " + tableName + " 添加字段 Title string " + (ret == "" ? "OK." : ret);

            #endregion ===== 系统模块,基础权限,基础功能 (tb_sys_Actions) =====
*/

/*
            #region ===== 系统模块,基础权限,基础功能 (tb_sys_Actions) =====

            tableName = "tb_sys_Actions";
            ret = await this.InternalCheckAddTable(tableName, "系统模块,基础权限,基础功能");
            result.Add("添加表 " + tableName + " " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Id", "int", "4", "0", "Id");
            result.Add("为表 " + tableName + " 添加字段 Id int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Status", "string", "50", "0", "状态");
            result.Add("为表 " + tableName + " 添加字段 Status string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Name", "string", "50", "0", "名称");
            result.Add("为表 " + tableName + " 添加字段 Name string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Description", "string", "256", "0", "说明");
            result.Add("为表 " + tableName + " 添加字段 Description string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Orderno", "string", "10", "0", "排序号");
            result.Add("为表 " + tableName + " 添加字段 Orderno string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "MoudleId", "int", "4", "0", "模块Id");
            result.Add("为表 " + tableName + " 添加字段 MoudleId int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Controller", "string", "50", "0", "控制器");
            result.Add("为表 " + tableName + " 添加字段 Controller string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "CtrlNamespace", "string", "50", "0", "控制器命名空间");
            result.Add("为表 " + tableName + " 添加字段 CtrlNamespace string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "ControllerDescription", "string", "50", "0", "控制器描述");
            result.Add("为表 " + tableName + " 添加字段 ControllerDescription string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Title", "string", "50", "0", "标题");
            result.Add("为表 " + tableName + " 添加字段 Title string " + (ret == "" ? "OK." : ret));

            #endregion ===== 系统模块,基础权限,基础功能 (tb_sys_Actions) =====
*/
