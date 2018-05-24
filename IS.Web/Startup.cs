using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using IS.Data.DbContexts;
using IS.Data.Interfaces;
using IS.Data.Model;
using IS.Data.Repositories;
using IS.Services.Interfaces;
using IS.Services.Services;
using IS.Web.Components;
using IS.Web.Components.Dtos;
using IS.Web.Components.ViewModels;
using Microsoft.ApplicationInsights.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IS.Web
{
    public class Startup
    {



        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.


            services.AddMvc();

            var connectionString = Configuration[ "ConnectionStrings:IsDbContext" ];
             services.AddDbContext<IsDbContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IRosterRepository, RosterRepository>();
            services.AddScoped<ISafetyZoneRepository, SafetyZoneRepository>();
            services.AddScoped<ISafetyZoneService, SafetyZoneService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddTransient<DepartmentFormService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>( );
            services.AddTransient<IDownloadService, DownloadService>();
            services.AddTransient<IDownloadRepository, DownloadRepository>();
            services.AddTransient<ITeamRosterService, TeamRosterService>();
            services.AddTransient<ITeamRosterRepository, TeamRosterRepository>();
            services.AddScoped<ISafetyTeamService, SafetyTeamService>();
            services.AddScoped<ISafetyTeamRepository, SafetyTeamRepository>();
            services.Configure<EmailSettingsModel>(Configuration.GetSection("Email"));
            

            services.AddIdentity<IgnitionUser, IdentityRole>()
                .AddEntityFrameworkStores<IsDbContext>( ).AddDefaultTokenProviders( ); 

           

            services.AddAuthorization( cfg =>
            {
                cfg.AddPolicy( "AccountOwners", p => p.RequireClaim( "AccountOwner", "True" ) );
                cfg.AddPolicy( "SuperUsers", p => p.RequireClaim( "SuperUsers", "True" ) );
                cfg.AddPolicy( "AccountManagers", p => p.RequireClaim( "AccountManager", "True" ) );
                cfg.AddPolicy( "AppUsers", p => p.RequireClaim( "AppUser", "True" ) );
            } );

            services.Configure<IdentityOptions> ( options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes ( 1 );
                options.Lockout.MaxFailedAccessAttempts = 10;

                // User settings
                options.User.RequireUniqueEmail = true;
            } );
            services.AddAutoMapper();
           

        }

       

       

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            Mapper.Initialize ( cfg =>
            {
                cfg.CreateMap<RegisterViewModel,Account> ( );
                cfg.CreateMap<SafetyZoneDto, SafetyZone>();
                cfg.CreateMap<Department, DepartmentDto>();
                cfg.CreateMap<RosterMember, TeamRosterMemberDto>();
                cfg.CreateMap<TeamRosterMemberDto, RosterMember>( ).ForMember( x => x.Department, opt => opt.Ignore( ) ); ;
                cfg.CreateMap<IgnitionUser, SafetyTeamMemberDto>();
                cfg.CreateMap<SafetyTeamMemberDto, IgnitionUser>( );
                cfg.CreateMap<RosterMember, EmergencyTeamMemberDto>();
            } );

            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
