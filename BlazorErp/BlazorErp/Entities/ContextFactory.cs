using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace BlazorErp.Entities
{
    /// <summary>
    /// Klasa koja kreira db kontekst za izvrsenje migracija
    /// </summary>
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        /// <summary>
        /// Kreira db kontekst
        /// </summary>
        /// <returns>Db kontekst</returns>
        public Context CreateDbContext(string[] args)
        {
            // dobavimo postavke aplikacije
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();

            // podesimo bazu
            var connectionString = configuration.GetConnectionString("BlazorContext");

            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseNpgsql(connectionString);

            // kreiramo kontekst
            return new Context(optionsBuilder.Options, null);
        }
    }
}
