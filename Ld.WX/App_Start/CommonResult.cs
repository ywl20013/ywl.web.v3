using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ld.WX
{
    public class CommonResult
    {
        public bool Success;
        public string ErrorMessage;
        public string errcode;

        public string ToJsonString() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
    public class ErrorJsonResult
    {
        public string errmsg;
        public string errcode;

        public string ToJsonString() => Newtonsoft.Json.JsonConvert.SerializeObject(this);

    }
}