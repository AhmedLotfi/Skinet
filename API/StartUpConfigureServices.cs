using System.Linq;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public static class StartUpConfigureServices
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

            services.AddDbContext<StoreContext>(x => x.UseSqlite(config.GetConnectionString("DefaultConnection")));

            services.Configure<ApiBehaviorOptions>(options =>
                        {
                            options.InvalidModelStateResponseFactory = actionContext =>
                            {
                                var errors = actionContext.ModelState
                                .Where(z => z.Value.Errors.Count > 0)
                                .SelectMany(z => z.Value.Errors)
                                .Select(z => z.ErrorMessage).ToArray();

                                var errorResponse = new ApiValidationErrorResponse
                                {
                                    Errors = errors
                                };

                                return new BadRequestObjectResult(errorResponse);
                            };
                        });
        }
    }
}