using Microsoft.Extensions.DependencyInjection;
using SanStore.Application.Common;
using SanStore.Application.Services;
using SanStore.Application.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SanStore.Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBrandService,BrandService>();
            return services;
        }
    }
}
