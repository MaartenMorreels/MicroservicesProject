using Assessment.BLL;
using Assessment.BLL.Mapper;
using Assessment.DAL.Context;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Assessment.API
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
            services.AddDbContext<AssessmentContext>(options => options.UseSqlServer("Server=localhost;Database=IRM.HRM.Assessment;Trusted_Connection=True;"));
            
            services.RegisterBusinessLogic(Configuration);
            services.AddMvc().AddControllersAsServices();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "My API", Version = "v1"}); });

            var config = new MapperConfiguration(cfg => {

                cfg.AddProfile<MapperProfile>();
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Questionary}/{action=Index}/{id?}");
                
            });
        }
    }
}
