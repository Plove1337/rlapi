using System.Text.Json.Serialization;

namespace rlapi.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detal { get; set; }
        public string Address { get; set; }
        public string ImgUrl { get; set; }
        public double Price { get; set; }
        public bool isTrending { get; set; }
        public int CategoryID { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        public int UserID { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
