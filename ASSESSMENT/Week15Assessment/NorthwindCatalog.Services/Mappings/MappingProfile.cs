using AutoMapper;
using NorthwindCatalog.Services.DTOs;
using NorthwindCatalog.Services.Models;

namespace NorthwindCatalog.Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => BuildCategoryImageUrl(src.CategoryName)));

            CreateMap<Product, ProductDto>();
        }

        private static string BuildCategoryImageUrl(string categoryName)
        {
            var normalized = categoryName.Replace("/", string.Empty).Replace("\\", string.Empty);
            return $"/images/{normalized}.jpeg";
        }
    }
}
