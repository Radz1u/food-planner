using System;
using FoodPlanner.Infrastructure.Builders;
using FoodPlanner.Infrastructure.Configurations;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodPlanner.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterCosmosDb(this IServiceCollection services, IConfiguration configuration, string configurationKey)
        {
            var cosmosConfiguration = configuration.Bind<CosmosConfiguration>(configurationKey);
            var cosmosClientBuilder = new CosmosClientBuilder(cosmosConfiguration);

            services.AddSingleton(typeof(CosmosClient), cosmosClientBuilder.Build());

            return services;
        }
    }
}
