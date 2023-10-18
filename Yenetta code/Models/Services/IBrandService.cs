namespace Yenetta_code.Models.Services
{
    public interface IBrandService
    {
        Task<int> Add(Brand Brand);
        Task<int> Update(Brand Brand);
        Task Delete(Brand Brand);
        Task<Brand> GetById(int id);
        Task<List<Brand>> GetAll();
        Task<bool> BrandExists(int id);
        Task<bool> BrandNameExists(string BrandSlug);
        Task<List<string>> GetBrandNames();
        Task<int> GetIdByBrandName(string BrandName);
        Task<List<Brand>> DeletedBrands();
        Task<int> RestoreDeletedBrand(Brand brand);
    }
}
