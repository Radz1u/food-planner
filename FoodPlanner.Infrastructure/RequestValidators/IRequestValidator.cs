using FoodPlanner.Infrastructure.Requests;
namespace FoodPlanner.Infrastructure.RequestValidators
{
    public interface IRequestValidator<T> where T : IRequest
    {
        bool Validate(T request);
    }
}