using Yenetta_code.Models.DTOs.ResponseDTOs;

namespace Yenetta_code.Models.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDBContext _context;
        public ProductService(AppDBContext appDBContext)
        {
            _context = appDBContext;
        }
        public async Task<int> Add(Product product)
        {
            await _context.products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.id;
        }
        public async Task<int> Update(Product product)
        {
            await _context.SaveChangesAsync();
            return product.id;
        }
        public async Task Delete(Product product)
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Product> GetById(int id)
        {
            return await _context.products.FirstOrDefaultAsync(temp => temp.id == id);
        }
        public async Task<List<ProductResponseDTO>> GetAll()
        {
            var products = await _context.products
                .Where(temp => temp.quantity > 0 && !temp.isDeleted)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .OrderBy(temp => temp.quantity)
                .ToListAsync();

            var productResponse = products.Select(p => new ProductResponseDTO
            {
                id = p.id,
                productName = p.productName,
                description = p.description,
                price = p.price,
                quantity = p.quantity,
                brandName = p.Brand?.brandName,
                categoryName = p.Category?.categoryName
            }).ToList();

            return productResponse;
        }
        public async Task<bool> ProductExists(int id)
        {
            return await _context.products.AnyAsync(temp => temp.id == id);
        }
        public async Task<bool> ProductNameExists(string productSlug)
        {
            return await _context.products.AnyAsync(temp => temp.slug == productSlug);
        }
        public async Task<List<ProductResponseDTO>> DeletedProducts()
        {
            var products = await _context.products
                .Where(temp => temp.isDeleted)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .OrderBy(temp => temp.quantity)
                .ToListAsync();

            var productResponse = products.Select(p => new ProductResponseDTO
            {
                id = p.id,
                productName = p.productName,
                description = p.description,
                price = p.price,
                quantity = p.quantity,
                brandName = p.Brand?.brandName,
                categoryName = p.Category?.categoryName
            }).ToList();

            return productResponse;
        }

    }
}
