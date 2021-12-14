using System;
using System.ComponentModel.DataAnnotations;

namespace FoodPlanner.Web.Models
{
    public class ReceiptProductModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public UnitEnum Unit { get; set; }
    }
}
