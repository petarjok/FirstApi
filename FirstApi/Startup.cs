﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mediator.Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StructureMap;
using Swashbuckle.AspNetCore.Swagger;

namespace FirstApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public System.IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //services
            //.AddAuthentication(options =>
            //{
            //	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //	options.TokenValidationParameters = new TokenValidationParameters()
            //	{
            //		ValidateActor = false,
            //		ValidateAudience = false,
            //		ValidateLifetime = true,
            //		ValidateIssuer = false,
            //		ValidateIssuerSigningKey = true,
            //		IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("HalzAHalzAHalzAHalzA"))
            //	};
            //});

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ////dodavame swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ML API", Version = "v1" });

            });

            var container = new Container(c =>
            {
                c.AddRegistry(new MediatorPipelineRegistry("FirstApi.Application"));
                c.For<IHttpContextAccessor>().Use<HttpContextAccessor>();
                c.For<IConfiguration>().Use(Configuration);

                c.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            //za swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cool ASP.NET Core Rest API");
            });

            app.UseMvc();
        }
    }
}
