using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Devine.Models.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; } = string.Empty;  // Başlangıç değeri verildi

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
        public int DisplayOrder { get; set; }

        // Constructor ekleyerek 'Name' özelliğini başlatabilirsiniz
        public Category()
        {
            Name = string.Empty;  // Default value
        }
    }
}
