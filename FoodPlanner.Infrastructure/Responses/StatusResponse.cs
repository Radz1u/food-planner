namespace FoodPlanner.Infrastructure.Responses
{
    public class StatusResponse : IResponse
    {
        public int Id { get; set; }
        public int ErrorCode { get; set; }
    }
}