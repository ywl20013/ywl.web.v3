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

    [Description(Title = "系统模块动作与用户关系", Description = "系统模块动作与用户关系")]
    public class UserAction : Ywl.Data.Entity.Models.Entity
    {

        /// <summary>
        /// 用户Id; 用户Id
        /// </summary>
        [Display(Name = "用户Id", Description = "用户Id")]

        public int UserId { get; set; }

        /// <summary>
        /// 动作Id; 动作Id
        /// </summary>
        [Display(Name = "动作Id", Description = "动作Id")]

        public int ActionId { get; set; }

        public UserAction()
        {

        }

        public UserAction(UserAction source) { this.CopyFrom(source); }

        public void CopyFrom(UserAction source)
        {
            base.CopyFrom(source);

            //this.Id = source.Id;
            //this.Status = source.Status;
            this.UserId = source.UserId;
            this.ActionId = source.ActionId;
        }

    }

    public class UserActionOracleMap : EntityTypeConfiguration<UserAction>
    {
        public UserActionOracleMap(string schemaName)
        {
            this.ToTable("tb_sys_UserActions".ToUpper(), schemaName);
            this.HasKey(e => e.Id);
            this.Property(e => e.Id).HasColumnName("Id".ToUpper());
            this.Ignore(e => e.Status);
            this.Property(e => e.UserId).HasColumnName("UserId".ToUpper());
            this.Property(e => e.ActionId).HasColumnName("ActionId".ToUpper());
        }
    }
    public class UserActionMSSqlMap : EntityTypeConfiguration<UserAction>
    {
        public UserActionMSSqlMap(string schemaName)
        {
            this.ToTable("tb_sys_UserActions", schemaName);
            this.HasKey(e => e.Id);
            this.Property(e => e.Id).HasColumnName("Id");
            this.Ignore(e => e.Status);
            this.Property(e => e.UserId).HasColumnName("UserId");
            this.Property(e => e.ActionId).HasColumnName("ActionId");
        }
    }
}

/*

{
    "isLdEntity":true,
    "nameSpace":"Ywl.Data.Entity.Models",
    "entityName":"UserAction",
    "inheritedFrom":"Ywl.Data.Entity.Models.Entity",
    "entityTitle":"系统模块动作与用户关系",
    "entityDesc":"系统模块动作与用户关系",
    "mappedTable":"tb_sys_UserActions",
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
            "id":2,
            "name":"Status",
            "mapped":false,
            "mappedName":"Status",
            "title":"数据状态",
            "description":"名称，数据行名称",
            "dataType":"string",
            "length":50,
            "scale":0,
            "inherited":true
        },
        {
            "dataType":"int",
            "length":4,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"UserId",
            "title":"用户Id",
            "mappedName":"UserId",
            "description":"用户Id"
        },
        {
            "dataType":"int",
            "length":4,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"ActionId",
            "title":"动作Id",
            "mappedName":"ActionId",
            "description":"动作Id"
        }
    ]
}


*/

/*
            #region ===== 系统模块动作与用户关系 (tb_sys_UserActions) =====

            tableName = "tb_sys_UserActions";
            ret = await this.InternalCheckAddTable(tableName, "系统模块动作与用户关系");
            result += "<br />" + "添加表 " + tableName + " " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Id", "int", "4", "0", "Id");
            result += "<br />" + "为表 " + tableName + " 添加字段 Id int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "UserId", "int", "4", "0", "用户Id");
            result += "<br />" + "为表 " + tableName + " 添加字段 UserId int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "ActionId", "int", "4", "0", "动作Id");
            result += "<br />" + "为表 " + tableName + " 添加字段 ActionId int " + (ret == "" ? "OK." : ret);

            #endregion ===== 系统模块动作与用户关系 (tb_sys_UserActions) =====
*/

/*
            #region ===== 系统模块动作与用户关系 (tb_sys_UserActions) =====

            tableName = "tb_sys_UserActions";
            ret = await this.InternalCheckAddTable(tableName, "系统模块动作与用户关系");
            result.Add("添加表 " + tableName + " " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Id", "int", "4", "0", "Id");
            result.Add("为表 " + tableName + " 添加字段 Id int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "UserId", "int", "4", "0", "用户Id");
            result.Add("为表 " + tableName + " 添加字段 UserId int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "ActionId", "int", "4", "0", "动作Id");
            result.Add("为表 " + tableName + " 添加字段 ActionId int " + (ret == "" ? "OK." : ret));

            #endregion ===== 系统模块动作与用户关系 (tb_sys_UserActions) =====
*/
