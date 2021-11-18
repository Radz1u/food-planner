using System;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Core.Entities;
using FoodPlanner.Infrastructure.Constants;
using FoodPlanner.Infrastructure.DbContexts;
using FoodPlanner.Infrastructure.Requests;
using FoodPlanner.Infrastructure.Responses;

namespace FoodPlanner.Infrastructure.RequestHandlers
{
    public class UpdateProductRequestHandler :
    IRequestHandler<UpdateProductRequest, StatusResponse>
    {
        private readonly EFDbContext _dbContext;
        public UpdateProductRequestHandler(EFDbContext dbContext)
        {
            _dbContext = dbContext
                ?? throw new System.ArgumentNullException(nameof(dbContext));
        }

        public async Task<StatusResponse> HandleAsync(UpdateProductRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Id == default(int))
            {
                throw new ArgumentNullException(nameof(request.Id));
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentNullException(nameof(request.Name));
            }

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var entity = _dbContext.Products
                    .AsQueryable()
                    .FirstOrDefault(x => x.Id == request.Id);
                    var errorCode = 0;

                    if (entity != null)
                    {
                        if (!request.Name.Equals(entity.Name, StringComparison.InvariantCulture))
                        {
                            entity.Name = request.Name;
                            _dbContext.Update(entity);
                            await _dbContext.SaveChangesAsync();
                            await transaction.CommitAsync();
                        }
                    }
                    else
                    {
                        errorCode = ErrorCodes.EntityNotFound;
                    }

                    return new Response<ProductEntity>
                    {
                        Id = request.Id,
                        ErrorCode = errorCode
                    };
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}