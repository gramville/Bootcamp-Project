using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Yenetta_code.Configurations;
using Yenetta_code.Models.DTOs.AddDTOs;
using Yenetta_code.Models.DTOs.UpdateDTOs;
using Yenetta_code.Models.Services;
using Yenetta_code.Models;

namespace Yenetta_code.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _CategoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService CategoryService, IMapper mapper)
        {
            _CategoryService = CategoryService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var Categorys = await _CategoryService.GetAll();
            return View(Categorys);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddCategoryDTO category)
        {
            var slug = Slug.CreateSlug(category.categoryName);
            if (await _CategoryService.CategoryNameExists(slug))
            {
                ModelState.AddModelError("categoryName", "This Category name already exists");
                return View(category);
            }
            var Category = _mapper.Map<Category>(category);
            Category.slug = slug;
            var newId = await _CategoryService.Add(Category);
            if (newId != 0)
            {
                TempData["AddedCategory"] = "Category added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong on our end. Try again in a few minutes.");
                return View(Category);
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            var category = await _CategoryService.GetById(id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var CATEGORY = _mapper.Map<UpdateCategoryDTO>(category);
            return View(CATEGORY);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryDTO category)
        {
            var updatedCategory = await _CategoryService.GetById(category.id);
            if (updatedCategory == null)
            {
                return RedirectToAction("Index");
            }
            string slug = "";
            if (!string.IsNullOrEmpty(category.categoryName))
            {
                slug = Slug.CreateSlug(category.categoryName);
                if (await _CategoryService.CategoryNameExists(slug) && updatedCategory.slug != slug)
                {
                    ModelState.AddModelError("categoryName", "the Category name already exists");
                    return View(category);
                }

            }
            var CATEGORY = _mapper.Map<UpdateCategoryDTO, Category>(category, updatedCategory);
            CATEGORY.slug = (!string.IsNullOrEmpty(category.categoryName)) ? slug : updatedCategory.slug;
            var newId = await _CategoryService.Update(CATEGORY);
            if (newId != 0)
            {
                TempData["UpdatedCategory"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong on our end. Try again in a few minutes.");
                return View(category);
            }

        }
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _CategoryService.GetById(id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var CATEGORY = _mapper.Map<UpdateCategoryDTO>(category);
            return View(CATEGORY);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, int? ID)
        {
            var CATEGORY = await _CategoryService.GetById(id);
            if (CATEGORY == null)
            {
                return RedirectToAction("Index");
            }
            CATEGORY.isDeleted = true;
            await _CategoryService.Delete(CATEGORY);
            TempData["DeletedCategory"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeletedCategories()
        {
            var deletedCategories = await _CategoryService.DeletedCaegories();
            return View(deletedCategories);
        }
        [HttpPost]
        public async Task<IActionResult> RestoreCategory(int id)
        {
            var CATEGORY = await _CategoryService.GetById(id);
            if (CATEGORY == null)
            {
                return RedirectToAction("DeletedCategories");
            }
            if (CATEGORY.isDeleted)
            {
                CATEGORY.isDeleted = false;
                var newId = await _CategoryService.RestoreDeletedCategory(CATEGORY);
                if (newId != 0)
                {
                    TempData["RestoreCategory"] = "Category restored successfully";
                    return RedirectToAction("DeletedCategories");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong on our end. Try again in a few minutes.");
                    return View(CATEGORY);
                }
            }
            return RedirectToAction("DeletedCategories");
        }
    }
}
