using Microsoft.AspNetCore.Mvc;

namespace Yenetta_code.Models.Services
{
    public class BrandService : IBrandService
    {
        private readonly AppDBContext _context;
        public BrandService(AppDBContext appDBContext)
        {
            _context = appDBContext;
        }
        public async Task<int> Add(Brand Brand)
        {
            await _context.brands.AddAsync(Brand);
            await _context.SaveChangesAsync();
            return Brand.id;
        }
        public async Task<int> Update(Brand Brand)
        {
            await _context.SaveChangesAsync();
            return Brand.id;
        }
        public async Task Delete(Brand Brand)
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Brand> GetById(int id)
        {
            return await _context.brands.FirstOrDefaultAsync(temp => temp.id == id);
        }
        public async Task<List<Brand>> GetAll()
        {
            return await _context.brands.Where(temp => !temp.isDeleted).ToListAsync();
        }
        public async Task<bool> BrandExists(int id)
        {
            return await _context.brands.AnyAsync(temp => temp.id == id);
        }
        public async Task<bool> BrandNameExists(string BrandSlug)
        {
            return await _context.brands.AnyAsync(temp => temp.slug == BrandSlug);
        }
        public async Task<List<string>> GetBrandNames()
        {
            return await _context.brands.Where(temp => !temp.isDeleted).Select(temp => temp.brandName).ToListAsync();
        }
        public async Task<int> GetIdByBrandName(string BrandName)
        {
            var brand = await _context.brands.FirstOrDefaultAsync(temp => temp.brandName == BrandName);
            Console.WriteLine("The brand id is : " + brand.id);
            return brand == null ? 0 : brand.id;
        }
        public async Task<List<Brand>> DeletedBrands()
        {
            return await _context.brands.Where(temp => temp.isDeleted).ToListAsync();
        }
        public async Task<int> RestoreDeletedBrand(Brand brand)
        {
            await _context.SaveChangesAsync();
            return brand.id;
        }
    }
}
