namespace FoodPlanner.Infrastructure.Requests
{
    public class GetProductsRequest : IRequest
    {
        public int Count { get; set; }
    }
}