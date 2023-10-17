using System.ComponentModel.DataAnnotations.Schema;

namespace Yenetta_code.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string productName { get; set; }
        public string slug { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public int quantity { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public bool isDeleted { get; set; } = false;


        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }


    }
}
