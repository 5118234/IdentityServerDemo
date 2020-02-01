using CoreDX.Application.EntityFrameworkCore;
using CoreDX.Domain.Entity.Identity;
using IdentityServerAdmin.Admin.EntityFramework.Shared.DbContexts;
using IdentityServerAdmin.Admin.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

#if !DEBUG

using System.Diagnostics;

#endif

namespace IdentityServerAdmin.Admin
{
    public class Program
    {
        private const string SeedArgs = "/seed";

        public static async Task Main(string[] args)
        {
            #region ȷ�����а�Ĺ���Ŀ¼��ȷ

#if !DEBUG
            //ͨ�������ֶ�����Ӧ�ÿ��ܵ��¹���Ŀ¼������ļ�����Ŀ¼��һ�£���Ҫ���������������ļ������������޷����أ����޷���ģʽ������ģʽ�޸Ĺ���Ŀ¼Ҳ���ܵ������ú����������޷����أ�
            var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            var pathToContentRoot = Path.GetDirectoryName(pathToExe);
            Directory.SetCurrentDirectory(pathToContentRoot);
#endif

            #endregion

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            try
            {
                var seed = args.Any(x => x == SeedArgs);
                if (seed) args = args.Except(new[] { SeedArgs }).ToArray();

                var host = CreateHostBuilder(args).Build();

                // Uncomment this to seed upon startup, alternatively pass in `dotnet run /seed` to seed using CLI
                // await DbMigrationHelpers.EnsureSeedData<IdentityServerConfigurationDbContext, AdminIdentityDbContext, IdentityServerPersistedGrantDbContext, AdminLogDbContext, AdminAuditLogDbContext, UserIdentity, UserIdentityRole>(host);
                if (seed)
                {
                    await DbMigrationHelpers
                        .EnsureSeedData<IdentityServerConfigurationDbContext, ApplicationIdentityDbContext,
                            IdentityServerPersistedGrantDbContext, AdminLogDbContext, AdminAuditLogDbContext,
                            ApplicationUser, ApplicationRole, int>(host);
                }

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("serilog.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((hostContext, configApp) =>
                 {
                     configApp.AddJsonFile("serilog.json", optional: true, reloadOnChange: true);
                     configApp.AddJsonFile("identitydata.json", optional: true, reloadOnChange: true);
                     configApp.AddJsonFile("identityserverdata.json", optional: true, reloadOnChange: true);

                     if (hostContext.HostingEnvironment.IsDevelopment())
                     {
                         configApp.AddUserSecrets<Startup>();
                     }

                     configApp.AddEnvironmentVariables();
                     configApp.AddCommandLine(args);
                 })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options => options.AddServerHeader = false);
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((hostContext, loggerConfig) =>
                {
                    loggerConfig
                        .ReadFrom.Configuration(hostContext.Configuration)
                        .Enrich.WithProperty("ApplicationName", hostContext.HostingEnvironment.ApplicationName);
                });
    }
}





