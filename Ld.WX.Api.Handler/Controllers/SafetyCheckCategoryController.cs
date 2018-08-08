using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ld.WX.Api.Handler.Controllers
{
    using Ywl.Web.Api.Controllers;

    public class SafetyCheckCategoryController : DbApiController<Models.DbContext>
    {

        /*
         * 0	未填写
1	安全审核
2	专项检查
3	安全督导

         8	A类：人的行为
        9	B类：工具和工艺设备
        10	C类：人机工程
        11	D类：规章制度
        12	E类：作业环境和应急

         * */

        public class Result
        {
            public class Item
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }
            public int Total { get; set; }
            public int Filter { get; set; }
            public List<Item> Data { get; set; }
            public Result()
            {
                Data = new List<Item>();
            }
        }
        public Result Get()
        {
            var result = new Result();
            var _id = Ywl.Data.Entity.Utils.StrToInt(HttpContext.Current.Request["pid"], null);

            if (_id == null)
            {
                result.Total = 1;
                result.Filter = 1;
                result.Data.Add(new Result.Item { Id = 1, Name = "安全审核" });
                //result.Data.Add(new Result.Item { Id = 2, Name = "专项检查" });
                //result.Data.Add(new Result.Item { Id = 3, Name = "安全督导" });
            }
            else if (_id == 1)
            {
                result.Total = 5;
                result.Filter = 5;
                result.Data.Add(new Result.Item { Id = 8, Name = "A类：人的行为" });
                result.Data.Add(new Result.Item { Id = 9, Name = "B类：工具和工艺设备" });
                result.Data.Add(new Result.Item { Id = 10, Name = "C类：人机工程" });
                result.Data.Add(new Result.Item { Id = 11, Name = "D类：规章制度" });
                result.Data.Add(new Result.Item { Id = 12, Name = "E类：作业环境和应急" });
            }
            return result;
        }
        public Result Get(int id)
        {
            var result = new Result();

            if (id == 1)
            {
                result.Total = 5;
                result.Filter = 5;
                result.Data.Add(new Result.Item { Id = 8, Name = "A类：人的行为" });
                result.Data.Add(new Result.Item { Id = 9, Name = "B类：工具和工艺设备" });
                result.Data.Add(new Result.Item { Id = 10, Name = "C类：人机工程" });
                result.Data.Add(new Result.Item { Id = 11, Name = "D类：规章制度" });
                result.Data.Add(new Result.Item { Id = 12, Name = "E类：作业环境和应急" });
            }
            return result;
        }
    }
}