using System;
using System.ComponentModel.DataAnnotations;

namespace FoodPlanner.Web.Models
{
    public class ProductModel
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}
