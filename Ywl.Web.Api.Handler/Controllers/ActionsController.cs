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

    public class ActionsController : DbSheetApiController<Models.DbContext, Action>
    {
        protected override IQueryable<Action> InternalBeforeGet(DataTableRequest req, IQueryable<Action> Items)
        {
            Items = Items.Where(e => e.Status == Ywl.Data.Entity.Models.Status.Normal);
            var uid = Ywl.Data.Entity.Utils.StrToInt(Requests["uid"], null);
            if (uid != null)
            {
                var aids = from t in db.UserActions
                           where t.UserId == uid
                           select t.ActionId;
                return from t in db.Actions
                       where t.Status == Ywl.Data.Entity.Models.Status.Normal
                       && aids.Contains(t.Id)
                       select t;
            }

            return base.InternalBeforeGet(req, Items);
        }
        protected override List<Action> InternalBeforeGetReturnData(List<Action> Data)
        {
            //foreach (var item in Data)
            //{
            //    item.Moudle = db.Moudles.Find(item.MoudleId);
            //}
            return base.InternalBeforeGetReturnData(Data);
        }
    }
}
