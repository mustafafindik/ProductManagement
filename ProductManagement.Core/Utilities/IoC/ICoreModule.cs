using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProductManagement.Core.Utilities.IoC
{
    public  interface ICoreModule
    {
        void Load(IServiceCollection services, IConfiguration configuration);
    }
}
