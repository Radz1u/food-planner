namespace FoodPlanner.Core.Entities
{
    public class ReceiptItemEntity : EntityBase
    {
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }

        public int Weight { get; set; }
    }
}
