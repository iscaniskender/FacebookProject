using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FacebookProject.Hubs;
using FacebookProject.identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace FacebookProject
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
            services.AddControllersWithViews();

            //services.AddSignalR();

            //services.AddDbContext<FacebookContext>(options => options.UseSqlServer(Configuration["ConnectionStr"]));

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration["ConnectionStr"]));

            //AddDefaultTokenProviders parola sýfýrlama için token veriyor.

            services.AddIdentity<UserTable, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                //password
                //mutlaka rakam gir
                options.Password.RequireDigit = true;
                //küçükharf oalcak  
                options.Password.RequireLowercase = true;
                //bütükharf olacak
                options.Password.RequireUppercase = true;
                //mininm deger 
                options.Password.RequiredLength = 8;
                //alfanumaric
                options.Password.RequireNonAlphanumeric = true;

                //Lockout
                //5defadeneme
                options.Lockout.MaxFailedAccessAttempts = 5;
                //60sn bekleme
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(60);
                //kullanýcý devam etsin
                options.Lockout.AllowedForNewUsers = true;

                //optionsuser
                //options.User.AllowedUserNameCharacters = "";
                //riqure email
                options.User.RequireUniqueEmail = true;
                // email onaylama
                options.SignIn.RequireConfirmedEmail = false;
                //phonenumber onaylama
                //options.SignIn.RequireConfirmedPhoneNumber=true;
            });
            services.ConfigureApplicationCookie(options =>
            {
                //cookie beni tanýmazsa þu sayfaya git
                options.LoginPath = "/account/login";
                // cookie beni silsin
                options.LogoutPath = "/account/logout";
                // istek yenileme 20dkyý sýfýrla
                options.SlidingExpiration = true;
                //20dkyý uzatma
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.Cookie = new CookieBuilder
                {
                    //sadece http request alýr
                    HttpOnly = true,
                    Name = ".Facebook.Security.Cookie"
                };
            });

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


            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=LoginIndex}/{id?}");
                //endpoints.MapHub<ChatHub>("/Home/Index");
            });
        }
    }
}
