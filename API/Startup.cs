using API.Extensions;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            _ = services.AddAutoMapper(typeof(MappingProfiles));

            _ = services.AddDbContext<StoreContext>(options =>
            {
                _ = options.UseSqlite(_config.GetConnectionString("DefaultConnection"));
            });

            _ = services.AddApplicationServices();

            _ = services.AddSwaggerDocumentation();

            _ = services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    _ = policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("https://localhost:4200");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _ = app.UseMiddleware<ExceptionMiddleware>();

            if (env.IsDevelopment())
            {
                // _ = app.UseDeveloperExceptionPage();
                _ = app.UseSwaggerDocumentation();
            }

            _ = app.UseStatusCodePagesWithReExecute("/errors/{0}");

            _ = app.UseHttpsRedirection();

            _ = app.UseRouting();

            _ = app.UseStaticFiles();

            _ = app.UseCors("CorsPolicy");

            _ = app.UseAuthorization();

            _ = app.UseEndpoints(endpoints =>
            {
                _ = endpoints.MapControllers();
            });
        }
    }
}
