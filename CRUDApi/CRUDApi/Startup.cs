using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDApi.Models;
using CRUDApi.Data;
using System.Web.Http;
using System.Web.Mvc;
using Amazon.EC2.Model;
using CRUDApi.Respository;
using CRUDApi.Respository.Impl;
using CRUDApi.Services;
using CRUDApi.Services.Impl;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CRUDApi
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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => builder.AllowAnyOrigin());
            });
          
            // config appseting and get data from appSetting.json

            var appsetting = Configuration.GetSection("AppSetting");
            services.Configure<AppSetting>(appsetting);
            var setting = appsetting.Get<AppSetting>();
            var secrectKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.secrectKey));

            services.AddDbContext<AuthDbContext>(opt => opt.UseLazyLoadingProxies().UseSqlServer(setting.ConnectString), ServiceLifetime.Scoped);

            services.AddDbContext<ProductContext>(opt =>
                                                  opt.UseSqlServer(setting.ConnectString), ServiceLifetime.Transient);

            services.AddDbContext<CustomerContext>(opt =>
                                                    opt.UseSqlServer(setting.ConnectString), ServiceLifetime.Transient);

            // this block code to validate authentication incomming resquest
            services.AddAuthentication(
                t =>
                {
                    t.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    t.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }

                ).AddJwtBearer(t =>
                {
                    t.RequireHttpsMetadata = false;
                    t.SaveToken = false;
                    t.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = secrectKey, 
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero,
                        ValidateActor = false,
                    };
                });

           /* services.AddMvc();*/
            // config response returned from server is camel case
            services.AddControllers().AddNewtonsoftJson(t => t.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // serviceDependencyInjection
            services.AddTransient<IProductRespository, ProductRespositoryIml>();
            services.AddTransient<ICustomerRespository,CustomerRespositoryIml>();
            services.AddTransient<IProductService, ProductServiceImpl>();
            services.AddTransient<ICustomerService, CustomerServiceImpl>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
