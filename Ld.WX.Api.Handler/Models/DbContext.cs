using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ld.WX.Api.Handler.Models
{
    public class DbContext : Ywl.Web.Api.Handler.Models.DbContext
    {
        public System.Data.Entity.DbSet<Ld.WX.Models.SafetyRecord> SafetyRecords { get; set; }
        public System.Data.Entity.DbSet<Ld.WX.Models.SafetyRecordAttachment> SafetyRecordAttachments { get; set; }
        public System.Data.Entity.DbSet<Ld.WX.Models.SafetyRecordCheckHistory> SafetyRecordCheckHistories { get; set; }
        public System.Data.Entity.DbSet<Ld.WX.Models.Organization> Organizations { get; set; }
    }
}