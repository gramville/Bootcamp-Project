namespace Yenetta_code.Models.DTOs.UpdateDTOs
{
    public class UpdateProductDTO
    {
        public int id { get; set; }
        public string? productName { get; set; }
        public string? description { get; set; }
        public float? price { get; set; }
        public int? quantity { get; set; }
        public string? categoryName { get; set; }
        public string? brandName { get; set; }
        public int? brandId { get; set; }
        public int? categoryId { get; set; }
    }
}
