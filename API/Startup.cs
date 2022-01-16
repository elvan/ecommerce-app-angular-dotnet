using System.Linq;
using API.Errors;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.AddControllers();

            _ = services.AddScoped<IProductRepository, ProductRepository>();
            _ = services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            _ = services.AddAutoMapper(typeof(MappingProfiles));

            _ = services.Configure<ApiBehaviorOptions>(options =>
              {
                  options.InvalidModelStateResponseFactory = actionContext =>
                  {
                      string[] errors = actionContext.ModelState
                          .Where(e => e.Value.Errors.Count > 0)
                          .SelectMany(x => x.Value.Errors)
                          .Select(x => x.ErrorMessage)
                          .ToArray();

                      ApiValidationErrorResponse errorResponse = new ApiValidationErrorResponse
                      {
                          Errors = errors
                      };

                      return new BadRequestObjectResult(errorResponse);
                  };
              });

            _ = services.AddDbContext<StoreContext>(options =>
            {
                _ = options.UseSqlite(_config.GetConnectionString("DefaultConnection"));
            });

            _ = services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _ = app.UseMiddleware<ExceptionMiddleware>();

            if (env.IsDevelopment())
            {
                // _ = app.UseDeveloperExceptionPage();
                _ = app.UseSwagger();
                _ = app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            _ = app.UseStatusCodePagesWithReExecute("/errors/{0}");

            _ = app.UseHttpsRedirection();

            _ = app.UseRouting();

            _ = app.UseStaticFiles();

            _ = app.UseAuthorization();

            _ = app.UseEndpoints(endpoints =>
            {
                _ = endpoints.MapControllers();
            });
        }
    }
}
