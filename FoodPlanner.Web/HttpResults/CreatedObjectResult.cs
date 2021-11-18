using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace FoodPlanner.Web.HttpResults
{
    public class CreatedObjectResult : ObjectResult
    {
        public CreatedObjectResult(object @object) : base(@object)
        {
            StatusCode = (int)HttpStatusCode.Created;
        }
    }
}