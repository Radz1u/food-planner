namespace FoodPlanner.Infrastructure.Builders {
    using Azure.Identity;
    using FoodPlanner.Infrastructure.Configurations;
    using Microsoft.Azure.Cosmos;

    public class CosmosClientBuilder {
        private readonly CosmosConfiguration configuration;

        public CosmosClientBuilder (CosmosConfiguration configuration) {
            this.configuration = configuration;
        }

        public CosmosClient Build () {
            var credential = new DefaultAzureCredential ();
            return new CosmosClient (configuration.Uri, credential);
        }
    }
}