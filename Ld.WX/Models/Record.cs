using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wx_api_test.Models
{
    public class Record
    {
        public int id { get; set; }
        public string content { get; set; }
        public bool isdanger { get; set; }

        public string ToJsonString() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}