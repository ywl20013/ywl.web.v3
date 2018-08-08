using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Ywl.Web.Api.Controllers
{

    public class DbApiController<TContext> : ApiController where TContext : Data.Entity.DbContext, new()
    {
        protected TContext db = new TContext();

        //public virtual string Json(object data)
        //{
        //    if (data == null) return "null";
        //    var settings = new Newtonsoft.Json.JsonSerializerSettings
        //    {
        //        //这句是解决问题的关键,也就是json.net官方给出的解决配置选项.                
        //        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize,
        //        MaxDepth = 5
        //    };
        //    var scriptSerializer = Newtonsoft.Json.JsonSerializer.Create(settings);
        //    using (var sw = new System.IO.StringWriter())
        //    {
        //        scriptSerializer.Serialize(sw, data);
        //        return sw.ToString();
        //    }
        //}

        // GET api/values
        //public virtual IEnumerable<string> Get()
        //{
        //    return new string[] { "api value1", "api value2" };
        //}

        //// GET api/values/5
        //public virtual string Get(int id)
        //{
        //    return "api single value";
        //}

        //// POST api/values
        //public virtual void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //public virtual void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public virtual void Delete(int id)
        //{
        //}

        private string convertOracleType(string source)
        {
            switch (source.ToLower())
            {
                case "datetime":
                    return "Date";
                case "string":
                    return "nvarchar2";
                case "int":
                    return "number";
                case "double":
                    return "number";
                case "bool":
                    return "number";
                default:
                    return source;
            }
        }
        private string convertMsSqlType(string source)
        {
            switch (source.ToLower())
            {
                case "datetime":
                    return "DateTime";
                case "string":
                    return "nvarchar";
                case "double":
                    return "numeric";
                case "bool":
                    return "numeric";
                default:
                    return source;
            }
        }

        /// <summary>
        /// 检查添加数据库表
        /// </summary>
        /// <param name="Table">表名称</param>
        /// <param name="TableDescription">表描述</param>
        /// <returns></returns>
        protected async Task<string> InternalCheckAddTable(string Table, string TableDescription)
        {
            StringBuilder sb = new StringBuilder();
            if (db.GetDbType() == Ywl.Data.Entity.DbContext.DataBaseType.Oracle)
            {
                sb.AppendLine("");
                sb.AppendLine("declare");
                sb.AppendLine("  v_ret int;");
                sb.AppendLine("begin");
                sb.AppendLine("  select case when exists(select * from user_tables");
                sb.AppendLine("  where lower(table_name) = lower('" + Table + "')) then 1 else 0 end into v_ret from dual;");
                sb.AppendLine("  if v_ret = 0 then");
                sb.AppendLine("    execute immediate 'create table " + Table + " (id number(10))';");
                sb.AppendLine("    execute immediate 'comment on table " + Table + " is ''" + TableDescription + "''';");
                sb.AppendLine("    execute immediate 'create sequence sq_" + Table + "';");
                sb.AppendLine("    execute immediate 'create or replace trigger tr_" + Table + " before insert on " + Table + " for each row begin select sq_" + Table + ".nextval into :new.id from dual;end;';");
                sb.AppendLine("  end if;");
                sb.AppendLine("end;");
            }
            else if (db.GetDbType() == Ywl.Data.Entity.DbContext.DataBaseType.MSSql)
            {
                sb.Clear();
                var sql = @"
declare 
	@TableName varchar(50), @sql nvarchar(max);
begin
	set @TableName = N'{0}';

    if not exists(select * from sysobjects where xtype=N'U' and name = @TableName) begin
		print 'not exists';
		set @sql = 'create table ' + @TableName + ' (id int identity (1, 1) not null,CONSTRAINT [PK_' + @TableName + '] PRIMARY KEY ([id]))';
		execute sp_executesql @stmt = @sql;
		execute sp_addextendedproperty @name = N'MS_Description', @value = N'{1}', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = @TableName;
	end;
end;
";
                sb.Append(String.Format(sql, Table, TableDescription));
            }
            try
            {
                if (sb.Length > 0)
                    await db.Database.ExecuteSqlCommandAsync(sb.ToString());
                return "";
            }
            catch (Exception e)
            {
                return sb.ToString() + "\n" + e.Message;
            }
        }

        /// <summary>
        /// 检查添加数据表字段
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="Field"></param>
        /// <param name="FieldType"></param>
        /// <param name="FieldDescription"></param>
        /// <returns></returns>
        protected async Task<string> InternalCheckAddTableField(string Table, string Field, string FieldType, string DataLength, string DataScale, string FieldDescription)
        {
            var _FieldType = "";
            StringBuilder sb = new StringBuilder();
            if (db.GetDbType() == Ywl.Data.Entity.DbContext.DataBaseType.Oracle)
            {
                _FieldType = convertOracleType(FieldType);
                switch (FieldType)
                {
                    case "string":
                        {
                            _FieldType += "(" + DataLength + ")";
                        }
                        break;
                    case "double":
                        {
                            _FieldType += "(" + DataLength + "," + DataScale + ")";
                        }
                        break;
                }
                sb.AppendLine("");
                sb.AppendLine("declare");
                sb.AppendLine("  v_ret int;");
                sb.AppendLine("begin");
                sb.AppendLine("  select case when exists(select * from user_tab_columns");
                sb.AppendLine("  where lower(table_name) = lower('" + Table + "')");
                sb.AppendLine("    and lower(column_name) = lower('" + Field + "')) then 1 else 0 end into v_ret from dual;");
                sb.AppendLine("  if v_ret = 0 then");
                sb.AppendLine("    execute immediate 'alter table " + Table + " add " + Field + " " + _FieldType + "';");
                sb.AppendLine("    execute immediate 'comment on column " + Table + "." + Field + " is ''" + FieldDescription + "''';");
                sb.AppendLine("  end if;");
                sb.AppendLine("end;");
            }
            else if (db.GetDbType() == Ywl.Data.Entity.DbContext.DataBaseType.MSSql)
            {
                _FieldType = convertMsSqlType(FieldType);
                switch (FieldType)
                {
                    case "string":
                        {
                            _FieldType += "(" + DataLength + ")";
                        }
                        break;
                    case "double":
                        {
                            _FieldType += "(" + DataLength + "," + DataScale + ")";
                        }
                        break;
                }

                sb.Clear();
                var sql = @"
declare 
	@TableName varchar(50), @FieldName varchar(50), @sql nvarchar(max);
begin
	set @TableName = N'{0}';
	set @FieldName = N'{1}';
	if exists(select 1 from sysobjects where xtype=N'U' and name = @TableName) begin
		if not exists(select 1 from syscolumns t1 where t1.name=@FieldName and exists(select 1 from sysobjects t2 where t2.xtype='U' and t2.name=@TableName and t1.id=t2.id))
		begin
			set @sql ='alter table ' + @TableName + ' add [' + @FieldName + '] {2} null;';
			execute sp_executesql @stmt = @sql;
			execute sp_addextendedproperty @name = N'MS_Description', @value = N'{3}', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = @TableName, @level2type = N'COLUMN', @level2name = @FieldName;
		end;
	end;
end;
";
                sb.Append(String.Format(sql, Table, Field, _FieldType, FieldDescription));
            }
            try
            {
                if (sb.Length > 0)
                    await db.Database.ExecuteSqlCommandAsync(sb.ToString());
                return "";
            }
            catch (Exception e)
            {
                return sb.ToString() + "\n" + e.Message;
            }
        }
    }
}
