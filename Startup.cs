using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EmployeeDeactivation.Models;
using EmployeeDeactivation.Interface;
using EmployeeDeactivation.BusinessLayer;
using EmployeeDeactivation.Data;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace EmployeeDeactivation
{
    public class Startup
    {
        //private IPdfDataOperation _pdfDataOperation;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAuthentication(AzureADDefaults.AuthenticationScheme).AddAzureAD(options => Configuration.Bind("AzureAd", options)).AddCookie();

            services.AddDbContext<EmployeeDeactivationContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("EmployeeDeactivationContext")));
           
            services.AddScoped<IEmployeeDataOperation, EmployeeDataOperation>();
            services.AddScoped<IPdfDataOperation, PdfDataOperation>();
            services.AddScoped<IAdminDataOperation, AdminDataOperation>();
            services.AddScoped<IManagerAprovalOperation, ManagerAprovalOperation>();
            services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
             {
                 options.Authority = options.Authority + "/v2.0/";
                 options.TokenValidationParameters.ValidateIssuer = false;
                 options.RequireHttpsMetadata = true;
             });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Manager", policyBuilder => policyBuilder.RequireClaim("groups", "3b080549-368c-4755-8fff-599d311fcb79"));
            });


            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

                options.Filters.Add(new AuthorizeFilter(policy));


            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(
                routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Employees}/{action=Create}/{id?}");
            });
        }
    }
}
