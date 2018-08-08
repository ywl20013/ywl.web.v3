using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ywl.Data.Entity.Models;

namespace Ld.WX.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Apps()
        {
            return View();
        }
        public ActionResult Me()
        {
            return View();
        }

        public ActionResult CurrentUser()
        {
            var user = (User)Session["current_user"];
            return Json(new
            {
                user.Id,
                user.Account,
                user.Name,
                user.OrganizationId,
                user.OrganizationPath,
                user.HeadImagePath,
                user.DepId,
                user.DepName,
                user.GroupId,
                user.GroupName
            }, JsonRequestBehavior.AllowGet);
        }
    }
}