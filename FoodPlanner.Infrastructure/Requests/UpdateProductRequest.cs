namespace FoodPlanner.Infrastructure.Requests
{
    public class UpdateProductRequest : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}