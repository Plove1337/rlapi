using System.ComponentModel.DataAnnotations;

namespace rlapi.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Category name can't be null or empty")]
        public string Name { get; set;}
        [Required(ErrorMessage = "Image name can't be null or empty")]
        public string ImgUrl { get; set; }

        public ICollection<Property> Properties { get; set; }
    }
}
