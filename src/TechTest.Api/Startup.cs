using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Tappau.DateTimeProvider.Abstractions;
using TechTest.Core;
using TechTest.Core.Entities;
using TechTest.DataLayer;
using TechTest.Infrastructure;

namespace TechTest.Api
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechTest.Api", Version = "v1" });
            });
            var dbName = Configuration.GetValue<string>("SqliteDbName");
            //var connectionString =
            //    $"Data Source={new FileInfo(Assembly.GetExecutingAssembly().Location).Directory?.FullName}\\{dbName}";
            var connectionString = "Filename=:memory:";
            //services.AddDatabaseContext(connectionString);
            services.AddDbContext<LibraryDataContext>(options =>
            {
                options.UseInMemoryDatabase("Library");
            });

            services.AddRepositories();

            services.AddCoreServices();

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            });
        }

        private static DbConnection AsInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            return connection;
        }

        private void LoadStandingData(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var services = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
            {
                var context = services?.ServiceProvider.GetRequiredService<LibraryDataContext>();
                if (env.IsDevelopment())
                {
                    //context?.Database.EnsureDeleted();
                }

                context?.Database.EnsureCreated();

                if (env.IsDevelopment())
                {
                    var authors = new List<Author>
                    {
                        {
                            new Author()
                            {
                                Name = "Bob Sinclair", Books = new List<Book>()
                                {
                                    { new Book() { Title = "The greatest Hits" } }
                                  , { new Book() { Title = "The Second we heard the beat" } }
                                }
                            }
                        },
                        {
                            new Author
                            {
                                Name = "Stephen King",
                                Books = new List<Book>()
                                {
                                    {new Book(){Title = "IT"}},
                                    {new Book(){Title = "The Shining"}},
                                    {new Book(){Title = "Carrie"}},
                                    
                                }
                            }
                        }
                    };

                    context?.Author.AddRange(authors);
                    context?.SaveChanges();
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            LoadStandingData(app, env);

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechTest.Api v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
