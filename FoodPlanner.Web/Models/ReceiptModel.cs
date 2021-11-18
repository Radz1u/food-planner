using System;
using System.ComponentModel.DataAnnotations;

namespace FoodPlanner.Web.Models
{
    public class ReceiptModel
    {
        [Required]
        public ReceiptProductModel[] Products { get; set; }
        
        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        public int Calories { get; set; }

        [Required]
        public string[] Steps { get; set; }
        public string Notes { get; set; }
    }
}
