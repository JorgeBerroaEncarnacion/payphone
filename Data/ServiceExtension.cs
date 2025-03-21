using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public static class ServiceExtension
{
    public static void AddCustomDbContext<TContext>(this IServiceCollection services, IConfiguration config)
    where TContext : DbContext
    {
        var provider = config["DatabaseConfig:Provider"];
        const int maxRetryCount = 3;
        var maxRetryDelay = TimeSpan.FromSeconds(10);

        services.AddDbContext<TContext>(options =>
        {
            options.UseLazyLoadingProxies();
            if (string.IsNullOrEmpty(provider)) provider = "sqlserver";

            var mysqlConnection = GetConnectionString(config, "MySqlConnection");
            options.UseMySql(mysqlConnection, ServerVersion.AutoDetect(mysqlConnection), mySqlOptions =>
            {
                mySqlOptions.EnableRetryOnFailure(maxRetryCount, maxRetryDelay, null);
            });
        });
    }

    private static string GetConnectionString(IConfiguration config, string name)
    {
        var connectionString = config.GetConnectionString(name);
        if (!string.IsNullOrEmpty(connectionString)) return connectionString;

        return connectionString;
    }
}
