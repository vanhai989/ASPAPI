using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using CRUDApi.Data;
using CRUDApi.Respository;
using CRUDApi.Respository.Impl;
using CRUDApi.Services;
using CRUDApi.Services.Impl;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CRUDApi.Models.AuthModels;
using CRUDApi.Models.EmailModels;
using Microsoft.Extensions.FileProviders;
using System.IO;

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
            #region
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => builder.AllowAnyOrigin());
            });

            // config swagger
            services.AddSwaggerGen();

            // config appseting and get data from appSetting.json

            var appsetting = Configuration.GetSection("AppSetting");
            services.Configure<AppSetting>(appsetting);
            var setting = appsetting.Get<AppSetting>();

            // config sender email
            var emailConfig = Configuration.GetSection("EmailConfiguration");
            services.Configure<EmailConfiguration>(emailConfig);
            services.AddSingleton(emailConfig);

            var secrectKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.SecrectKey));

            services.AddDbContext<AuthDbContext>(opt => 
            opt.UseLazyLoadingProxies().UseSqlServer(setting.ConnectString), ServiceLifetime.Scoped);

            services.AddDbContext<ProductContext>(opt =>
            opt.UseSqlServer(setting.ConnectString), ServiceLifetime.Transient);

            services.AddDbContext<CustomerContext>(opt =>
            opt.UseSqlServer(setting.ConnectString), ServiceLifetime.Transient);

            services.AddDbContext<EmailContext>(opt =>
            opt.UseSqlServer(setting.ConnectString), ServiceLifetime.Transient);

            services.AddDbContext<ForgotPasswordContext>(opt =>
            opt.UseSqlServer(setting.ConnectString), ServiceLifetime.Transient);

            services.AddDbContext<ConfirmActionContext>(opt =>
            opt.UseSqlServer(setting.ConnectString), ServiceLifetime.Transient);

            services.AddDbContext<FormFileContext>(opt =>
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
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = secrectKey, 
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
            services.AddTransient<Services.IProductService, ProductServiceImpl>();
            services.AddTransient<Respository.IProductRespository, ProductRespositoryImpl>();
            services.AddTransient<ICustomerService, CustomerServiceImpl>();
            services.AddTransient<ICustomerRespository,CustomerRespositoryImpl>();
            services.AddScoped<IEmailSender, EmailSender>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            #region
            /*app.UseCors(options => options.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader());*/

            app.UseCors();
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Images")),
                RequestPath = "/Images"
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            #endregion

        }
    }
}
