using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ywl.Web.Api.Handler.Controllers
{
    public class DbController : Ywl.Web.Api.Controllers.DbApiController<Models.DbContext>
    {
        // GET api/values
        public virtual async Task<IEnumerable<string>> Get()
        {
            List<string> result = new List<string>();

            var ret = ""; var tableName = "";

            #region ===== 系统登录用户 (tb_sys_users) =====

            tableName = "tb_sys_users";
            ret = await this.InternalCheckAddTable(tableName, "系统登录用户");
            result.Add("添加表 " + tableName + " " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Id", "int", "4", "0", "Id");
            result.Add("为表 " + tableName + " 添加字段 Id int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Status", "string", "50", "0", "状态");
            result.Add("为表 " + tableName + " 添加字段 Status string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Name", "string", "50", "0", "名称");
            result.Add("为表 " + tableName + " 添加字段 Name string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Account", "string", "20", "undefined", "账户");
            result.Add("为表 " + tableName + " 添加字段 Account string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Password", "string", "50", "undefined", "密码");
            result.Add("为表 " + tableName + " 添加字段 Password string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Sex", "string", "4", "undefined", "性别");
            result.Add("为表 " + tableName + " 添加字段 Sex string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "OrgId", "int", "undefined", "undefined", "组织机构编号");
            result.Add("为表 " + tableName + " 添加字段 OrgId int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "OrgPath", "string", "256", "undefined", "组织机构路径");
            result.Add("为表 " + tableName + " 添加字段 OrgPath string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "DepId", "int", "undefined", "undefined", "部门编号");
            result.Add("为表 " + tableName + " 添加字段 DepId int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "DepName", "string", "50", "undefined", "部门名称");
            result.Add("为表 " + tableName + " 添加字段 DepName string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "GroupId", "int", "undefined", "undefined", "班组编号");
            result.Add("为表 " + tableName + " 添加字段 GroupId int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "GroupName", "string", "50", "undefined", "班组名称");
            result.Add("为表 " + tableName + " 添加字段 GroupName string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "HeadImagePath", "string", "256", "undefined", "头像路径");
            result.Add("为表 " + tableName + " 添加字段 HeadImagePath string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "PhotoPath", "string", "256", "undefined", "照片路径");
            result.Add("为表 " + tableName + " 添加字段 PhotoPath string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "WxOpenId", "string", "50", "0", "微信公众号OpenId");
            result.Add("为表 " + tableName + " 添加字段 WxOpenId string " + (ret == "" ? "OK." : ret));

            #endregion ===== 系统登录用户 (tb_sys_users) =====

            #region ===== 系统模块,基础权限 (tb_sys_Moudles) =====

            tableName = "tb_sys_Moudles";
            ret = await this.InternalCheckAddTable(tableName, "系统模块,基础权限");
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

            ret = await this.InternalCheckAddTableField(tableName, "PId", "int", "undefined", "undefined", "上一级");
            result.Add("为表 " + tableName + " 添加字段 PId int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Url", "string", "256", "undefined", "链接");
            result.Add("为表 " + tableName + " 添加字段 Url string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "MType", "string", "50", "undefined", "类型");
            result.Add("为表 " + tableName + " 添加字段 MType string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Category", "string", "50", "undefined", "分类");
            result.Add("为表 " + tableName + " 添加字段 Category string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "NeedPower", "bool", "1", "0", "是否需要权限");
            result.Add("为表 " + tableName + " 添加字段 NeedPower bool " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "NameSpace", "string", "256", "undefined", "命名空间");
            result.Add("为表 " + tableName + " 添加字段 NameSpace string " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Path", "string", "50", "0", "层次路径");
            result.Add("为表 " + tableName + " 添加字段 Path string " + (ret == "" ? "OK." : ret));

            #endregion ===== 系统模块,基础权限 (tb_sys_Moudles) =====

            #region ===== 系统模块动作 (tb_sys_Actions) =====

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

            #region ===== 安全审核审核历史 (tb_sc_checkhistories) =====

            tableName = "tb_sc_checkhistories";
            ret = await this.InternalCheckAddTable(tableName, "安全审核审核历史");
            result.Add("添加表 " + tableName + " " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "Id", "int", "4", "0", "Id");
            result.Add("为表 " + tableName + " 添加字段 Id int " + (ret == "" ? "OK." : ret));

            ret = await this.InternalCheckAddTableField(tableName, "NodeName", "string", "20", "0", "步骤");
            result.Add("为表 " + tableName + " 添加字段 NodeName string " + (ret == "" ? "OK." : ret));

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

            result.Add("<br />检查完成。");
            return result.ToArray();
        }
    }
}