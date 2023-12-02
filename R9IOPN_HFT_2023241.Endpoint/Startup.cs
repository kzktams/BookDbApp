using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using R9IOPN_HFT_2023241.Logic;
using R9IOPN_HFT_2023241.Models;
using R9IOPN_HFT_2023241.Repository;

namespace R9IOPN_HFT_2023241.Endpoint
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<BookDbContext>();
            services.AddTransient<IRepository<Book>, BookRepository>();
            services.AddTransient<IRepository<Author>, AuthorRepository>();
            services.AddTransient<IRepository<Loan>, LoanRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();

            services.AddTransient<IBookLogic, BookLogic>();
            services.AddTransient<IUserLogic, UserLogic>();
            services.AddTransient<IAuthorLogic, AuthorLogic>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
