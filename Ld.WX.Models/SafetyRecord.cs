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

    [Description(Title = "安全审核记录", Description = "安全审核记录")]
    public class SafetyRecord : NamedEntity
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
        /// 创建者姓名; 创建者姓名
        /// </summary>
        [Display(Name = "创建者姓名", Description = "创建者姓名")]
        [MaxLength(50)]
        public string CreatorName { get; set; }

        /// <summary>
        /// 编号; 编号
        /// </summary>
        [Display(Name = "编号", Description = "编号")]
        [MaxLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// 发现人; 发现人
        /// </summary>
        [Display(Name = "发现人", Description = "发现人")]

        public int? Finder { get; set; }

        [NotMapped]
        public string FinderName { get; set; }

        /// <summary>
        /// 发现时间; 发现时间
        /// </summary>
        [Display(Name = "发现时间", Description = "发现时间")]

        public DateTime? FindTime { get; set; }

        /// <summary>
        /// 预计完成时间; 预计完成时间
        /// </summary>
        [Display(Name = "预计完成时间", Description = "预计完成时间")]

        public DateTime? PlanFinishTime { get; set; }

        /// <summary>
        /// 发现部门; 发现部门
        /// </summary>
        [Display(Name = "发现部门", Description = "发现部门")]

        public int? FindDepartment { get; set; }

        [NotMapped]
        public string FindDepartmentName { get; set; }

        /// <summary>
        /// 问题来源; 问题来源
        /// </summary>
        [Display(Name = "问题来源", Description = "问题来源")]

        public int? SourceType { get; set; }

        /// <summary>
        /// 问题来源2; 问题来源2
        /// </summary>
        [Display(Name = "问题来源2", Description = "问题来源2")]

        public int? SouceSubType { get; set; }

        /// <summary>
        /// 责任部门; 责任部门
        /// </summary>
        [Display(Name = "责任部门", Description = "责任部门")]

        public int? ResponsibleDepartment { get; set; }

        [NotMapped]
        public string ResponsibleDepartmentName { get; set; }

        /// <summary>
        /// 责任班组; 责任班组
        /// </summary>
        [Display(Name = "责任班组", Description = "责任班组")]

        public int? ResponsibleClass { get; set; }

        [NotMapped]
        public string ResponsibleClassName { get; set; }

        /// <summary>
        /// 责任人; 责任人
        /// </summary>
        [Display(Name = "责任人", Description = "责任人")]

        public int? ResponsiblePerson { get; set; }

        [NotMapped]
        public string ResponsiblePersonName { get; set; }

        /// <summary>
        /// 提交人; 提交人
        /// </summary>
        [Display(Name = "提交人", Description = "提交人")]

        public int? Submitter { get; set; }

        /// <summary>
        /// 提交时间; 提交时间
        /// </summary>
        [Display(Name = "提交时间", Description = "提交时间")]

        public DateTime? SubmitTime { get; set; }

        /// <summary>
        /// 接收人; 接收人
        /// </summary>
        [Display(Name = "接收人", Description = "接收人")]

        public int? Receiver { get; set; }

        /// <summary>
        /// 接收时间; 接收时间
        /// </summary>
        [Display(Name = "接收时间", Description = "接收时间")]

        public DateTime? ReceiveTime { get; set; }

        /// <summary>
        /// 验收人; 验收人
        /// </summary>
        [Display(Name = "验收人", Description = "验收人")]

        public int? Acceptor { get; set; }

        [NotMapped]
        public string AcceptorName { get; set; }

        /// <summary>
        /// 验收时间; 验收时间
        /// </summary>
        [Display(Name = "验收时间", Description = "验收时间")]

        public DateTime? AcceptTime { get; set; }

        /// <summary>
        /// 问题描述; 问题描述
        /// </summary>
        [Display(Name = "问题描述", Description = "问题描述")]
        [MaxLength(2000)]
        public string Content { get; set; }

        /// <summary>
        /// 验收结果; 验收结果
        /// </summary>
        [Display(Name = "验收结果", Description = "验收结果")]
        [MaxLength(2000)]
        public string Result { get; set; }

        /// <summary>
        /// 防范和应急措施; 防范和应急措施
        /// </summary>
        [Display(Name = "防范和应急措施", Description = "防范和应急措施")]
        [MaxLength(2000)]
        public string Measures { get; set; }

        /// <summary>
        /// 整改进度; 整改进度
        /// </summary>
        [Display(Name = "整改进度", Description = "整改进度")]
        [MaxLength(2000)]
        public string RectifiedSchedule { get; set; }

        /// <summary>
        /// 流程Id; 流程Id
        /// </summary>
        [Display(Name = "流程Id", Description = "流程Id")]

        public int? FlowId { get; set; }

        /// <summary>
        /// 流程状态; 流程状态
        /// </summary>
        [Display(Name = "流程状态", Description = "流程状态")]
        [MaxLength(50)]
        public string FlowStatus { get; set; }

        /// <summary>
        /// 是否隐患; 是否隐患
        /// </summary>
        [Display(Name = "是否隐患", Description = "是否隐患")]
        [MaxLength(50)]
        public string DangerType { get; set; }

        /// <summary>
        /// 隐患等级; 隐患等级
        /// </summary>
        [Display(Name = "隐患等级", Description = "隐患等级")]
        [MaxLength(50)]
        public string DangerLevel { get; set; }

        /// <summary>
        /// 安全风险属性; 安全风险属性
        /// </summary>
        [Display(Name = "安全风险属性", Description = "安全风险属性")]
        [MaxLength(50)]
        public string BreakRulesType { get; set; }

        /// <summary>
        /// 整改要求; 整改要求
        /// </summary>
        [Display(Name = "整改要求", Description = "整改要求")]
        [MaxLength(2000)]
        public string RectificationRequirements { get; set; }

        /// <summary>
        /// 专业验收时间; 专业验收时间
        /// </summary>
        [Display(Name = "专业验收时间", Description = "专业验收时间")]

        public DateTime? ProfessionalAcceptTime { get; set; }

        /// <summary>
        /// 发现人电话; 发现人电话
        /// </summary>
        [Display(Name = "发现人电话", Description = "发现人电话")]
        [MaxLength(50)]
        public string FinderTelphone { get; set; }

        /// <summary>
        /// 同步Id; 同步Id
        /// </summary>
        [Display(Name = "同步Id", Description = "同步Id")]

        public int? SyncId { get; set; }

        /// <summary>
        /// 已同步; 已同步
        /// </summary>
        [Display(Name = "已同步", Description = "已同步")]

        public bool? Synced { get; set; }

        [NotMapped]
        public List<SafetyRecordAttachment> Attachments { get; set; }

        [NotMapped]
        public List<SafetyRecordCheckHistory> CheckHistories { get; set; }

        public SafetyRecord()
        {
            this.CreateTime = DateTime.Now;
        }

        public SafetyRecord(SafetyRecord source) { this.CopyFrom(source); }

        public void CopyFrom(SafetyRecord source)
        {
            base.CopyFrom(source);

            //this.Id = source.Id;
            //this.Status = source.Status;
            //this.Name = source.Name;
            this.Creator = source.Creator;
            this.CreateTime = source.CreateTime;
            this.Description = source.Description;
            this.Orderno = source.Orderno;
            this.CreatorName = source.CreatorName;
            this.Code = source.Code;
            this.Finder = source.Finder;
            this.FindTime = source.FindTime;
            this.PlanFinishTime = source.PlanFinishTime;
            this.FindDepartment = source.FindDepartment;
            this.SourceType = source.SourceType;
            this.SouceSubType = source.SouceSubType;
            this.ResponsibleDepartment = source.ResponsibleDepartment;
            this.ResponsibleClass = source.ResponsibleClass;
            this.ResponsiblePerson = source.ResponsiblePerson;
            this.Submitter = source.Submitter;
            this.SubmitTime = source.SubmitTime;
            this.Receiver = source.Receiver;
            this.ReceiveTime = source.ReceiveTime;
            this.Acceptor = source.Acceptor;
            this.AcceptTime = source.AcceptTime;
            this.Content = source.Content;
            this.Result = source.Result;
            this.Measures = source.Measures;
            this.RectifiedSchedule = source.RectifiedSchedule;
            this.FlowId = source.FlowId;
            this.FlowStatus = source.FlowStatus;
            this.DangerType = source.DangerType;
            this.DangerLevel = source.DangerLevel;
            this.BreakRulesType = source.BreakRulesType;
            this.RectificationRequirements = source.RectificationRequirements;
            this.ProfessionalAcceptTime = source.ProfessionalAcceptTime;
            this.FinderTelphone = source.FinderTelphone;
            this.SyncId = source.SyncId;
            this.Synced = source.Synced;
        }

    }

    public class SafetyRecordOracleMap : EntityTypeConfiguration<SafetyRecord>
    {
        public SafetyRecordOracleMap(string schemaName)
        {
            this.ToTable("tb_sc_record".ToUpper(), schemaName);
            this.HasKey(e => e.Id);
            this.Property(e => e.Id).HasColumnName("Id".ToUpper());
            this.Property(e => e.Status).HasColumnName("Status".ToUpper());
            this.Property(e => e.Name).HasColumnName("Name".ToUpper());
            this.Property(e => e.Creator).HasColumnName("Creator".ToUpper());
            this.Property(e => e.CreateTime).HasColumnName("CreateTime".ToUpper());
            this.Property(e => e.Description).HasColumnName("Description".ToUpper());
            this.Property(e => e.Orderno).HasColumnName("Orderno".ToUpper());
            this.Property(e => e.CreatorName).HasColumnName("CreatorName".ToUpper());
            this.Property(e => e.Code).HasColumnName("Code".ToUpper());
            this.Property(e => e.Finder).HasColumnName("Finder".ToUpper());
            this.Property(e => e.FindTime).HasColumnName("FindTime".ToUpper());
            this.Property(e => e.PlanFinishTime).HasColumnName("PlanFinishTime".ToUpper());
            this.Property(e => e.FindDepartment).HasColumnName("FindDepartment".ToUpper());
            this.Property(e => e.SourceType).HasColumnName("SourceType".ToUpper());
            this.Property(e => e.SouceSubType).HasColumnName("SouceSubType".ToUpper());
            this.Property(e => e.ResponsibleDepartment).HasColumnName("ResponsibleDepartment".ToUpper());
            this.Property(e => e.ResponsibleClass).HasColumnName("ResponsibleClass".ToUpper());
            this.Property(e => e.ResponsiblePerson).HasColumnName("ResponsiblePerson".ToUpper());
            this.Property(e => e.Submitter).HasColumnName("Submitter".ToUpper());
            this.Property(e => e.SubmitTime).HasColumnName("SubmitTime".ToUpper());
            this.Property(e => e.Receiver).HasColumnName("Receiver".ToUpper());
            this.Property(e => e.ReceiveTime).HasColumnName("ReceiveTime".ToUpper());
            this.Property(e => e.Acceptor).HasColumnName("Acceptor".ToUpper());
            this.Property(e => e.AcceptTime).HasColumnName("AcceptTime".ToUpper());
            this.Property(e => e.Content).HasColumnName("Content".ToUpper());
            this.Property(e => e.Result).HasColumnName("Result".ToUpper());
            this.Property(e => e.Measures).HasColumnName("Measures".ToUpper());
            this.Property(e => e.RectifiedSchedule).HasColumnName("RectifiedSchedule".ToUpper());
            this.Property(e => e.FlowId).HasColumnName("FlowId".ToUpper());
            this.Property(e => e.FlowStatus).HasColumnName("FlowStatus".ToUpper());
            this.Property(e => e.DangerType).HasColumnName("DangerType".ToUpper());
            this.Property(e => e.DangerLevel).HasColumnName("DangerLevel".ToUpper());
            this.Property(e => e.BreakRulesType).HasColumnName("BreakRulesType".ToUpper());
            this.Property(e => e.RectificationRequirements).HasColumnName("RectificationRequirements".ToUpper());
            this.Property(e => e.ProfessionalAcceptTime).HasColumnName("ProfessionalAcceptTime".ToUpper());
            this.Property(e => e.FinderTelphone).HasColumnName("FinderTelphone".ToUpper());
            this.Property(e => e.SyncId).HasColumnName("SyncId".ToUpper());
            this.Property(e => e.Synced).HasColumnName("Synced".ToUpper());
        }
    }
    public class SafetyRecordMSSqlMap : EntityTypeConfiguration<SafetyRecord>
    {
        public SafetyRecordMSSqlMap(string schemaName)
        {
            this.ToTable("tb_sc_record", schemaName);
            this.HasKey(e => e.Id);
            this.Property(e => e.Id).HasColumnName("Id");
            this.Property(e => e.Status).HasColumnName("Status");
            this.Property(e => e.Name).HasColumnName("Name");
            this.Property(e => e.Creator).HasColumnName("Creator");
            this.Property(e => e.CreateTime).HasColumnName("CreateTime");
            this.Property(e => e.Description).HasColumnName("Description");
            this.Property(e => e.Orderno).HasColumnName("Orderno");
            this.Property(e => e.CreatorName).HasColumnName("CreatorName");
            this.Property(e => e.Code).HasColumnName("Code");
            this.Property(e => e.Finder).HasColumnName("Finder");
            this.Property(e => e.FindTime).HasColumnName("FindTime");
            this.Property(e => e.PlanFinishTime).HasColumnName("PlanFinishTime");
            this.Property(e => e.FindDepartment).HasColumnName("FindDepartment");
            this.Property(e => e.SourceType).HasColumnName("SourceType");
            this.Property(e => e.SouceSubType).HasColumnName("SouceSubType");
            this.Property(e => e.ResponsibleDepartment).HasColumnName("ResponsibleDepartment");
            this.Property(e => e.ResponsibleClass).HasColumnName("ResponsibleClass");
            this.Property(e => e.ResponsiblePerson).HasColumnName("ResponsiblePerson");
            this.Property(e => e.Submitter).HasColumnName("Submitter");
            this.Property(e => e.SubmitTime).HasColumnName("SubmitTime");
            this.Property(e => e.Receiver).HasColumnName("Receiver");
            this.Property(e => e.ReceiveTime).HasColumnName("ReceiveTime");
            this.Property(e => e.Acceptor).HasColumnName("Acceptor");
            this.Property(e => e.AcceptTime).HasColumnName("AcceptTime");
            this.Property(e => e.Content).HasColumnName("Content");
            this.Property(e => e.Result).HasColumnName("Result");
            this.Property(e => e.Measures).HasColumnName("Measures");
            this.Property(e => e.RectifiedSchedule).HasColumnName("RectifiedSchedule");
            this.Property(e => e.FlowId).HasColumnName("FlowId");
            this.Property(e => e.FlowStatus).HasColumnName("FlowStatus");
            this.Property(e => e.DangerType).HasColumnName("DangerType");
            this.Property(e => e.DangerLevel).HasColumnName("DangerLevel");
            this.Property(e => e.BreakRulesType).HasColumnName("BreakRulesType");
            this.Property(e => e.RectificationRequirements).HasColumnName("RectificationRequirements");
            this.Property(e => e.ProfessionalAcceptTime).HasColumnName("ProfessionalAcceptTime");
            this.Property(e => e.FinderTelphone).HasColumnName("FinderTelphone");
            this.Property(e => e.SyncId).HasColumnName("SyncId");
            this.Property(e => e.Synced).HasColumnName("Synced");
        }
    }
}

/*

{
    "isLdEntity":true,
    "nameSpace":"Ld.WX.Models",
    "entityName":"SafetyRecord",
    "inheritedFrom":"NamedEntity",
    "entityTitle":"安全审核记录",
    "entityDesc":"安全审核记录",
    "mappedTable":"tb_sc_record",
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
            "nullable":false,
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
            "nullable":true,
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
            "scale":0,
            "nullable":true,
            "inherited":false
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
            "scale":0,
            "nullable":true
        },
        {
            "dataType":"string",
            "length":50,
            "scale":0,
            "name":"CreatorName",
            "mapped":true,
            "title":"创建者姓名",
            "mappedName":"CreatorName",
            "description":"创建者姓名",
            "nullable":true
        },
        {
            "dataType":"string",
            "length":50,
            "scale":0,
            "name":"Code",
            "mapped":true,
            "mappedName":"Code",
            "title":"编号",
            "description":"编号",
            "nullable":true
        },
        {
            "dataType":"int",
            "length":4,
            "scale":0,
            "name":"Finder",
            "mapped":true,
            "mappedName":"Finder",
            "title":"发现人",
            "description":"发现人",
            "nullable":true
        },
        {
            "dataType":"datetime",
            "length":8,
            "scale":4,
            "name":"FindTime",
            "mapped":true,
            "mappedName":"FindTime",
            "title":"发现时间",
            "description":"发现时间",
            "nullable":true
        },
        {
            "dataType":"datetime",
            "length":8,
            "scale":4,
            "name":"PlanFinishTime",
            "mapped":true,
            "mappedName":"PlanFinishTime",
            "title":"预计完成时间",
            "description":"预计完成时间",
            "nullable":true
        },
        {
            "dataType":"int",
            "length":50,
            "scale":0,
            "mapped":true,
            "name":"FindDepartment",
            "title":"发现部门",
            "mappedName":"FindDepartment",
            "description":"发现部门",
            "nullable":true
        },
        {
            "dataType":"int",
            "length":4,
            "scale":0,
            "mapped":true,
            "name":"SourceType",
            "title":"问题来源",
            "mappedName":"SourceType",
            "description":"问题来源",
            "nullable":true
        },
        {
            "dataType":"int",
            "length":4,
            "scale":0,
            "mapped":true,
            "name":"SouceSubType",
            "title":"问题来源2",
            "mappedName":"SouceSubType",
            "description":"问题来源2",
            "nullable":true
        },
        {
            "dataType":"int",
            "length":4,
            "scale":0,
            "mapped":true,
            "name":"ResponsibleDepartment",
            "title":"责任部门",
            "mappedName":"ResponsibleDepartment",
            "description":"责任部门",
            "nullable":true
        },
        {
            "dataType":"int",
            "length":4,
            "scale":0,
            "mapped":true,
            "name":"ResponsibleClass",
            "title":"责任班组",
            "mappedName":"ResponsibleClass",
            "description":"责任班组",
            "nullable":true
        },
        {
            "dataType":"int",
            "length":4,
            "scale":0,
            "mapped":true,
            "name":"ResponsiblePerson",
            "title":"责任人",
            "mappedName":"ResponsiblePerson",
            "description":"责任人",
            "nullable":true
        },
        {
            "dataType":"int",
            "length":4,
            "scale":0,
            "mapped":true,
            "name":"Submitter",
            "title":"提交人",
            "mappedName":"Submitter",
            "description":"提交人",
            "nullable":true
        },
        {
            "dataType":"datetime",
            "length":8,
            "scale":4,
            "mapped":true,
            "name":"SubmitTime",
            "title":"提交时间",
            "mappedName":"SubmitTime",
            "description":"提交时间",
            "nullable":true
        },
        {
            "dataType":"int",
            "length":4,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"Receiver",
            "title":"接收人",
            "mappedName":"Receiver",
            "description":"接收人"
        },
        {
            "dataType":"datetime",
            "length":8,
            "scale":4,
            "mapped":true,
            "nullable":true,
            "name":"ReceiveTime",
            "title":"接收时间",
            "mappedName":"ReceiveTime",
            "description":"接收时间"
        },
        {
            "dataType":"int",
            "length":4,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"Acceptor",
            "title":"验收人",
            "mappedName":"Acceptor",
            "description":"验收人"
        },
        {
            "dataType":"datetime",
            "length":8,
            "scale":4,
            "mapped":true,
            "nullable":true,
            "name":"AcceptTime",
            "title":"验收时间",
            "mappedName":"AcceptTime",
            "description":"验收时间"
        },
        {
            "dataType":"string",
            "length":"2000",
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"Content",
            "title":"问题描述",
            "mappedName":"Content",
            "description":"问题描述"
        },
        {
            "dataType":"string",
            "length":"2000",
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"Result",
            "title":"验收结果",
            "mappedName":"Result",
            "description":"验收结果"
        },
        {
            "dataType":"string",
            "length":"2000",
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"Measures",
            "mappedName":"Measures",
            "title":"防范和应急措施",
            "description":"防范和应急措施"
        },
        {
            "dataType":"string",
            "length":"2000",
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"RectifiedSchedule",
            "title":"整改进度",
            "mappedName":"RectifiedSchedule",
            "description":"整改进度"
        },
        {
            "dataType":"int",
            "length":4,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"FlowId",
            "title":"流程Id",
            "mappedName":"FlowId",
            "description":"流程Id"
        },
        {
            "dataType":"string",
            "length":50,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"FlowStatus",
            "title":"流程状态",
            "mappedName":"FlowStatus",
            "description":"流程状态"
        },
        {
            "dataType":"string",
            "length":50,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"DangerType",
            "title":"是否隐患",
            "mappedName":"DangerType",
            "description":"是否隐患"
        },
        {
            "dataType":"string",
            "length":50,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"DangerLevel",
            "title":"隐患等级",
            "mappedName":"DangerLevel",
            "description":"隐患等级"
        },
        {
            "dataType":"string",
            "length":50,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "title":"安全风险属性",
            "name":"BreakRulesType",
            "mappedName":"BreakRulesType",
            "description":"安全风险属性"
        },
        {
            "dataType":"string",
            "length":"2000",
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"RectificationRequirements",
            "title":"整改要求",
            "mappedName":"RectificationRequirements",
            "description":"整改要求"
        },
        {
            "dataType":"datetime",
            "length":8,
            "scale":4,
            "mapped":true,
            "nullable":true,
            "name":"ProfessionalAcceptTime",
            "title":"专业验收时间",
            "mappedName":"ProfessionalAcceptTime",
            "description":"专业验收时间"
        },
        {
            "dataType":"string",
            "length":50,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"FinderTelphone",
            "title":"发现人电话",
            "mappedName":"FinderTelphone",
            "description":"发现人电话"
        },
        {
            "dataType":"int",
            "length":4,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"SyncId",
            "title":"同步Id",
            "mappedName":"SyncId",
            "description":"同步Id"
        },
        {
            "dataType":"bool",
            "length":1,
            "scale":0,
            "mapped":true,
            "nullable":true,
            "name":"Synced",
            "title":"已同步",
            "mappedName":"Synced",
            "description":"已同步"
        }
    ]
}


*/

/*
            #region ===== 安全审核记录 (tb_sc_record) =====

            tableName = "tb_sc_record";
            ret = await this.InternalCheckAddTable(tableName, "安全审核记录");
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

            ret = await this.InternalCheckAddTableField(tableName, "CreatorName", "string", "50", "0", "创建者姓名");
            result += "<br />" + "为表 " + tableName + " 添加字段 CreatorName string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Code", "string", "50", "0", "编号");
            result += "<br />" + "为表 " + tableName + " 添加字段 Code string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Finder", "int", "4", "0", "发现人");
            result += "<br />" + "为表 " + tableName + " 添加字段 Finder int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "FindTime", "datetime", "8", "4", "发现时间");
            result += "<br />" + "为表 " + tableName + " 添加字段 FindTime datetime " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "PlanFinishTime", "datetime", "8", "4", "预计完成时间");
            result += "<br />" + "为表 " + tableName + " 添加字段 PlanFinishTime datetime " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "FindDepartment", "int", "50", "0", "发现部门");
            result += "<br />" + "为表 " + tableName + " 添加字段 FindDepartment int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "SourceType", "int", "4", "0", "问题来源");
            result += "<br />" + "为表 " + tableName + " 添加字段 SourceType int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "SouceSubType", "int", "4", "0", "问题来源2");
            result += "<br />" + "为表 " + tableName + " 添加字段 SouceSubType int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "ResponsibleDepartment", "int", "4", "0", "责任部门");
            result += "<br />" + "为表 " + tableName + " 添加字段 ResponsibleDepartment int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "ResponsibleClass", "int", "4", "0", "责任班组");
            result += "<br />" + "为表 " + tableName + " 添加字段 ResponsibleClass int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "ResponsiblePerson", "int", "4", "0", "责任人");
            result += "<br />" + "为表 " + tableName + " 添加字段 ResponsiblePerson int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Submitter", "int", "4", "0", "提交人");
            result += "<br />" + "为表 " + tableName + " 添加字段 Submitter int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "SubmitTime", "datetime", "8", "4", "提交时间");
            result += "<br />" + "为表 " + tableName + " 添加字段 SubmitTime datetime " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Receiver", "int", "4", "0", "接收人");
            result += "<br />" + "为表 " + tableName + " 添加字段 Receiver int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "ReceiveTime", "datetime", "8", "4", "接收时间");
            result += "<br />" + "为表 " + tableName + " 添加字段 ReceiveTime datetime " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Acceptor", "int", "4", "0", "验收人");
            result += "<br />" + "为表 " + tableName + " 添加字段 Acceptor int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "AcceptTime", "datetime", "8", "4", "验收时间");
            result += "<br />" + "为表 " + tableName + " 添加字段 AcceptTime datetime " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Content", "string", "2000", "0", "问题描述");
            result += "<br />" + "为表 " + tableName + " 添加字段 Content string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Result", "string", "2000", "0", "验收结果");
            result += "<br />" + "为表 " + tableName + " 添加字段 Result string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Measures", "string", "2000", "0", "防范和应急措施");
            result += "<br />" + "为表 " + tableName + " 添加字段 Measures string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "RectifiedSchedule", "string", "2000", "0", "整改进度");
            result += "<br />" + "为表 " + tableName + " 添加字段 RectifiedSchedule string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "FlowId", "int", "4", "0", "流程Id");
            result += "<br />" + "为表 " + tableName + " 添加字段 FlowId int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "FlowStatus", "string", "50", "0", "流程状态");
            result += "<br />" + "为表 " + tableName + " 添加字段 FlowStatus string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "DangerType", "string", "50", "0", "是否隐患");
            result += "<br />" + "为表 " + tableName + " 添加字段 DangerType string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "DangerLevel", "string", "50", "0", "隐患等级");
            result += "<br />" + "为表 " + tableName + " 添加字段 DangerLevel string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "BreakRulesType", "string", "50", "0", "安全风险属性");
            result += "<br />" + "为表 " + tableName + " 添加字段 BreakRulesType string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "RectificationRequirements", "string", "2000", "0", "整改要求");
            result += "<br />" + "为表 " + tableName + " 添加字段 RectificationRequirements string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "ProfessionalAcceptTime", "datetime", "8", "4", "专业验收时间");
            result += "<br />" + "为表 " + tableName + " 添加字段 ProfessionalAcceptTime datetime " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "FinderTelphone", "string", "50", "0", "发现人电话");
            result += "<br />" + "为表 " + tableName + " 添加字段 FinderTelphone string " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "SyncId", "int", "4", "0", "同步Id");
            result += "<br />" + "为表 " + tableName + " 添加字段 SyncId int " + (ret == "" ? "OK." : ret);

            ret = await this.InternalCheckAddTableField(tableName, "Synced", "bool", "1", "0", "已同步");
            result += "<br />" + "为表 " + tableName + " 添加字段 Synced bool " + (ret == "" ? "OK." : ret);

            #endregion ===== 安全审核记录 (tb_sc_record) =====
*/

/*
            #region ===== 安全审核记录 (tb_sc_record) =====

            tableName = "tb_sc_record";
            ret = await this.InternalCheckAddTable(tableName, "安全审核记录");
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

            ret = await this.InternalCheckAddTableField(tableName, "CreatorName", "string", "50", "0", "创建者姓名");
            result.Add("为表 " + tableName + " 添加字段 CreatorName string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Code", "string", "50", "0", "编号");
            result.Add("为表 " + tableName + " 添加字段 Code string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Finder", "int", "4", "0", "发现人");
            result.Add("为表 " + tableName + " 添加字段 Finder int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "FindTime", "datetime", "8", "4", "发现时间");
            result.Add("为表 " + tableName + " 添加字段 FindTime datetime " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "PlanFinishTime", "datetime", "8", "4", "预计完成时间");
            result.Add("为表 " + tableName + " 添加字段 PlanFinishTime datetime " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "FindDepartment", "int", "50", "0", "发现部门");
            result.Add("为表 " + tableName + " 添加字段 FindDepartment int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "SourceType", "int", "4", "0", "问题来源");
            result.Add("为表 " + tableName + " 添加字段 SourceType int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "SouceSubType", "int", "4", "0", "问题来源2");
            result.Add("为表 " + tableName + " 添加字段 SouceSubType int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "ResponsibleDepartment", "int", "4", "0", "责任部门");
            result.Add("为表 " + tableName + " 添加字段 ResponsibleDepartment int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "ResponsibleClass", "int", "4", "0", "责任班组");
            result.Add("为表 " + tableName + " 添加字段 ResponsibleClass int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "ResponsiblePerson", "int", "4", "0", "责任人");
            result.Add("为表 " + tableName + " 添加字段 ResponsiblePerson int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Submitter", "int", "4", "0", "提交人");
            result.Add("为表 " + tableName + " 添加字段 Submitter int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "SubmitTime", "datetime", "8", "4", "提交时间");
            result.Add("为表 " + tableName + " 添加字段 SubmitTime datetime " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Receiver", "int", "4", "0", "接收人");
            result.Add("为表 " + tableName + " 添加字段 Receiver int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "ReceiveTime", "datetime", "8", "4", "接收时间");
            result.Add("为表 " + tableName + " 添加字段 ReceiveTime datetime " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Acceptor", "int", "4", "0", "验收人");
            result.Add("为表 " + tableName + " 添加字段 Acceptor int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "AcceptTime", "datetime", "8", "4", "验收时间");
            result.Add("为表 " + tableName + " 添加字段 AcceptTime datetime " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Content", "string", "2000", "0", "问题描述");
            result.Add("为表 " + tableName + " 添加字段 Content string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Result", "string", "2000", "0", "验收结果");
            result.Add("为表 " + tableName + " 添加字段 Result string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Measures", "string", "2000", "0", "防范和应急措施");
            result.Add("为表 " + tableName + " 添加字段 Measures string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "RectifiedSchedule", "string", "2000", "0", "整改进度");
            result.Add("为表 " + tableName + " 添加字段 RectifiedSchedule string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "FlowId", "int", "4", "0", "流程Id");
            result.Add("为表 " + tableName + " 添加字段 FlowId int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "FlowStatus", "string", "50", "0", "流程状态");
            result.Add("为表 " + tableName + " 添加字段 FlowStatus string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "DangerType", "string", "50", "0", "是否隐患");
            result.Add("为表 " + tableName + " 添加字段 DangerType string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "DangerLevel", "string", "50", "0", "隐患等级");
            result.Add("为表 " + tableName + " 添加字段 DangerLevel string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "BreakRulesType", "string", "50", "0", "安全风险属性");
            result.Add("为表 " + tableName + " 添加字段 BreakRulesType string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "RectificationRequirements", "string", "2000", "0", "整改要求");
            result.Add("为表 " + tableName + " 添加字段 RectificationRequirements string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "ProfessionalAcceptTime", "datetime", "8", "4", "专业验收时间");
            result.Add("为表 " + tableName + " 添加字段 ProfessionalAcceptTime datetime " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "FinderTelphone", "string", "50", "0", "发现人电话");
            result.Add("为表 " + tableName + " 添加字段 FinderTelphone string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "SyncId", "int", "4", "0", "同步Id");
            result.Add("为表 " + tableName + " 添加字段 SyncId int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Synced", "bool", "1", "0", "已同步");
            result.Add("为表 " + tableName + " 添加字段 Synced bool " + (ret == "" ? "OK." : ret));

            #endregion ===== 安全审核记录 (tb_sc_record) =====
*/
