using lightweight.business.Abstract;
using lightweight.business.Concrete;
using lightweight.core.Abstract;
using lightweight.core.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using lightweight.data.Context;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using System;
using Newtonsoft.Json;

namespace lightweight.webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // cors add
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", // I introduced a string constant just as a label "AllowAllOriginsPolicy"
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyHeader();

                    });

            });
            services.AddControllers(options => options.EnableEndpointRouting = false);
            services.AddHttpContextAccessor();
     

            services.AddDbContext<lwContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "lightweight Asp.net Core",
                    Description = "lightweight Asp.net Core Api Repository Design Pattern , Jwt ,Ef Core,Role Based Starter Project",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Berkan ARIKAN",
                        Email = "i@berkanarikan.com.tr",
                        Url = new Uri("https://github.com/BerkanARIKAN"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

            });

            var key = Encoding.ASCII.GetBytes("jwt secret key. change please ");
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddMvc().AddNewtonsoftJson(options =>
                           options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // global cors policy
            app.UseCors(MyAllowSpecificOrigins);
            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    //TODO: Either use the SwaggerGen generated Swagger contract (generated from C# classes)
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Swagger Test .Net Core");

                    //TODO: Or alternatively use the original Swagger contract that's included in the static files
                    // c.SwaggerEndpoint("/swagger-original.json", "Swagger Petstore Original");
                });

            app.UseAuthentication();
            //cors add
            app.UseRouting();
            app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}

