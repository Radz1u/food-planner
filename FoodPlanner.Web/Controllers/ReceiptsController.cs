using System.Net;
using System.Threading.Tasks;
using FoodPlanner.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodPlanner.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceiptsController : ControllerBase
    {
        public ReceiptsController()
        {
        }

        [HttpGet]
        public async Task<ReceiptModel[]> Get()
        {
            return new[]
            {
                new ReceiptModel{ },
                new ReceiptModel{ }
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReceiptModel model)
        {
            return new StatusCodeResult((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ReceiptModel model)
        {
            return new StatusCodeResult((int)HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(ReceiptModel model)
        {
            return new StatusCodeResult((int)HttpStatusCode.NoContent);
        }
    }
}
