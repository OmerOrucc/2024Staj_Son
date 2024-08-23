using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Devine.Models.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }

        [Required]
        public string? Title { get; set; }  // Nullable olarak işaretlendi

        //[Required]
        //public string? Name { get; set; }  // Nullable olarak işaretlendi

        [Required]
        [Display(Name = "List Price")]
        [Range(1, 1000000000)]
        public int ListPrice { get; set; }

        [Required]
        public string? Seller { get; set; }  // Nullable olarak işaretlendi

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
    }
}
