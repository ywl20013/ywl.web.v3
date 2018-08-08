using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ywl.Web.Api.Controllers;

namespace Ywl.Web.Api.Handler.Controllers
{
    public class OrganizationsController<TEntity, TDbContext> : DbSheetApiController<TDbContext, TEntity>
        where TEntity : Ywl.Data.Entity.Models.BaseOrganization, new()
        where TDbContext : Models.DbContext, new()
    {

        protected override IQueryable<TEntity> InternalBeforeGet(DataTableRequest req, IQueryable<TEntity> Items)
        {
            if (req.search != null && req.search.value != null)
            {
                var value = req.search.value;
                Items = Items.Where(e => e.Name.Contains(req.search.value));
            }

            if (req.columns != null)
            {
                foreach (var column in req.columns)
                {
                    var data = column.data;
                    var search_value = column.search.value;
                    if (data.ToLower() == "Name".ToLower())
                    {
                        Items = Items.Where(e => e.Name.Contains(search_value));
                    }
                    else if (data.ToLower() == "IsDepartment".ToLower())
                    {
                        if (search_value == "1" || search_value == "true")
                            Items = Items.Where(e => e.IsDepartment);
                    }
                    else if (data.ToLower() == "IsGroup".ToLower())
                    {
                        if (search_value == "1" || search_value == "true")
                            Items = Items.Where(e => e.IsGroup);
                    }
                    else if (data.ToLower() == "PId".ToLower())
                    {
                        var intVal = Ywl.Data.Entity.Utils.StrToInt(search_value, null);
                        if (intVal.HasValue)
                            Items = Items.Where(e => e.PId == intVal);
                    }
                }
            }
            return Items;
        }
    }
}