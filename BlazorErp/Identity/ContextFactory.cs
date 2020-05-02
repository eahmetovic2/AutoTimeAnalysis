using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BlazorErp.Identity
{
    /// <summary>
    /// Klasa koja kreira db kontekst za izvrsenje migracija
    /// </summary>
    public class ContextFactory : IDesignTimeDbContextFactory<IdentityContext>
    {
        /// <summary>
        /// Kreira db kontekst
        /// </summary>
        /// <returns>Db kontekst</returns>
        public IdentityContext CreateDbContext(string[] args)
        {
            // dobavimo postavke aplikacije
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();

            // podesimo bazu
            var connectionString = configuration.GetConnectionString("IdentityConnection");

            var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
            optionsBuilder.UseSqlServer(connectionString, p => p.UseRowNumberForPaging());

            // kreiramo kontekst
            return new IdentityContext(optionsBuilder.Options);
        }
    }
}
