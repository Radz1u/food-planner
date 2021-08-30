using System;
namespace FoodPlanner.Web.Models
{
    public class ReceiptProductModel
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public UnitEnum Unit { get; set; }
    }
}
