using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ywl.Web.Api.Handler.Controllers
{
    public class DbSheetApiController<TContext, TEntity> : Ywl.Web.Api.Controllers.DbSheetApiController<TContext, TEntity>
        where TContext : Models.DbContext, new()
        where TEntity : Data.Entity.Models.Entity, new()
    {
        protected override IQueryable<TEntity> InternalBeforeGet(DataTableRequest req, IQueryable<TEntity> Items)
        {
            return base.InternalBeforeGet(req, Items);
        }
    }
}