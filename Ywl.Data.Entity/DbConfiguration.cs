using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;

namespace Ywl.Data.Entity
{
    public class DbConfiguration : System.Data.Entity.DbConfiguration
    {
        public DbConfiguration()
        {
            //SetExecutionStrategy("System.Data.SqlClient", () => new System.Data.Entity.SqlServer.SqlAzureExecutionStrategy());
            DbInterception.Add(new InterceptorTransientErrors());
            DbInterception.Add(new EFInterceptorLogging());
        }
    }
}
