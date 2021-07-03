using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using J6.DAL.Database;
using AutoMapper;
using J6.BL.Mapper;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using J6.DAL.Entities;
using J6.BL.Servises;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using J6.Interfaces;
using J6.BL.Repositry;
using J6.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace J6
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DbContainer>(options => options
                .UseSqlServer(Configuration.GetConnectionString("J6DataBase")));

            services.AddIdentity<AppUser, AppRole>(options => {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 3;
            options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<DbContainer>();


            services.AddSignalR();



            services.AddScoped<ITokenServices, TokenService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
                       ValidateIssuer = false,
                       ValidateAudience = false,
                   };
               }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => Configuration.Bind("CookieSettings", options));

            services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            services.AddCors();

            

            services.AddControllersWithViews()
                    //.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                    //.AddDataAnnotationsLocalization()
                    .AddNewtonsoftJson(x => 
                    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //services.AddMvc(options =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //                    .RequireAuthenticatedUser()
            //                    .Build();
            //    options.Filters.Add(new AuthorizeFilter(policy));
            //});

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole",
                    policy => policy.RequireRole("Admin", "Moderator"));

                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role"));

                options.AddPolicy("EditRolePolicy",
                    policy => policy.RequireClaim("Edit Role"));
            });

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            //D.I.
            services.AddTransient<IRandomProducts, ProductServices>();

            services.AddScoped<IProductRepository, ProductRepositry>();

            services.AddTransient<IUserSavedBagServices, UserSavedBagServices>();

            services.AddScoped<IAdminStatisticsService, AdminStatisticsService>();

            services.AddTransient<IOrderServices, OrderServices>();

            services.AddTransient<IUserService, UserService>();

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
            }


            // Take Instance From Languages
            var supportedCultures = new[] {
                  new CultureInfo("ar-EG"),
                  new CultureInfo("en-US"),
            };


            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                }
            });


            

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(policy =>
            policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
                endpoints.MapHub<ChatHub>("/ChatHubs");
            });
        }
    }
}
