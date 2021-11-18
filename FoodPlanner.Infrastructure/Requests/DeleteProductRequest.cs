namespace FoodPlanner.Infrastructure.Requests
{
    public class DeleteProductRequest : IRequest
    {
        public int Id { get; set; }
    }   
}