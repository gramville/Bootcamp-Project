using AutoMapper;
using System.Security.Principal;
using Yenetta_code.Models;
using Yenetta_code.Models.DTOs.AddDTOs;
using Yenetta_code.Models.DTOs.ResponseDTOs;
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
            CreateMap<UpdateProductDTO, Product>()
            .ForMember(dest => dest.productName, opt => opt.Condition((src, dest) => src.productName != null))
             .ForMember(dest => dest.description, opt => opt.Condition((src, dest) => src.description != null))
              .ForMember(dest => dest.price, opt => opt.Condition((src, dest) => src.price != null))
               .ForMember(dest => dest.quantity, opt => opt.Condition((src, dest) => src.quantity != null));
            CreateMap<Product, ProductResponseDTO>();
            CreateMap<ProductResponseDTO, Product>();
            CreateMap<Brand,AddBrandDTO>();
            CreateMap<AddBrandDTO,Brand>();
            CreateMap<Brand, UpdateBrandDTO>();
            CreateMap<UpdateBrandDTO,Brand>()

                 .ForMember(dest => dest.brandName, opt => opt.Condition((src, dest) => src.brandName != null));
            CreateMap<Category,AddCategoryDTO>();
            CreateMap<AddCategoryDTO,Category>();
            CreateMap<Category,UpdateCategoryDTO>();
            CreateMap<UpdateCategoryDTO, Category>()
                 .ForMember(dest => dest.categoryName, opt => opt.Condition((src, dest) => src.categoryName != null));
        }

    } 
}
