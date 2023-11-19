using FluentValidation.AspNetCore;
using HomeApi.Confugurations;
using HomeApi.Contracts.Validators;
using HomeApi.Data;
using HomeApi.Data.Repos;
using HomeApi.MappingProfiles;
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
using System.Reflection;
using System.Threading.Tasks;

namespace HomeApi
{
    public class Startup
    {
        /// <summary>
        /// Загрузка конфигурации из файла Json
        /// </summary>
        private IConfiguration Configuration
        { get; } = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json")
          .AddJsonFile("appsettings.Development.json")
          .AddJsonFile("HomeOptions.json")
          .Build();


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Подключаем автомаппер
            var assembly = Assembly.GetAssembly(typeof(MappingProfile));
            services.AddAutoMapper(assembly);

            // Подключаем репозитории 
            services.AddSingleton<IDeviceRepository, DeviceRepository>();
            services.AddSingleton<IRoomRepository, RoomRepository>();

            // Подключаем контекст Базы данных
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<HomeApiContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);

            // Подключаем валидацию запросов
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddDeviceRequestValidator>());

            // Загружаем только адрес (вложенный Json-объект) 
            services.Configure<Address>(Configuration.GetSection("Address"));
            services.Configure<HomeOptions>(Configuration);
            services.Configure<HomeOptions>(opt =>
            {
                opt.Area = 228;
            });

            services.AddControllers();
            // поддерживает автоматическую генерацию документации WebApi с использованием Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HomeApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            // Сопоставляем маршруты с контроллерами
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
