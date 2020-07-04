using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using BlazorErp.Services.Registration;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using BlazorErp.Entities;
using BlazorErp.Entities.Models.Korisnik;
using IdentityServer4.Stores;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using BlazorErp.Server.Areas.Identity;
using BlazorErp.Server.Common.Services;
using BlazorErp.Services.Definition.Base;

namespace BlazorErp.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            services.AddDbContext<Context>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("BlazorContext")));

            services.AddDefaultIdentity<IdentityKorisnik>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Lockout.AllowedForNewUsers = false;
            })

                .AddRoles<Uloga>()
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();

            //var cert = new X509Certificate2(Path.Combine(".", "Certificates", "IdentityServer4Auth.pfx"), "",
            //    X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);


            services.AddIdentityServer()
                .AddApiAuthorization<IdentityKorisnik, Context>(options =>
                {
                    options.IdentityResources.Clear();
                    options.IdentityResources.AddOpenId();
                    options.IdentityResources.AddEmail();
                    options.IdentityResources.AddProfile();
                })
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddProfileService<IdentityProfileService>();
                //.AddSigningCredential(cert)
                //.AddValidationKey(cert);


            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.Configure<UploadSettings>(Configuration.GetSection("Upload"));

            builder.RegisterModule<ServiceModule>();

            //dodajem implemetaciju ApplicationConfigurationService
            builder.RegisterType<ApplicationConfigurationService>().As<IApplicationConfigurationService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // dodaj asp.net servise u kontejner
            builder.Populate(services);
            // konstruisi kontejner
            var container = builder.Build();

            // create the IServiceProvider based on the container.
            //return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // seed database
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<Context>();
                context.EnsureSeedData();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
