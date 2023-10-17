using AutoMapper;
using System.Security.Principal;
using Yenetta_code.Models;
using Yenetta_code.Models.DTOs.AddDTOs;
using Yenetta_code.Models.DTOs.UpdateDTOs;

namespace Yenetta_code.Configurations
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {

            CreateMap<Product, AddProductDTO>();
            CreateMap<AddProductDTO, Product>();
            CreateMap<Product, UpdateProductDTO>();
            CreateMap<UpdateProductDTO,Product>();
            CreateMap<Brand,AddBrandDTO>();
            CreateMap<AddBrandDTO,Brand>();
            CreateMap<Brand,UpdateBrandDTO>();
            CreateMap<UpdateBrandDTO,Brand>();
            CreateMap<Category,AddCategoryDTO>();
            CreateMap<AddCategoryDTO,Category>();
            CreateMap<Category,UpdateCategoryDTO>();
            CreateMap<UpdateCategoryDTO,Category>();
        }

    } 
}
