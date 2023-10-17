using System.ComponentModel.DataAnnotations.Schema;

namespace Yenetta_code.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string categoryName { get; set; }
        public string slug { get; set; }
        public bool isDeleted { get; set; } = false;
    }
}
