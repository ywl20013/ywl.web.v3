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

namespace Ywl.Web.Api.Handler.Controllers
{
    using Ywl.Data.Entity.Models;
    using Ywl.Web.Api;
    using Ywl.Web.Api.Controllers;

    public class MoudlesController : DbSheetApiController<Models.DbContext, Moudle>
    {
        protected override IQueryable<Moudle> InternalBeforeGet(DataTableRequest req, IQueryable<Moudle> Items)
        {
            Items = Items.Where(e => e.Status == Ywl.Data.Entity.Models.Status.Normal);
            var uid = Ywl.Data.Entity.Utils.StrToInt(Requests["uid"], null);
            if (uid != null)
            {
                var aids = from t in db.UserActions
                           where t.UserId == uid
                           select t.ActionId;
                var mids = from t in db.Actions
                           where aids.Contains(t.Id)
                           select t.MoudleId;
                var q = from t in Items
                        where mids.Contains(t.Id) || t.NeedPower == false
                        orderby t.ParentId, t.Orderno
                        select t;
                return q;
            }
            if (req.search != null && req.search.value != null)
            {
                Items = Items.Where(e => e.Name.Contains(req.search.value) || e.HierarchicalPath.Contains(req.search.value));
            }

            return base.InternalBeforeGet(req, Items);
        }
    }
}
