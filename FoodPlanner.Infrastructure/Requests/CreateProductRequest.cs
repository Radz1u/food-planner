namespace FoodPlanner.Infrastructure.Requests
{
    public class CreateProductRequest : IRequest
    {
        public string Name { get; set; }
    }
}