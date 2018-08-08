using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ywl.Web.Api.Models
{
    using Ywl.Data.Entity;

    [Description(Title = "用户", Description = "系统登录用户")]
    public class User : Ywl.Data.Entity.Models.BaseUser
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

    }
    public class UserOracleMap : EntityTypeConfiguration<User>
    {
        public UserOracleMap(string schemaName)
        {
            this.ToTable("tb_sys_users".ToUpper(), schemaName);
            this.HasKey(e => e.Id);
            this.Property(e => e.Id).HasColumnName("Id".ToUpper());
            this.Property(e => e.Status).HasColumnName("Status".ToUpper());
            this.Property(e => e.Name).HasColumnName("Name".ToUpper());
            this.Ignore(e => e.Creator);
            this.Ignore(e => e.CreateTime);
            this.Ignore(e => e.Description);
            this.Ignore(e => e.Orderno);
            this.Property(e => e.Account).HasColumnName("Account".ToUpper());
            this.Property(e => e.Password).HasColumnName("Password".ToUpper());
            this.Property(e => e.Sex).HasColumnName("Sex".ToUpper());
            this.Property(e => e.OrganizationId).HasColumnName("OrganizationId".ToUpper());
            this.Property(e => e.OrganizationPath).HasColumnName("OrganizationPath".ToUpper());
            this.Property(e => e.DepId).HasColumnName("DepId".ToUpper());
            this.Property(e => e.DepName).HasColumnName("DepName".ToUpper());
            this.Property(e => e.GroupId).HasColumnName("GroupId".ToUpper());
            this.Property(e => e.GroupName).HasColumnName("GroupName".ToUpper());
            this.Property(e => e.HeadImagePath).HasColumnName("HeadImagePath".ToUpper());
            this.Property(e => e.PhotoPath).HasColumnName("PhotoPath".ToUpper());

        }
    }
    public class UserMSSqlMap : EntityTypeConfiguration<User>
    {
        public UserMSSqlMap(string schemaName)
        {
            this.ToTable("tb_sys_users", schemaName);
            this.HasKey(e => e.Id);
            this.Property(e => e.Id).HasColumnName("Id");
            this.Property(e => e.Status).HasColumnName("Status");
            this.Property(e => e.Name).HasColumnName("Name");
            this.Ignore(e => e.Creator);
            this.Ignore(e => e.CreateTime);
            this.Ignore(e => e.Description);
            this.Ignore(e => e.Orderno);
            this.Property(e => e.Account).HasColumnName("Account");
            this.Property(e => e.Password).HasColumnName("Password");
            this.Property(e => e.Sex).HasColumnName("Sex");
            this.Property(e => e.OrganizationId).HasColumnName("OrgId");
            this.Property(e => e.OrganizationPath).HasColumnName("OrgPath");
            this.Property(e => e.DepId).HasColumnName("DepId");
            this.Property(e => e.DepName).HasColumnName("DepName");
            this.Property(e => e.GroupId).HasColumnName("GroupId");
            this.Property(e => e.GroupName).HasColumnName("GroupName");
            this.Property(e => e.HeadImagePath).HasColumnName("HeadImagePath");
            this.Property(e => e.PhotoPath).HasColumnName("PhotoPath");

        }
    }
}

/*

{
    "isLdEntity":false,
    "nameSpace":"Ywl.Web.Test.Models",
    "entityName":"User",
    "inheritedFrom":"Ywl.Web.Mvc.Models.BaseUser",
    "entityTitle":"用户",
    "entityDesc":"系统登录用户",
    "mappedTable":"tb_sys_users",
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
            "inherited":true,
            "nullable":true
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
            "inherited":true,
            "nullable":true
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
            "nullable":true
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
            "nullable":true
        },
        {
            "id":5,
            "name":"Description",
            "mapped":false,
            "mappedName":"Description",
            "title":"说明",
            "description":"说明",
            "dataType":"string",
            "length":256,
            "scale":0,
            "nullable":true
        },
        {
            "id":6,
            "name":"Orderno",
            "mapped":false,
            "mappedName":"Orderno",
            "title":"排序号",
            "description":"排序号",
            "dataType":"string",
            "length":10,
            "scale":0,
            "inherited":false,
            "nullable":true
        },
        {
            "title":"账户",
            "description":"账户，用户登录用户名",
            "name":"Account",
            "mappedName":"Account",
            "dataType":"string",
            "length":"20",
            "mapped":true,
            "inherited":true,
            "nullable":true
        },
        {
            "name":"Password",
            "mapped":true,
            "mappedName":"Password",
            "dataType":"string",
            "length":"50",
            "description":"密码，用户登录密码",
            "title":"密码",
            "inherited":true,
            "nullable":true
        },
        {
            "title":"性别",
            "name":"Sex",
            "mapped":true,
            "mappedName":"Sex",
            "dataType":"string",
            "length":"4",
            "description":"性别",
            "inherited":true,
            "nullable":true
        },
        {
            "title":"组织机构编号",
            "name":"OrganizationId",
            "mappedName":"OrgId",
            "dataType":"int",
            "description":"组织机构编号",
            "mapped":true,
            "inherited":true,
            "nullable":true
        },
        {
            "title":"组织机构路径",
            "name":"OrganizationPath",
            "mappedName":"OrgPath",
            "dataType":"string",
            "length":"256",
            "description":"组织机构路径",
            "mapped":true,
            "inherited":true,
            "nullable":true
        },
        {
            "title":"部门编号",
            "description":"部门编号",
            "dataType":"int",
            "name":"DepId",
            "mappedName":"DepId",
            "mapped":true,
            "inherited":true,
            "nullable":true
        },
        {
            "title":"部门名称",
            "description":"部门名称",
            "dataType":"string",
            "length":"50",
            "name":"DepName",
            "mappedName":"DepName",
            "mapped":true,
            "inherited":true,
            "nullable":true
        },
        {
            "title":"班组编号",
            "description":"班组编号",
            "dataType":"int",
            "name":"GroupId",
            "mappedName":"GroupId",
            "mapped":true,
            "inherited":true,
            "nullable":true
        },
        {
            "title":"班组名称",
            "description":"班组名称",
            "dataType":"string",
            "length":"50",
            "name":"GroupName",
            "mappedName":"GroupName",
            "mapped":true,
            "inherited":true,
            "nullable":true
        },
        {
            "title":"头像路径",
            "description":"头像路径",
            "dataType":"string",
            "length":"256",
            "name":"HeadImagePath",
            "mappedName":"HeadImagePath",
            "mapped":true,
            "inherited":true,
            "nullable":true
        },
        {
            "title":"照片路径",
            "description":"照片路径",
            "dataType":"string",
            "length":"256",
            "name":"PhotoPath",
            "mappedName":"PhotoPath",
            "mapped":true,
            "inherited":true,
            "nullable":true
        }
    ]
}


*/

/*
            #region ===== 系统登录用户 (tb_sys_users) =====

            tableName = "tb_sys_users";
            ret = await this.InternalCheckAddTable(tableName, "系统登录用户");
            result += "<br />" + "添加表 " + tableName + " " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Id", "int", "4", "0", "Id");
            result += "<br />" + "为表 " + tableName + " 添加字段 Id int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Status", "string", "50", "0", "状态");
            result += "<br />" + "为表 " + tableName + " 添加字段 Status string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Name", "string", "50", "0", "名称");
            result += "<br />" + "为表 " + tableName + " 添加字段 Name string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Account", "string", "20", "undefined", "账户");
            result += "<br />" + "为表 " + tableName + " 添加字段 Account string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Password", "string", "50", "undefined", "密码");
            result += "<br />" + "为表 " + tableName + " 添加字段 Password string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Sex", "string", "4", "undefined", "性别");
            result += "<br />" + "为表 " + tableName + " 添加字段 Sex string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "OrgId", "int", "undefined", "undefined", "组织机构编号");
            result += "<br />" + "为表 " + tableName + " 添加字段 OrgId int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "OrgPath", "string", "256", "undefined", "组织机构路径");
            result += "<br />" + "为表 " + tableName + " 添加字段 OrgPath string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "DepId", "int", "undefined", "undefined", "部门编号");
            result += "<br />" + "为表 " + tableName + " 添加字段 DepId int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "DepName", "string", "50", "undefined", "部门名称");
            result += "<br />" + "为表 " + tableName + " 添加字段 DepName string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "GroupId", "int", "undefined", "undefined", "班组编号");
            result += "<br />" + "为表 " + tableName + " 添加字段 GroupId int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "GroupName", "string", "50", "undefined", "班组名称");
            result += "<br />" + "为表 " + tableName + " 添加字段 GroupName string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "HeadImagePath", "string", "256", "undefined", "头像路径");
            result += "<br />" + "为表 " + tableName + " 添加字段 HeadImagePath string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "PhotoPath", "string", "256", "undefined", "照片路径");
            result += "<br />" + "为表 " + tableName + " 添加字段 PhotoPath string " + (ret == "" ? "OK." : ret);

            #endregion ===== 系统登录用户 (tb_sys_users) =====
*/
