using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using TestDbContext.Api.Infra.Executor;
using TestDbContext.Api.Infra.Filler;
using TestDbContext.Api.Infra.Retriever;
using TestDbContext.Api.Schedule;
using TestDbContext.Api.Service;
using TestDbContext.Infra.Db;


namespace TestDbContext
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
            services.AddControllers();
            var connectionString = GetConnectionString();
            services.AddScoped<ITestDataModelRetriever, TestDataModelRetriever>();
            services.AddScoped<IExecutor, TestDataModelExecutor>();
            services.AddScoped<IFiller, TestDataModelFiller>();
            services.AddScoped<IExecutorService, MyExecutorService>();
            services.AddHostedService<ExecutorJob>();
            services.AddDbContext<MyDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
        }

        private string GetConnectionString()
        {
            var connectionBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = "db",
                Database = "test",
                Username = "postgres",
                Password = "postgres",
                SearchPath = "public",
                Port = 5432,
            };

            return connectionBuilder.ToString();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
