using System.ComponentModel.DataAnnotations;

namespace DotNetCoreRestAPI.Models
{
    public class Product
    {
        //validations

        [Required, StringLength(3, ErrorMessage = "Length should be 3 for ")]
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Length should be less than or equal to 20")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "it is requred")]
        public string Price { get; set; }
    }
}
