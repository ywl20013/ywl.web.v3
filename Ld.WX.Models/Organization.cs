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

    [Description(Title = "组织机构", Description = "组织机构")]
    public class Organization : BaseOrganization
    {

        /// <summary>
        /// 路径; 路径
        /// </summary>
        [Display(Name = "路径", Description = "路径")]
        [MaxLength(500)]
        public string Path { get; set; }

        /// <summary>
        /// 说明; 说明
        /// </summary>
        [Display(Name = "说明", Description = "说明")]
        [MaxLength(256)]
        public string Description { get; set; }

        public Organization()
        {

        }

        public Organization(Organization source) { this.CopyFrom(source); }

        public void CopyFrom(Organization source)
        {
            base.CopyFrom(source);

            //this.Id = source.Id;
            //this.PId = source.PId;
            //this.Status = source.Status;
            //this.Name = source.Name;
            //this.IsDepartment = source.IsDepartment;
            //this.IsGroup = source.IsGroup;
            //this.OrderNumber = source.OrderNumber;
            this.Path = source.Path;
            this.Description = source.Description;
        }

    }

    public class OrganizationOracleMap : EntityTypeConfiguration<Organization>
    {
        public OrganizationOracleMap(string schemaName)
        {
            this.ToTable("tb_sys_organizations".ToUpper(), schemaName);
            this.HasKey(e => e.Id);
            this.Property(e => e.Id).HasColumnName("Id".ToUpper());
            this.Property(e => e.PId).HasColumnName("PId".ToUpper());
            this.Property(e => e.Status).HasColumnName("Status".ToUpper());
            this.Property(e => e.Name).HasColumnName("Name".ToUpper());
            this.Property(e => e.IsDepartment).HasColumnName("IsDep".ToUpper());
            this.Property(e => e.IsGroup).HasColumnName("IsClass".ToUpper());
            this.Property(e => e.OrderNumber).HasColumnName("Orderno".ToUpper());
            this.Property(e => e.Path).HasColumnName("Path".ToUpper());
            this.Property(e => e.Description).HasColumnName("Description".ToUpper());
        }
    }
    public class OrganizationMSSqlMap : EntityTypeConfiguration<Organization>
    {
        public OrganizationMSSqlMap(string schemaName)
        {
            this.ToTable("tb_sys_organizations", schemaName);
            this.HasKey(e => e.Id);
            this.Property(e => e.Id).HasColumnName("Id");
            this.Property(e => e.PId).HasColumnName("PId");
            this.Property(e => e.Status).HasColumnName("Status");
            this.Property(e => e.Name).HasColumnName("Name");
            this.Property(e => e.IsDepartment).HasColumnName("IsDep");
            this.Property(e => e.IsGroup).HasColumnName("IsClass");
            this.Property(e => e.OrderNumber).HasColumnName("Orderno");
            this.Property(e => e.Path).HasColumnName("Path");
            this.Property(e => e.Description).HasColumnName("Description");
        }
    }
}

/*

{
    "isLdEntity":false,
    "nameSpace":"Ld.WX.Api.Handler.Models",
    "entityName":"Organization",
    "inheritedFrom":"BaseOrganization",
    "entityTitle":"组织机构",
    "entityDesc":"组织机构",
    "mappedTable":"tb_sys_organizations",
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
            "scale":0,
            "colIndex":"01"
        },
        {
            "id":3,
            "name":"PId",
            "mapped":true,
            "mappedName":"PId",
            "title":"上一级",
            "description":"创建者，创建者的Id",
            "dataType":"int",
            "length":4,
            "scale":0,
            "colIndex":"02",
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
            "colIndex":"03",
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
            "colIndex":"04",
            "inherited":true
        },
        {
            "id":4,
            "name":"IsDepartment",
            "mapped":true,
            "mappedName":"IsDep",
            "title":"是否部门",
            "description":"创建时间",
            "dataType":"bool",
            "length":1,
            "scale":0,
            "colIndex":"05",
            "inherited":true
        },
        {
            "dataType":"bool",
            "length":1,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"IsGroup",
            "title":"是否班组",
            "mappedName":"IsClass",
            "description":"是否班组",
            "colIndex":"06",
            "inherited":true
        },
        {
            "id":6,
            "name":"OrderNumber",
            "mapped":true,
            "mappedName":"Orderno",
            "title":"排序号",
            "description":"排序号",
            "dataType":"string",
            "length":10,
            "scale":0,
            "colIndex":"07",
            "inherited":true
        },
        {
            "dataType":"string",
            "length":"500",
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"Path",
            "title":"路径",
            "colIndex":"08",
            "mappedName":"Path",
            "description":"路径"
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
            "scale":0,
            "colIndex":"09"
        }
    ]
}


*/

/*
            #region ===== 组织机构 (tb_sys_organizations) =====

            tableName = "tb_sys_organizations";
            ret = await this.InternalCheckAddTable(tableName, "组织机构");
            result += "<br />" + "添加表 " + tableName + " " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Id", "int", "4", "0", "Id");
            result += "<br />" + "为表 " + tableName + " 添加字段 Id int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "PId", "int", "4", "0", "上一级");
            result += "<br />" + "为表 " + tableName + " 添加字段 PId int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Status", "string", "50", "0", "状态");
            result += "<br />" + "为表 " + tableName + " 添加字段 Status string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Name", "string", "50", "0", "名称");
            result += "<br />" + "为表 " + tableName + " 添加字段 Name string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "IsDep", "bool", "1", "0", "是否部门");
            result += "<br />" + "为表 " + tableName + " 添加字段 IsDep bool " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "IsClass", "bool", "1", "0", "是否班组");
            result += "<br />" + "为表 " + tableName + " 添加字段 IsClass bool " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Orderno", "string", "10", "0", "排序号");
            result += "<br />" + "为表 " + tableName + " 添加字段 Orderno string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Path", "string", "500", "0", "路径");
            result += "<br />" + "为表 " + tableName + " 添加字段 Path string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Description", "string", "256", "0", "说明");
            result += "<br />" + "为表 " + tableName + " 添加字段 Description string " + (ret == "" ? "OK." : ret);

            #endregion ===== 组织机构 (tb_sys_organizations) =====
*/

/*
            #region ===== 组织机构 (tb_sys_organizations) =====

            tableName = "tb_sys_organizations";
            ret = await this.InternalCheckAddTable(tableName, "组织机构");
            result.Add("添加表 " + tableName + " " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Id", "int", "4", "0", "Id");
            result.Add("为表 " + tableName + " 添加字段 Id int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "PId", "int", "4", "0", "上一级");
            result.Add("为表 " + tableName + " 添加字段 PId int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Status", "string", "50", "0", "状态");
            result.Add("为表 " + tableName + " 添加字段 Status string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Name", "string", "50", "0", "名称");
            result.Add("为表 " + tableName + " 添加字段 Name string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "IsDep", "bool", "1", "0", "是否部门");
            result.Add("为表 " + tableName + " 添加字段 IsDep bool " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "IsClass", "bool", "1", "0", "是否班组");
            result.Add("为表 " + tableName + " 添加字段 IsClass bool " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Orderno", "string", "10", "0", "排序号");
            result.Add("为表 " + tableName + " 添加字段 Orderno string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Path", "string", "500", "0", "路径");
            result.Add("为表 " + tableName + " 添加字段 Path string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Description", "string", "256", "0", "说明");
            result.Add("为表 " + tableName + " 添加字段 Description string " + (ret == "" ? "OK." : ret));

            #endregion ===== 组织机构 (tb_sys_organizations) =====
*/
