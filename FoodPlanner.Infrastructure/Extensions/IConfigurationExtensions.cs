using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodPlanner.Infrastructure.Extensions
{
    public static class IConfigurationExtensions
    {
        public static IConfiguration Register<T>(this IConfiguration configuration,IServiceCollection services, string key)
            where T:new()
        {
            var @object = configuration.Bind<T>(key);

            services.AddSingleton(typeof(T), @object);

            return configuration;
        }

        public static T Bind<T>(this IConfiguration configuration, string key)
           where T : new()
        {
            var @object = new T();

            configuration.Bind(key, @object);

            return @object;
        }
    }
}
