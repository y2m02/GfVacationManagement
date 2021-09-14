using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using VacationManagerApi.Models.Entities;
using VacationManagerApi.Repositories;
using VacationManagerApi.Services;

namespace VacationManagerApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<VacationManagerContext>(
                option => option.UseSqlServer(
                    Configuration.GetConnectionString("VacationManagerConnection")
                )
            );

            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<VacationManagerContext>()
                .AddDefaultTokenProviders();

            services
                .AddAuthentication(
                    options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                )
                .AddJwtBearer(
                    option =>
                    {
                        option.SaveToken = true;

                        option.TokenValidationParameters = new()
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidAudience = Configuration["JWT:ValidAudience"],
                            ValidIssuer = Configuration["JWT:ValidIssuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(Configuration["JWT:Secret"])
                            ),
                        };

                        option.RequireHttpsMetadata = true;
                    }
                );

            services.AddControllers();
            services.AddRouting(r => r.LowercaseUrls = true);
            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new() { Title = "VacationManager", Version = "v1" });
                }
            );

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "VacationManager v1");
                    }
                );
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private static void RegisterServices(IServiceCollection services)
        {
            var names = new List<string> { "Service", "Repository" };

            services.Scan(
                scan =>
                    scan.FromAssemblies(
                            typeof(HolidayService).Assembly,
                            typeof(HolidayRepository).Assembly
                        )
                        .AddClasses(x => x.Where(c => names.Any(name => c.Name.EndsWith(name))))
                        .AsMatchingInterface()
            );
        }
    }
}
