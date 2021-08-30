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
    public class ReceiptProductsController : ControllerBase
    {
        public ReceiptProductsController()
        {
        }

        [HttpGet]
        public async Task<ReceiptProductModel[]> Get(Guid receiptId)
        {
            return new[]
            {
                new ReceiptProductModel{ },
                new ReceiptProductModel{ }
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid receiptId, ReceiptProductModel model)
        {
            return new StatusCodeResult((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid receiptId, ReceiptProductModel model)
        {
            return new StatusCodeResult((int)HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid receiptId, ReceiptProductModel model)
        {
            return new StatusCodeResult((int)HttpStatusCode.NoContent);
        }
    }
}
