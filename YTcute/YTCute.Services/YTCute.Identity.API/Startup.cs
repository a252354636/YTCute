using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using YTCute.Identity.API.Configuration;
using YTCute.Identity.API.Data;
using YTCute.Identity.API.Services;
using Autofac.Extensions.DependencyInjection;
using YTCute.Identity.API.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace YTCute.Identity.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<EFContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));//注入DbContext

            //services.AddIdentity<Users, IdentityRole>()
            //    .AddEntityFrameworkStores<EFContext>()
            //   .AddDefaultTokenProviders();



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<IUsersService, UsersService>();//service注入


            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
               // .AddAspNetIdentity<Users>()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                 //.AddTestUsers(Config.GetUsers())
                 .Services.AddTransient<IProfileService, ProfileService>();
            ;//添加测试用户



            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseIdentityServer();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
