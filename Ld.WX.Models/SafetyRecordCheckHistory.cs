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

    [Description(Title = "安全审核审核历史", Description = "安全审核审核历史")]
    public class SafetyRecordCheckHistory : CreatedEntity
    {

        /// <summary>
        /// 单据Id; 单据Id
        /// </summary>
        [Display(Name = "单据Id", Description = "单据Id")]

        public int? RId { get; set; }

        /// <summary>
        /// 创建者; 创建者
        /// </summary>
        [Display(Name = "创建者", Description = "创建者")]
        [MaxLength(50)]
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建者Ip; 创建者Ip
        /// </summary>
        [Display(Name = "创建者Ip", Description = "创建者Ip")]
        [MaxLength(50)]
        public string CreatorIp { get; set; }

        /// <summary>
        /// 审核意见; 说明
        /// </summary>
        [Display(Name = "审核意见", Description = "说明")]
        [MaxLength(500)]
        public string Opinion { get; set; }

        /// <summary>
        /// 审核结果; 排序号
        /// </summary>
        [Display(Name = "审核结果", Description = "排序号")]
        [MaxLength(10)]
        public string Result { get; set; }

        /// <summary>
        /// 下一步人员; 上传到服务器后的地址
        /// </summary>
        [Display(Name = "下一步人员", Description = "上传到服务器后的地址")]

        public int? NextStepManId { get; set; }

        /// <summary>
        /// 下一步人员; 附件分类
        /// </summary>
        [Display(Name = "下一步人员", Description = "附件分类")]
        [MaxLength(20)]
        public string NextStepManName { get; set; }

        public SafetyRecordCheckHistory()
        {
            this.CreateTime = DateTime.Now;
        }

        public SafetyRecordCheckHistory(SafetyRecordCheckHistory source) { this.CopyFrom(source); }

        public void CopyFrom(SafetyRecordCheckHistory source)
        {
            base.CopyFrom(source);

            //this.Id = source.Id;

            this.RId = source.RId;
            //this.Creator = source.Creator;
            this.CreatorName = source.CreatorName;
            //this.CreateTime = source.CreateTime;
            this.CreatorIp = source.CreatorIp;
            this.Opinion = source.Opinion;
            this.Result = source.Result;
            this.NextStepManId = source.NextStepManId;
            this.NextStepManName = source.NextStepManName;
        }

    }

    public class SafetyRecordCheckHistoryOracleMap : EntityTypeConfiguration<SafetyRecordCheckHistory>
    {
        public SafetyRecordCheckHistoryOracleMap(string schemaName)
        {
            this.ToTable("tb_sc_checkhistories".ToUpper(), schemaName);
            this.HasKey(e => e.Id);
            this.Property(e => e.Id).HasColumnName("Id".ToUpper());
            this.Property(e => e.RId).HasColumnName("RId".ToUpper());
            this.Property(e => e.Creator).HasColumnName("Creator".ToUpper());
            this.Property(e => e.CreatorName).HasColumnName("CreatorName".ToUpper());
            this.Property(e => e.CreateTime).HasColumnName("CreateTime".ToUpper());
            this.Property(e => e.CreatorIp).HasColumnName("CreatorIp".ToUpper());
            this.Property(e => e.Opinion).HasColumnName("Opinion".ToUpper());
            this.Property(e => e.Result).HasColumnName("Result".ToUpper());
            this.Property(e => e.NextStepManId).HasColumnName("NextStepManId".ToUpper());
            this.Property(e => e.NextStepManName).HasColumnName("NextStepManName".ToUpper());

            this.Property(e => e.Name).HasColumnName("NodeName".ToUpper());
            this.Ignore(e => e.Status);
        }
    }
    public class SafetyRecordCheckHistoryMSSqlMap : EntityTypeConfiguration<SafetyRecordCheckHistory>
    {
        public SafetyRecordCheckHistoryMSSqlMap(string schemaName)
        {
            this.ToTable("tb_sc_checkhistories", schemaName);
            this.HasKey(e => e.Id);
            this.Property(e => e.Id).HasColumnName("Id");
            this.Property(e => e.RId).HasColumnName("RId");
            this.Property(e => e.Creator).HasColumnName("Creator");
            this.Property(e => e.CreatorName).HasColumnName("CreatorName");
            this.Property(e => e.CreateTime).HasColumnName("CreateTime");
            this.Property(e => e.CreatorIp).HasColumnName("CreatorIp");
            this.Property(e => e.Opinion).HasColumnName("Opinion");
            this.Property(e => e.Result).HasColumnName("Result");
            this.Property(e => e.NextStepManId).HasColumnName("NextStepManId");
            this.Property(e => e.NextStepManName).HasColumnName("NextStepManName");
            this.Property(e => e.Name).HasColumnName("NodeName");
            this.Ignore(e => e.Status);
        }
    }
}

/*

{
    "isLdEntity":true,
    "nameSpace":"Ld.WX.Models",
    "entityName":"SafetyRecordCheckHistory",
    "inheritedFrom":"CreatedEntity",
    "entityTitle":"安全审核审核历史",
    "entityDesc":"安全审核审核历史",
    "mappedTable":"tb_sc_checkhistories",
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
            "dataType":"int",
            "length":4,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"RId",
            "title":"单据Id",
            "mappedName":"RId",
            "description":"单据Id",
            "colIndex":"02"
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
            "nullable":true,
            "colIndex":"05",
            "inherited":true
        },
        {
            "dataType":"string",
            "length":50,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"CreatorName",
            "title":"创建者",
            "mappedName":"CreatorName",
            "description":"创建者",
            "colIndex":"06"
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
            "nullable":true,
            "colIndex":"07",
            "inherited":true
        },
        {
            "dataType":"string",
            "length":50,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"CreatorIp",
            "title":"创建者Ip",
            "mappedName":"CreatorIp",
            "description":"创建者Ip",
            "colIndex":"08"
        },
        {
            "id":5,
            "name":"Opinion",
            "mapped":true,
            "mappedName":"Opinion",
            "title":"审核意见",
            "description":"说明",
            "dataType":"string",
            "nullable":true,
            "length":"500",
            "scale":0,
            "colIndex":"10"
        },
        {
            "id":6,
            "name":"Result",
            "mapped":true,
            "mappedName":"Result",
            "title":"审核结果",
            "description":"排序号",
            "dataType":"string",
            "length":10,
            "scale":0,
            "colIndex":"11"
        },
        {
            "dataType":"int",
            "length":4,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"NextStepManId",
            "title":"下一步人员",
            "mappedName":"NextStepManId",
            "description":"上传到服务器后的地址",
            "colIndex":"98"
        },
        {
            "dataType":"string",
            "length":"20",
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"NextStepManName",
            "title":"下一步人员",
            "mappedName":"NextStepManName",
            "description":"附件分类",
            "colIndex":"99"
        }
    ]
}


*/

/*
            #region ===== 安全审核审核历史 (tb_sc_checkhistories) =====

            tableName = "tb_sc_checkhistories";
            ret = await this.InternalCheckAddTable(tableName, "安全审核审核历史");
            result += "<br />" + "添加表 " + tableName + " " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Id", "int", "4", "0", "Id");
            result += "<br />" + "为表 " + tableName + " 添加字段 Id int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "RId", "int", "4", "0", "单据Id");
            result += "<br />" + "为表 " + tableName + " 添加字段 RId int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Creator", "int", "4", "0", "创建者");
            result += "<br />" + "为表 " + tableName + " 添加字段 Creator int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "CreatorName", "string", "50", "0", "创建者");
            result += "<br />" + "为表 " + tableName + " 添加字段 CreatorName string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "CreateTime", "datetime", "8", "4", "创建时间");
            result += "<br />" + "为表 " + tableName + " 添加字段 CreateTime datetime " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "CreatorIp", "string", "50", "0", "创建者Ip");
            result += "<br />" + "为表 " + tableName + " 添加字段 CreatorIp string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Opinion", "string", "500", "0", "审核意见");
            result += "<br />" + "为表 " + tableName + " 添加字段 Opinion string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Result", "string", "10", "0", "审核结果");
            result += "<br />" + "为表 " + tableName + " 添加字段 Result string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "NextStepManId", "int", "4", "0", "下一步人员");
            result += "<br />" + "为表 " + tableName + " 添加字段 NextStepManId int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "NextStepManName", "string", "20", "0", "下一步人员");
            result += "<br />" + "为表 " + tableName + " 添加字段 NextStepManName string " + (ret == "" ? "OK." : ret);

            #endregion ===== 安全审核审核历史 (tb_sc_checkhistories) =====
*/

/*
            #region ===== 安全审核审核历史 (tb_sc_checkhistories) =====

            tableName = "tb_sc_checkhistories";
            ret = await this.InternalCheckAddTable(tableName, "安全审核审核历史");
            result.Add("添加表 " + tableName + " " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Id", "int", "4", "0", "Id");
            result.Add("为表 " + tableName + " 添加字段 Id int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "RId", "int", "4", "0", "单据Id");
            result.Add("为表 " + tableName + " 添加字段 RId int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Creator", "int", "4", "0", "创建者");
            result.Add("为表 " + tableName + " 添加字段 Creator int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "CreatorName", "string", "50", "0", "创建者");
            result.Add("为表 " + tableName + " 添加字段 CreatorName string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "CreateTime", "datetime", "8", "4", "创建时间");
            result.Add("为表 " + tableName + " 添加字段 CreateTime datetime " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "CreatorIp", "string", "50", "0", "创建者Ip");
            result.Add("为表 " + tableName + " 添加字段 CreatorIp string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Opinion", "string", "500", "0", "审核意见");
            result.Add("为表 " + tableName + " 添加字段 Opinion string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Result", "string", "10", "0", "审核结果");
            result.Add("为表 " + tableName + " 添加字段 Result string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "NextStepManId", "int", "4", "0", "下一步人员");
            result.Add("为表 " + tableName + " 添加字段 NextStepManId int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "NextStepManName", "string", "20", "0", "下一步人员");
            result.Add("为表 " + tableName + " 添加字段 NextStepManName string " + (ret == "" ? "OK." : ret));

            #endregion ===== 安全审核审核历史 (tb_sc_checkhistories) =====
*/
