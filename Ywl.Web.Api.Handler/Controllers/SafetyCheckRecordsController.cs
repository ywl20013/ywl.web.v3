using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Ywl.Web.Api;
using Ywl.Web.Api.Controllers;

namespace Ywl.Web.Api.Handler.Controllers
{
    public class SafetyCheckRecordsController : DbSheetApiController<Models.DbContext, Models.SafetyCheckRecord>
    {
        protected override IQueryable<Models.Moudle> InternalBeforeGet(DataTableRequest req, IQueryable<Models.Moudle> Items)
        {
            if (req.search != null && req.search.value != null)
            {
                Items = Items.Where(e => e.Name.Contains(req.search.value) || e.HierarchicalPath.Contains(req.search.value));
            }

            return base.InternalBeforeGet(req, Items);
        }
    }
}
