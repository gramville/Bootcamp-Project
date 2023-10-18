using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Yenetta_code.Configurations;
using Yenetta_code.Models.DTOs.AddDTOs;
using Yenetta_code.Models.DTOs.UpdateDTOs;
using Yenetta_code.Models.Services;
using Yenetta_code.Models;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;

namespace Yenetta_code.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;
        public BrandController(IBrandService brandService, IMapper mapper)
        {
            _brandService = brandService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAll();
            return View(brands);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddBrandDTO brand)
        {
            var slug = Slug.CreateSlug(brand.brandName);
            if(await _brandService.BrandNameExists(slug))
            {
                ModelState.AddModelError("brandName", "This brand name already exists");
                return View(brand);
            }
            var BRAND = _mapper.Map<Brand>(brand);
            BRAND.slug = slug;
            var newId = await _brandService.Add(BRAND);
            if (newId != 0)
            {
                TempData["AddedBrand"] = "Brand added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong on our end. Try again in a few minutes.");
                return View(brand);
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            var brand = await _brandService.GetById(id);
            if(brand == null)
            {
                return RedirectToAction("Index");
            }
            var BRAND = _mapper.Map<UpdateBrandDTO>(brand);
            return View(BRAND);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateBrandDTO brand)
        {
            var updatedbrand = await _brandService.GetById(brand.id);
            if(updatedbrand == null)
            {
                return RedirectToAction("Index");
            }
            string slug = "";
            if (!string.IsNullOrEmpty(brand.brandName))
            {
                slug = Slug.CreateSlug(brand.brandName);
                if (await _brandService.BrandNameExists(slug) && updatedbrand.slug != slug)
                {
                    ModelState.AddModelError("brandName", "the brand name already exists");
                    return View(brand);
                }

            }
            var BRAND = _mapper.Map<UpdateBrandDTO, Brand>(brand, updatedbrand);
            BRAND.slug = (!string.IsNullOrEmpty(brand.brandName)) ? slug : BRAND.slug;
            var newId = await _brandService.Update(BRAND);
            if (newId != 0)
            {
                TempData["UpdatedBrand"] = "Brand updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong on our end. Try again in a few minutes.");
                return View(brand);
            }

        }
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _brandService.GetById(id);
            if (brand == null)
            {
                return RedirectToAction("Index");
            }
            var BRAND = _mapper.Map<UpdateBrandDTO>(brand);
            return View(BRAND);
        }
        [HttpPost]
        public async Task<IActionResult>Delete(int id , int? ID)
        {
            var BRAND = await _brandService.GetById(id);
            if (BRAND == null)
            {
                return RedirectToAction("Index");
            }
            BRAND.isDeleted = true;
            await _brandService.Delete(BRAND);
            TempData["DeletedBrand"] = "Brand deleted successfully";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeletedBrands()
        {
            var deletedBrands = await _brandService.DeletedBrands();
            return View(deletedBrands);
        }
        public async Task<IActionResult> RestoreBrand(int id)
        {
            var BRAND = await _brandService.GetById(id);
            if(BRAND == null)
            {
                return RedirectToAction("DeletedBrands");
            }
            if(BRAND.isDeleted)
            {
                BRAND.isDeleted = false;
                var newId = await _brandService.RestoreDeletedBrand(BRAND);
                if (newId != 0)
                {
                    TempData["RestoreBrand"] = "Brand restored successfully";
                    return RedirectToAction("DeletedBrands");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong on our end. Try again in a few minutes.");
                    return View(BRAND);
                }
            }
            return RedirectToAction("DeletedBrands");
        }
    }
}
