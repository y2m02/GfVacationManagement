using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VacationManager.Mappings;
using VacationManager.Models.Entities;
using VacationManager.Repositories;
using VacationManager.Services;

namespace VacationManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<VacationManagerContext>(option => option.UseSqlServer("VacationManagerConnection"));
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new() { Title = "VacationManager", Version = "v1" }));

            var mappingConfig = new MapperConfiguration(
                mc => mc.AddProfile(new ProfileMapper())
            );

            services.AddSingleton(mappingConfig.CreateMapper());

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VacationManager v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
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
