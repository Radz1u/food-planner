using System.Collections.Generic;

namespace FoodPlanner.Core.Entities
{
    public class ReceiptEntity : EntityBase
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public ICollection<ReceiptItemEntity> Items { get; set; }
    }
}