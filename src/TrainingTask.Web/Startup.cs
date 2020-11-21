using System.Reflection;

using Autofac;

using AutoMapper;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using NLog.Extensions.Logging;
using TrainingTask.Data;
using TrainingTask.Data.EF;
using TrainingTask.Data.EF.Model;
using TrainingTask.Data.EF.Model.EmployeeManagement;
using TrainingTask.Web.Infrastructure;

using CoreDependencyRegistrationModule = TrainingTask.Core.DependencyRegistrationModule;
using DataDependencyRegistrationModule = TrainingTask.Data.DependencyRegistrationModule;
using EmployeeMapperProfile = TrainingTask.Core.Mapper.EmployeeMapperProfile;

namespace TrainingTask.Web
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
            var dbConfiguration = new DataBaseConfig();
            Configuration.GetSection("Database").Bind(dbConfiguration);
            services.AddSingleton(dbConfiguration);

            services.AddAutoMapper(typeof(EmployeeConfiguration).GetTypeInfo().Assembly,
                typeof(EmployeeMapperProfile).GetTypeInfo().Assembly,
                Assembly.GetExecutingAssembly());

            services.AddDbContext<TrainingTaskDbContext>(option =>
                option.UseSqlServer(dbConfiguration.ConnectionString));

            services.AddLogging(builder =>
            {
                builder.AddNLog();
            });

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"}); });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { 
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseHsts();
          //  app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyModules(
                typeof(CoreDependencyRegistrationModule).GetTypeInfo().Assembly,
                typeof(DataDependencyRegistrationModule).GetTypeInfo().Assembly);
        }
    }
}