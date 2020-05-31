using System.ComponentModel.DataAnnotations;

namespace DotNetCoreRestAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required, StringLength(15)] public string Name { get; set; }
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email in not valid")]
        public string Email { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }
    }
}
