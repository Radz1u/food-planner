namespace FoodPlanner.Web.Models
{
    public class PageRequestModel
    {
        public int Take { get; set; }
        public string ContinuationToken { get; set; }
    }
}