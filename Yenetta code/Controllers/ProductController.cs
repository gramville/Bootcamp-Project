using Microsoft.AspNetCore.Mvc;
using Yenetta_code.Models.Services;
using Yenetta_code.Models;
using Yenetta_code.Configurations;
using Yenetta_code.Models.DTOs.AddDTOs;
using AutoMapper;
using Yenetta_code.Models.DTOs.UpdateDTOs;

namespace Yenetta_code.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IBrandService brandService, ICategoryService categoryService,IMapper mapper)
        {
            _productService = productService;
            _brandService = brandService;
            _categoryService = categoryService;
            _mapper = mapper;

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
        public async Task<IActionResult> Create(AddProductDTO product)
        {
            var slug = Slug.CreateSlug(product.productName);
            int brandId = await _brandService.GetIdByBrandName(product.brandName);
            Console.WriteLine("The brand id is: " + brandId);
            if(brandId == 0)
            {
                ModelState.AddModelError("brandName", "Invalid brand name");
                return View(product);
            }
            int categoryId = await _categoryService.GetIdByCategoryName(product.categoryName);
            if (categoryId == 0)
            {
                ModelState.AddModelError("categoryName", "Invalid brand name");
                return View(product);
            }

            if (await _productService.ProductNameExists(slug))
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
            var PRODUCT = _mapper.Map<Product>(product);
            PRODUCT.BrandId = brandId;
            PRODUCT.CategoryId = categoryId;
            PRODUCT.slug = slug;
            var newId = await _productService.Add(PRODUCT);
            if (newId != 0)
            {
                TempData["AddedProduct"] = "Product added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong on our end. Try again in a few minutes.");
                return View(product);
            }

        }
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.brands = await _brandService.GetBrandNames();
            ViewBag.categories = await _categoryService.GetCategoryNames();
            var product = await _productService.GetById(id);
            if(product == null)
            {
                return RedirectToAction("Index");
            }
            var PRODUCT = _mapper.Map<UpdateProductDTO>(product);
            return View(PRODUCT);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductDTO product)
        {
            var updatedProduct = await _productService.GetById(product.id);
            if(updatedProduct == null)
            {
                ModelState.AddModelError("id", "Invalid id");
                return View(product);
            }
            int brandId = 0;
            int categoryId = 0;
            string slug = "";
            if (!string.IsNullOrEmpty(product.brandName))
            {
                brandId = await _brandService.GetIdByBrandName(product.brandName);
                if (brandId == 0)
                {
                    ModelState.AddModelError("brandName", "Invalid brand name");
                }
            }
            if(!string.IsNullOrEmpty(product.categoryName))
            {
                categoryId = await _categoryService.GetIdByCategoryName(product.categoryName);
                if (categoryId == 0)
                {
                    ModelState.AddModelError("categoryName", "Invalid category name");
                }
            }
            
            if(!string.IsNullOrEmpty(product.productName))
            {
                slug = Slug.CreateSlug(product.productName);
                if (await _productService.ProductNameExists(slug) && updatedProduct.slug != slug)
                {
                    ModelState.AddModelError("productName", "The product name already exists");
                    return View(product);
                }
            }
            
            if (product.price <= 0)
            {
                ModelState.AddModelError("price", "Price must be greater than zero.");
                return View(product);
            }
            if (product.quantity <= 0)
            {
                ModelState.AddModelError("quantity", "quantity must be greater than zero.");
                return View(product);
            }
            var PRODUCT = _mapper.Map<UpdateProductDTO, Product>(product, updatedProduct);
            PRODUCT.BrandId = (brandId != 0) ? brandId : PRODUCT.BrandId;
            PRODUCT.CategoryId = (categoryId != 0) ? categoryId : PRODUCT.BrandId;
            PRODUCT.slug = (!string.IsNullOrEmpty(product.productName)) ? slug : PRODUCT.slug;
            var newId = await _productService.Update(PRODUCT);
            if (newId != 0)
            {
                TempData["UpdatedProduct"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong on our end. Try again in a few minutes.");
                return View(product);
            }


        }
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            var PRODUCT = _mapper.Map<UpdateProductDTO>(product);
            return View(PRODUCT);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, int? ID)
        {
            var PRODUCT = await _productService.GetById(id);
            if (PRODUCT == null)
            {
                return RedirectToAction("Index");
            }
            PRODUCT.isDeleted = true;
            await _productService.Delete(PRODUCT);
            TempData["DeletedProduct"] = "Product deleted successfully";
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> DeletedProducts()
        {
            var deletedProducts = await _productService.DeletedProducts();
            return View(deletedProducts);
        }

    }
}
