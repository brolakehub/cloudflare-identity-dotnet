using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BroLake.Cloudflare.Identity
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddCloudflareIdentity(this IServiceCollection services)
        {
            services.AddScoped<ICloudflareIdentity, CloudflareIdentity>();
            return services;
        }

        public static WebApplication UseCloudFlareMiddleware(this WebApplication builder)
        {
            builder.UseMiddleware<GetIdentityMiddleware>();
            return builder;
        }
    }
}
