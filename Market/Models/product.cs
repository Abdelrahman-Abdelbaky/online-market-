using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Models
{
    public class product
    {
        public int Id { get; set; }
        public string name { get; set; }

        public int productCount { get; set; }

        public string description { get; set; }

        public int price { get; set; }  

        public DateTime insertTime { get; set; }

        public string imgSrc { get; set; }

        [NotMapped]
        public IFormFile img { get; set; }

        public int category { get; set; }

        public List<Comments> ProductComment { get; set; }

    }
}
