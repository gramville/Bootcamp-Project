using Microsoft.AspNetCore.Mvc;
using Yenetta_code.Models.Services;
using Yenetta_code.Models;
using Yenetta_code.Configurations;

namespace Yenetta_code.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, IBrandService brandService, ICategoryService categoryService)
        {
            _productService = productService;
            _brandService = brandService;
            _categoryService = categoryService;

        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAll();
            return View(products);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.brands = await _brandService.GetBrandNames();
            ViewBag.categories = await _categoryService.GetCategoryNames();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var slug = Slug.CreateSlug(product.productName);
            //int brandId = _brandService.GetIdByBrandName(product.b);
            int categoryId = 0;

            if(await _productService.ProductNameExists(slug))
            {
                ModelState.AddModelError("productName", "The product name already exists");
                return View(product);
            }
            if(product.price <= 0)
            {
                ModelState.AddModelError("price", "Price must be greater than zero.");
                return View(product);
            }
            if (product.quantity <= 0)
            {
                ModelState.AddModelError("quantity", "quantity must be greater than zero.");
                return View(product);
            }
            return View();

        }
    }
}
