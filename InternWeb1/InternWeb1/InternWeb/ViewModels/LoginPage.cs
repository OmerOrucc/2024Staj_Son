using System.ComponentModel.DataAnnotations;

namespace InternWeb.ViewModels
{
    public class LoginPage
    {
        [Required(ErrorMessage ="Username is required.")]
        public string? Username {  get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password is required.")]
        public string? Password { get; set; }
    }
}
