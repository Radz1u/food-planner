using FoodPlanner.Core.Entities;

namespace FoodPlanner.Infrastructure.Responses
{
    public class GetProductsResponse : IResponse
    {
        public ProductEntity[] Products { get; set; }
        public string PreviousPageToken { get; set; }
        public string NextPageToken { get; set; }
    }
}