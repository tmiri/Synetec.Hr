using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synetec.Hr.Core.Services;
using Synetec.Hr.Core.Services.Administration;
using Synetec.Hr.Core.Services.Users;
using Synetec.Hr.Core.UnitsOfWork;
using Synetec.Hr.Database;
using Synetec.Hr.Database.Entities;
using Synetec.Hr.UnitOfWork.BaseUoW;

namespace Synetec.Hr.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<DbContext, SynetecHrDbContext>();
            var connectionString = Configuration["ConnectionStrings:SynetecHrConnectionString"];
            services.AddDbContext<SynetecHrDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<SynetecHrDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddScoped<IBaseUoW, BaseUoW>();
            services.AddScoped<IAccountUoW, AccountUoW>();
            services.AddScoped<IAdministrationUoW, AdministrationUoW>();

            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<SynetecHrDbContext>().Database.Migrate();
                    SynetecHrDbInitializer.SeedData(userManager, roleManager);
                }
            }
            catch (Exception ex)
            {
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
