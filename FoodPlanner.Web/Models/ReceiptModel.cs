using System;
namespace FoodPlanner.Web.Models
{
    public class ReceiptModel
    {
        public ReceiptProductModel[] Products { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public string[] Steps { get; set; }
        public string Notes { get; set; }
    }
}
