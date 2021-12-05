namespace FoodPlanner.Infrastructure.Requests
{
    public class GetProductsRequest : IRequest
    {
        public string ContinuationToken { get; set; }
        public int Take { get; set; }
    }
}