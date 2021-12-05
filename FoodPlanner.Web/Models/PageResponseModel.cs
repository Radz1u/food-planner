using System.Collections.Generic;

namespace FoodPlanner.Web.Models
{
    public class PageResponseModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public string NextPageToken { get; set; }
        public string PreviousPageToken { get; set; }
    }
}