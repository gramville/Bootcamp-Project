namespace Yenetta_code.Models.Services
{
    public interface IProductService
    {
        Task<int> Add(Product product);
        Task<int> Update(Product product);
        Task Delete(Product product);
        Task<Product> GetById(int id);
        Task<List<Product>> GetAll();
        Task<bool> ProductExists(int id);
        Task<bool> ProductNameExists(string productSlug);
    }
}
