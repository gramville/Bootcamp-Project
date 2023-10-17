using System.ComponentModel.DataAnnotations.Schema;

namespace Yenetta_code.Models
{
    public class Brand
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string brandName { get; set; }
        public string slug { get; set; }
        public bool isDeleted { get; set; } = false;

    }
}
