using Business.Infrastructure;
using Business.Service;
using Domain.Context;
using Domain.EF_Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; //using Business.Service;
using System;
using AutoMapper;
using IStore_WEB_.MapperProfile;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace IStore_WEB_
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddControllersWithViews();
            services.AddIdentity<User, IdentityRole>(op =>
            {
                op.Password.RequireDigit = false;
                op.Password.RequireLowercase = false;
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequireUppercase = false;
                op.Password.RequiredLength = 6;
                op.Password.RequiredUniqueChars = 0;
                op.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";

            }).AddEntityFrameworkStores<StoreContext>().AddDefaultTokenProviders().AddTokenProvider<EmailConfirmationTokenProvider<User>>("emailconfirmation");

            BusinessConfiguration.ConfigureServices(services, Configuration);
            services.Configure<SendGridSenderOptions>(op => Configuration.GetSection("SendGridSenderOptions").Bind(op));
            services.AddTransient<IEmailSender,EmailSenderService>();
            services.Configure<EmailConfiramtionProviderOption>(op => op.TokenLifespan = TimeSpan.FromDays(5));
     
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            services.AddAuthentication().AddCookie(op => op.LoginPath = "/login");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

         

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name:"conf",
                   pattern:"confirmation/",
                   defaults: new {controller="EmailConfirm",action = "Confirm" });

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}