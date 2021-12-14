using System;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Infrastructure.Constants;
using FoodPlanner.Infrastructure.DbContexts;
using FoodPlanner.Infrastructure.Requests;
using FoodPlanner.Infrastructure.Responses;

namespace FoodPlanner.Infrastructure.RequestHandlers
{
    public class DeleteProductRequestHandler : IRequestHandler<DeleteProductRequest, StatusResponse>
    {
        private readonly EFDbContext _dbContext;
        public DeleteProductRequestHandler(EFDbContext dbContext)
        {
            _dbContext = dbContext
                ?? throw new System.ArgumentNullException(nameof(dbContext));
        }

        public async Task<StatusResponse> HandleAsync(DeleteProductRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Id == default(int))
            {
                throw new ArgumentNullException(nameof(request.Id));
            }

            var entity = _dbContext.Products
                .FirstOrDefault(x => x.Id == request.Id);

            if (entity == null)
            {
                return new StatusResponse
                {
                    ErrorCode = ErrorCodes.EntityNotFound,
                    Id = request.Id
                };
            }

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Products.Remove(entity);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return new StatusResponse
                    {
                        Id = request.Id,
                        ErrorCode = default(int)
                    };
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}