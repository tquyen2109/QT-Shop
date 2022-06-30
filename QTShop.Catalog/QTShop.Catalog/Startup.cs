using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using QTShop.Catalog.Model;
using QTShop.Catalog.Repositories;
using QTShop.Catalog.ServiceWorker;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace QTShop.Catalog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IProductsRepository, ProductsRepository>();
            services.AddSingleton<IOutboxRepository, OutboxRepository>();
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // Add our job
            services.AddSingleton<OutboxJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(OutboxJob),
                cronExpression: "0/5 * * * * ?"));
            services.AddHostedService<QuartzHostedService>();
            services.Configure<ProductCollectionDatabaseSettings>(
                Configuration.GetSection(nameof(ProductCollectionDatabaseSettings)));

            services.AddSingleton<IProductCollectionDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ProductCollectionDatabaseSettings>>().Value);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "QTShop.Catalog.API", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QTShop.Catalog.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}