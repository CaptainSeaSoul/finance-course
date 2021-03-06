using FinanceCourse.Areas.Identity.Data;
using FinanceCourse.Areas.Tools.Services;
using FinanceCourse.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPortfolio.Services;

namespace FinanceCourse
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
            // configure email
            EmailServerConfiguration config = Configuration.GetSection("EmailServerConfiguration").Get<EmailServerConfiguration>();
            services.AddSingleton<EmailServerConfiguration>(config);

            services.AddTransient<IEmailSender, MailKitEmailService>();

            // configure db
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            // configure Identity
            services.AddDefaultIdentity<FinanceCourseUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddHttpContextAccessor();

            // configure Tools
            services.AddSingleton<ToolComponentService>();

            // configure Trading
            AlpacaConfig alpacaConfig = Configuration.GetSection("AlpacaConfig").Get<AlpacaConfig>();
            services.AddSingleton<AlpacaConfig>(alpacaConfig);
            services.AddSingleton<TradingService>();

            // configure more
            services.AddRazorPages();
            services.AddServerSideBlazor()
                .AddHubOptions(options => options.MaximumReceiveMessageSize = 64 * 1024); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
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
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
            });
        }
    }
}
