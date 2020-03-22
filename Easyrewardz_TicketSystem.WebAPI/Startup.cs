using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.DBContext;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Easyrewardz_TicketSystem.WebAPI
{
    public class Startup
    {
        /// <summary>
        /// Start Up method
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ISecurity Security { get; }

        //This method gets called by the runtime.Use this method to add services to the container.
             public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
                 {
                     options.AddPolicy("AllowAll",
                         builder =>
                         {
                             builder
                             .AllowAnyOrigin()
                             .AllowAnyMethod()
                             .AllowAnyHeader();
                         });
                 });
            //services.AddMvc(
            //    config => {
            //        config.Filters.Add(typeof(CustomExceptionFilter));
            //    }
            //);
            services.AddOptions();

            ////Register Appsetting---------------------------------------------------------- 
            services.AddSingleton<IConfiguration>(Configuration);
            //services.AddSingleton<ISecurity>(Security);

            //services.AddDbContext<ETSContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EmployeeDatabase")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            ////----------Register Interface-------------------------------------------------
            services.AddTransient<ISecurity, SecurityService>();
            #region AuthenticationAddTransient
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = SchemesNamesConst.TokenAuthenticationDefaultScheme;

            })
            .AddScheme<TokenAuthenticationOptions, TokenAuthenticationHandler>(SchemesNamesConst.TokenAuthenticationDefaultScheme, o => { });

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = PermissionModuleConst.ModuleAuthenticationDefaultScheme;

            })
            .AddScheme<ModuleAuthenticationOptions, PermissionRequirement>(PermissionModuleConst.ModuleAuthenticationDefaultScheme, o => { });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            string CurrentDirectory = Directory.GetCurrentDirectory();

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseForwardedHeaders();
            app.UseStaticFiles();

            string Resources = "Resources";
            string ResourcesURL = Path.Combine(CurrentDirectory, Resources);
            if (!Directory.Exists(ResourcesURL))
            {
                Directory.CreateDirectory(ResourcesURL);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(ResourcesURL),
                RequestPath = "/" + Resources
            });
            //Enable directory browsing
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(ResourcesURL),
                RequestPath = "/" + Resources
            });

            string TicketAttachment = "TicketAttachment";
            string TicketAttachmentURL = Path.Combine(CurrentDirectory, TicketAttachment);
            if (!Directory.Exists(TicketAttachmentURL))
            {
                Directory.CreateDirectory(TicketAttachmentURL);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(TicketAttachmentURL),
                RequestPath = "/"+ TicketAttachment
            });
            //Enable directory browsing
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(TicketAttachmentURL),
                RequestPath = "/"+ TicketAttachment
            });

            string ReportDownload = "ReportDownload";
            string ReportDownloadURL = Path.Combine(CurrentDirectory, ReportDownload);
            if (!Directory.Exists(ReportDownloadURL))
            {
                Directory.CreateDirectory(ReportDownloadURL);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(ReportDownloadURL),
                RequestPath = "/"+ ReportDownload
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(ReportDownloadURL),
                RequestPath = "/" + ReportDownload
            });
            //         app.UseCors(
            //    options => options.WithOrigins("*").AllowAnyMethod()
            //);
            app.UseMvc();
        }


        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddCors(options =>
        //    {
        //        options.AddPolicy("AllowAll",
        //            builder =>
        //            {
        //                builder
        //                .AllowAnyOrigin()
        //                .AllowAnyMethod()
        //                .AllowAnyHeader();
        //            });
        //    });

        //    services.AddCors();
        //    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        //    services.Configure<MvcOptions>(options =>
        //    {
        //        options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAll"));
        //    });
        //}

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }
        //    else
        //    {
        //        app.UseHsts();
        //    }

        //    app.UseCors("AllowAll");
        //    app.UseAuthentication();
        //    //app.UseHttpsRedirection();
        //    app.UseMvc();
        //}
    }
}
