using System;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Core.Entities;
using FoodPlanner.Infrastructure.DbContexts;
using FoodPlanner.Infrastructure.Requests;
using FoodPlanner.Infrastructure.Responses;
using FoodPlanner.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace FoodPlanner.Infrastructure.RequestHandlers
{
    public class GetProductsRequestHandler : IRequestHandler<GetProductsRequest, GetProductsResponse>
    {
        private readonly EFDbContext _dbContext;
        private readonly IContinuationTokenEncoding _continuationTokenEncoding;

        public GetProductsRequestHandler(
            EFDbContext dbContext
            , IContinuationTokenEncoding continuationTokenEncoding)
        {
            _dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
            _continuationTokenEncoding = continuationTokenEncoding ?? throw new ArgumentNullException(nameof(continuationTokenEncoding));
        }

        public async Task<GetProductsResponse> HandleAsync(GetProductsRequest request)
        {
            Validate(request);

            var response = new GetProductsResponse();

            return string.IsNullOrWhiteSpace(request.ContinuationToken)
                ? await GetAsync(request.Take)
                : await GetAsync(request.ContinuationToken);
        }

        private void Validate(GetProductsRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Take <= 0 && string.IsNullOrWhiteSpace(request.ContinuationToken))
            {
                throw new ArgumentException("ContinuationToken or Take must have a value.");
            }
        }

        private async Task<GetProductsResponse> GetAsync(string token)
        {
            var decodedToken = _continuationTokenEncoding.Decode(token);
            Validate(decodedToken);

            var response = new GetProductsResponse();

            response.Products = await _dbContext.Products
                .AsQueryable()
                .Skip(decodedToken.Skip)
                .Take(decodedToken.Take)
                .ToArrayAsync();

            if (await HasNextPage(decodedToken.Take, decodedToken.Skip + decodedToken.Take))
            {
                response.NextPageToken = GenerateContinuationToken(
                        decodedToken.Skip + decodedToken.Take,
                        decodedToken.Take
                    );
            }
            int previousPageSkip = decodedToken.Skip - decodedToken.Take;
            if (previousPageSkip < 0)
            {
                response.PreviousPageToken =null;
            }
            else
            {
                response.PreviousPageToken = GenerateContinuationToken(previousPageSkip, decodedToken.Take);
            }

            return response;
        }

        private void Validate(ContinuationToken token)
        {
            if (token.Take <= 0)
            {
                throw new ArgumentException(
                    $"{nameof(GetProductsRequest.ContinuationToken)}.{nameof(token.Take)} cannot less or equal to 0");
            }

            if (token.Skip < 0)
            {
                throw new ArgumentException(
                    $"{nameof(GetProductsRequest.ContinuationToken)}.{nameof(token.Skip)} cannot less than 0");
            }
        }

        private async Task<GetProductsResponse> GetAsync(int take)
        {
            var response = new GetProductsResponse();
            response.Products = await _dbContext.Products
                .AsQueryable()
                .Take(take)
                .ToArrayAsync();

            if (await HasNextPage(take, take))
            {
                response.NextPageToken = _continuationTokenEncoding.Encode(new ContinuationToken
                {
                    Take = take,
                    Skip = take
                });
            }

            return response;
        }

        private async Task<bool> HasNextPage(int take, int skip = 0)
        {
            return await _dbContext.Products
            .AsQueryable()
            .Skip(skip)
            .Take(take)
            .AnyAsync();
        }


        private string GenerateContinuationToken(int skip, int take)
        {
            var token = new ContinuationToken
            {
                Take = take,
                Skip = skip
            };

            return _continuationTokenEncoding.Encode(token);
        }
    }
}