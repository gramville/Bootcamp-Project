﻿namespace Yenetta_code.Models.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDBContext _context;
        public CategoryService(AppDBContext appDBContext)
        {
            _context = appDBContext;
        }
        public async Task<int> Add(Category Category)
        {
            await _context.categories.AddAsync(Category);
            await _context.SaveChangesAsync();
            return Category.id;
        }
        public async Task<int> Update(Category Category)
        {
            await _context.SaveChangesAsync();
            return Category.id;
        }
        public async Task Delete(Category Category)
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Category> GetById(int id)
        {
            return await _context.categories.FirstOrDefaultAsync(temp => temp.id == id);
        }
        public async Task<List<Category>> GetAll()
        {
            return await _context.categories.ToListAsync();
        }
        public async Task<bool> CategoryExists(int id)
        {
            return await _context.categories.AnyAsync(temp => temp.id == id);
        }
        public async Task<bool> CategoryNameExists(string CategorySlug)
        {
            return await _context.categories.AnyAsync(temp => temp.slug == CategorySlug);
        }
    }
}