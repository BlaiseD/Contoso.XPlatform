using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Bsl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
            //Data Source=tcp:bpswebsiteresourcegroupsqlserver.database.windows.net,1433;Initial Catalog=contosoDB;User Id=contosoUser@bpswebsiteresourcegroupsqlserver;Password=contosoPassword01
            //NLog.GlobalDiagnosticsContext.Set("DefaultConnection", config.GetConnectionString("DefaultConnection"));
            NLog.GlobalDiagnosticsContext.Set("DefaultConnection", "Data Source=tcp:bpswebsiteresourcegroupsqlserver.database.windows.net,1433;Initial Catalog=contosoDB;User Id=contosoUser@bpswebsiteresourcegroupsqlserver;Password=contosoPassword01");

            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            })
            .UseNLog();  // NLog: Setup NLog for Dependency injection
    }
}
