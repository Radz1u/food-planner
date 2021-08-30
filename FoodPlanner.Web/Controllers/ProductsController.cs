using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FoodPlanner.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodPlanner.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        public ProductsController()
        {
        }

        [HttpGet]
        public async Task<ProductModel[]> Get()
        {
            return new[]
            {
                new ProductModel{ Id = Guid.NewGuid(), Name="Papryka"},
                new ProductModel{ Id = Guid.NewGuid(), Name="Ryż"}
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductModel model)
        {
            return new StatusCodeResult((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductModel model)
        {
            return new StatusCodeResult((int)HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(ProductModel model)
        {
            return new StatusCodeResult((int)HttpStatusCode.NoContent);
        }
    }
}
