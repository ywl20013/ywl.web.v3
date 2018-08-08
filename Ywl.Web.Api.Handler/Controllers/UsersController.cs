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
    using Ywl.Web.Api.Handler.Models;

    public class UsersController : DbSheetApiController<Models.DbContext, User>
    {
        protected override IQueryable<User> InternalBeforeGet(DataTableRequest req, IQueryable<User> Items)
        {
            Items = Items.Where(e => e.Status == Ywl.Data.Entity.Models.Status.Normal);

            //判断权限
            var controller = Requests["controller"];
            var action = Requests["action"];
            //var uid = Ywl.Data.Entity.Utils.StrToInt(Requests["uid"], null);
            if (controller != null && action != null)
            {
                var aids = from t in db.Actions
                           where t.Status == Ywl.Data.Entity.Models.Status.Normal
                           && t.Controller.ToLower() == controller.ToLower()
                           && t.Name.ToLower() == action.ToLower()
                           select t.Id;
                var uids = from t in db.UserActions
                           where aids.Contains(t.ActionId)
                           select t.UserId;

                if (uids.Count() == 0)
                {
                    Items = from t in Items where 1 == 2 select t;
                    return Items;
                }
                else
                {
                    Items = from t in Items
                            where uids.Contains(t.Id)
                            select t;
                }
            }

            if (req.search != null && req.search.value != null)
            {
                var value = req.search.value;
                return Items.Where(e => e.Name.Contains(req.search.value) || e.OrganizationPath.Contains(req.search.value));
            }

            if (req.columns != null && req.columns.Count() == 1)
            {
                //临时使用800工号查询用户
                if (req.columns[0].data.ToLower() == "account")
                {
                    string value = req.columns[0].search.value;
                    return Items.Where(e => e.Account.Equals(value));
                }
                //临时使用 wxopenid 查询用户
                else if (req.columns[0].data.ToLower() == "wxopenid")
                {
                    string value = req.columns[0].search.value;
                    return Items.Where(e => e.WxOpenId.Equals(value));
                }
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
                    else if (data.ToLower() == "Account".ToLower())
                    {
                        return Items.Where(e => e.Account.Equals(search_value));
                    }
                    else if (data.ToLower() == "WxOpenId".ToLower())
                    {
                        return Items.Where(e => e.WxOpenId.Equals(search_value));
                    }
                    else if (data.ToLower() == "OrgId".ToLower())
                    {
                        var intVal = Ywl.Data.Entity.Utils.StrToInt(search_value, null);
                        if (intVal.HasValue)
                            Items = Items.Where(e => e.OrganizationId == intVal);
                    }
                    else if (data.ToLower() == "DepId".ToLower())
                    {
                        var intVal = Ywl.Data.Entity.Utils.StrToInt(search_value, null);
                        if (intVal.HasValue)
                            Items = Items.Where(e => e.DepId == intVal);
                    }
                    else if (data.ToLower() == "GroupId".ToLower())
                    {
                        var intVal = Ywl.Data.Entity.Utils.StrToInt(search_value, null);
                        if (intVal.HasValue)
                            Items = Items.Where(e => e.GroupId == intVal);
                    }
                }
            }




            return base.InternalBeforeGet(req, Items);
        }

        protected override IQueryable<User> InternalBeforeGet(string id)
        {
            int? _id = Ywl.Data.Entity.Utils.StrToInt(id, null);
            if (_id == null)
            {
                return db.Users.Where(e => e.WxOpenId == id);
            }
            return base.InternalBeforeGet(id);
        }
    }
}
