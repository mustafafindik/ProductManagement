using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Core.CrossCuttingConcerns.Caching;
using ProductManagement.Core.CrossCuttingConcerns.Caching.Microsoft;
using ProductManagement.Core.Utilities.IoC;

namespace ProductManagement.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, MemoryCacheService>();
            services.AddSingleton<Stopwatch>();

        }
    }
}
