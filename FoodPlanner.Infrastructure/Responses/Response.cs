using FoodPlanner.Core.Entities;

namespace FoodPlanner.Infrastructure.Responses
{
    public class Response<T> : StatusResponse,IResponse
        where T : EntityBase
    {
        public T Entity { get; set; }
    }
}