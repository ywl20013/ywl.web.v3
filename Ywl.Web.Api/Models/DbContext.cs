using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ywl.Web.Api.Models
{
    public class DbContext : Ywl.Data.Entity.DbContext
    {
        public System.Data.Entity.DbSet<User> Users { get; set; }

        /// <summary>
        /// 创建模型时的事件
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //一：数据库不存在时重新创建数据库
            //Database.SetInitializer<testContext>(new CreateDatabaseIfNotExists<testContext>());

            //二：每次启动应用程序时创建数据库
            //Database.SetInitializer<testContext>(new DropCreateDatabaseAlways<testContext>());

            //三：模型更改时重新创建数据库
            //Database.SetInitializer<testContext>(new DropCreateDatabaseIfModelChanges<testContext>());

            //四：从不创建数据库
            //Database.SetInitializer<testContext>(null);

            System.Data.Entity.Database.SetInitializer<DbContext>(null);

            base.OnModelCreating(modelBuilder);
        }
    }
}