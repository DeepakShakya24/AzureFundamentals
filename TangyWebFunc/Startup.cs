using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TangyWebFunc;
using TangyWebFunc.Data;

[assembly: FunctionsStartup(typeof(Startup))]
namespace TangyWebFunc
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connStr = "Server=tcp:azfundamentalserver.database.windows.net,1433;Initial Catalog=azFundamemtaLDB;Persist Security Info=False;User ID=adminsql;Password=deepak@123@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";//Environment.GetEnvironmentVariable("SqlDbConn");
            builder.Services.AddDbContext<TangyFuncDbContext>(op => op.UseSqlServer(connStr));

            //builder.Services.BuildServiceProvider();
        }
    }
}
