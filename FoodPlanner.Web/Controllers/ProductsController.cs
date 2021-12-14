using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FoodPlanner.Infrastructure.RequestHandlers;
using FoodPlanner.Infrastructure.Requests;
using FoodPlanner.Infrastructure.Responses;
using FoodPlanner.Web.HttpResults;
using FoodPlanner.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodPlanner.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IRequestHandler<GetProductsRequest, GetProductsResponse> _getProductsRequestHandler;
        private readonly IRequestHandler<CreateProductRequest, StatusResponse> _createProductRequestHandler;
        private readonly IRequestHandler<UpdateProductRequest, StatusResponse> _updateProdutRequestHandler;
        private readonly IRequestHandler<DeleteProductRequest, StatusResponse> _deleteProductRequestHandler;

        public ProductsController(
            IRequestHandler<GetProductsRequest, GetProductsResponse> getProductsRequestHandler,
            IRequestHandler<CreateProductRequest, StatusResponse> createProductRequestHandler,
            IRequestHandler<UpdateProductRequest, StatusResponse> updateProdutRequestHandler,
            IRequestHandler<DeleteProductRequest, StatusResponse> deleteProductRequestHandler)
        {
            _getProductsRequestHandler = getProductsRequestHandler
                ?? throw new ArgumentNullException(nameof(getProductsRequestHandler));
            _createProductRequestHandler = createProductRequestHandler
                ?? throw new ArgumentNullException(nameof(createProductRequestHandler));
            _updateProdutRequestHandler = updateProdutRequestHandler
                ?? throw new ArgumentNullException(nameof(updateProdutRequestHandler));
            _deleteProductRequestHandler = deleteProductRequestHandler
                ?? throw new ArgumentNullException(nameof(deleteProductRequestHandler));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageRequestModel model)
        {
            if (model == null)
            {
                return new BadRequestResult();
            }

            if (model != null && model.Take == default(int) && string.IsNullOrWhiteSpace(model.ContinuationToken))
            {
                return new BadRequestObjectResult(
                    $"{model.Take} and {model.ContinuationToken} cannot be empty or default at the same time");
            }

            var request = new GetProductsRequest
            {
                Take = model.Take,
                ContinuationToken = model.ContinuationToken
            };

            var result = await _getProductsRequestHandler.HandleAsync(request);
            var responseBody = new PageResponseModel<ProductModel>
            {
                NextPageToken = result.NextPageToken,
                PreviousPageToken = result.PreviousPageToken,
                Items = result.Products.Select(x => new ProductModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
            };
            return new OkObjectResult(responseBody);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductModel model)
        {
            var request = new CreateProductRequest
            {
                Name = model.Name
            };

            var statusResponse = await _createProductRequestHandler.HandleAsync(request);
            if (statusResponse.ErrorCode != 0)
            {
                return new BadRequestObjectResult(statusResponse);
            }

            model.Id = statusResponse.Id;

            return new CreatedObjectResult(model);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductModel model)
        {
            var request = new UpdateProductRequest
            {
                Name = model.Name,
                Id = model.Id
            };

            var response = await _updateProdutRequestHandler.HandleAsync(request);

            return new StatusCodeResult((int)HttpStatusCode.NoContent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteProductRequest
            {
                Id = id
            };

            var response = await _deleteProductRequestHandler.HandleAsync(request);

            return new StatusCodeResult((int)HttpStatusCode.NoContent);
        }
    }
}
