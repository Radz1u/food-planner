using System.Threading.Tasks;
using FoodPlanner.Infrastructure.Requests;
using FoodPlanner.Infrastructure.Responses;

namespace FoodPlanner.Infrastructure.RequestHandlers{
    public interface IRequestHandler<TRequest,TResponse>
     where TRequest:IRequest
     where TResponse:IResponse
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}