namespace Yenetta_code.Models.DTOs.ResponseDTOs
{
    public class ProductResponseDTO
    {
        public int id { get; set; }
        public string productName { get; set; }
        public string slug { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public int quantity { get; set; }
        public string brandName { get; set; }
        public string categoryName { get; set; }
    }
}
