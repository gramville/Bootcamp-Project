namespace Yenetta_code.Models.Services
{
    public interface ICategoryService
    {
        Task<int> Add(Category Category);
        Task<int> Update(Category Category);
        Task Delete(Category Category);
        Task<Category> GetById(int id);
        Task<List<Category>> GetAll();
        Task<bool> CategoryExists(int id);
        Task<bool> CategoryNameExists(string CategorySlug);
        Task<List<string>> GetCategoryNames();
        Task<int> GetIdByCategoryName(string CategoryName);
        Task<List<Category>> DeletedCaegories();
    }
}
