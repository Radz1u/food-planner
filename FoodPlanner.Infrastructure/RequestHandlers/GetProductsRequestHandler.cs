using System.Threading.Tasks;
using FoodPlanner.Infrastructure.DbContexts;
using FoodPlanner.Infrastructure.Requests;
using FoodPlanner.Infrastructure.Responses;
using Microsoft.EntityFrameworkCore;

namespace FoodPlanner.Infrastructure.RequestHandlers
{
    public class GetProductsRequestHandler : IRequestHandler<GetProductsRequest, GetProductsResponse>
    {
        private readonly EFDbContext _dbContext;
        public GetProductsRequestHandler(EFDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        }

        public async Task<GetProductsResponse> HandleAsync(GetProductsRequest request)
        {
            var products = await _dbContext.Products.ToArrayAsync();
            return new GetProductsResponse()
            {
                Products = products
            };
        }
    }
}