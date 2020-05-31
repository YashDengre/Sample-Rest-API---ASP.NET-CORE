using DotNetCoreRestAPI.Controllers;
using DotNetCoreRestAPI.Data;
using DotNetCoreRestAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCoreRestAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc().AddXmlSerializerFormatters();

            //if we remove this line then error will occure because the object will not be created at the tim of calling
            services.AddScoped<ICustomer, CustomerRepository>(); //responsible for dependencies and object creattion
            //this will lead us -  dependency injection by constructor


            //services.AddApiVersioning();
            //services.AddApiVersioning(x => x.ApiVersionReader = new MediaTypeApiVersionReader());
            //commented the above line for below code - more configuration for api versioning
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
                //below line will allows us to use custom hearder instead of accept header
                x.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
                //x.ApiVersionReader = new QueryStringOrHeaderApiVersionReader("x-api-version");
                //to define the versioning here isntead of attribute - see below line
                x.Conventions.Controller<ProductController>().HasApiVersion(new ApiVersion(1, 0));
            });
            services.AddDbContext<CustomerDBContext>(option => option.UseSqlServer(@"Data Source=HP-PC\SQLEXPRESS;Initial Catalog=RestCoreSample;Integrated Security=True"));
            //modified this for azure deployment
            //services.AddDbContext<CustomerDBContext>(option => option.UseSqlServer(Configuration.GetConnectionString("CustomerDBContext")));
            //user for NSwag
            //services.AddSwaggerDocument();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Rest Net Core", Version = "v1", Description = "This is the test app for DotNet Core API" }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CustomerDBContext customer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //use for Nswag
            //app.UseOpenApi();
            //app.UseSwaggerUi3();

            //swashbuckle
            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("v1/swagger.json", "RestCoreAPI"));

            app.UseMvc();
            customer.Database.EnsureCreated();

        }
    }
}
