using System;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Core.Entities;
using FoodPlanner.Infrastructure.DbContexts;
using FoodPlanner.Infrastructure.Requests;
using FoodPlanner.Infrastructure.Responses;

namespace FoodPlanner.Infrastructure.RequestHandlers
{
    public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, StatusResponse>
    {
        private readonly EFDbContext _dbContext;
        public CreateProductRequestHandler(EFDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        }

        public async Task<StatusResponse> HandleAsync(CreateProductRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentNullException(nameof(request.Name));
            }
             
            var entityId = default(int);
            var existingEntity = _dbContext
                .Products
                .AsQueryable()
                .FirstOrDefault(x => x.Name.ToLower()==request.Name.ToLower());

            if (existingEntity != null)
            {
                entityId = existingEntity.Id;
            }
            else
            {

                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var entityEntry = await _dbContext.Products.AddAsync(
                            new ProductEntity
                            {
                                Name = request.Name
                            });

                        await _dbContext.SaveChangesAsync();
                        entityId = entityEntry.Entity.Id;
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }

            return new StatusResponse
            {
                Id = entityId
            };
        }
    }
}