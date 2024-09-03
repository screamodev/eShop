using AutoMapper;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos.CatalogBrand;
using Catalog.Host.Models.Dtos.CatalogGender;
using Catalog.Host.Models.Dtos.CatalogItem;
using Catalog.Host.Models.Dtos.CatalogSize;
using Catalog.Host.Models.Dtos.CatalogType;

namespace Catalog.Host.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogItem, CatalogItemDto>()
            .ForMember("PictureUrl", opt
                => opt.MapFrom<CatalogItemPictureResolver, string>(c => c.PictureFileName));
        CreateMap<CatalogGender, CatalogGenderDto>();
        CreateMap<CatalogGenderCreateDto, CatalogGender>();
        CreateMap<CatalogGenderUpdateDto, CatalogGender>();
        CreateMap<CatalogSize, CatalogSizeDto>();
        CreateMap<CatalogSizeCreateDto, CatalogSize>();
        CreateMap<CatalogSizeUpdateDto, CatalogSize>();
        CreateMap<CatalogBrand, CatalogBrandDto>();
        CreateMap<CatalogBrandCreateDto, CatalogBrand>();
        CreateMap<CatalogBrandUpdateDto, CatalogBrand>();
        CreateMap<CatalogType, CatalogTypeDto>();
        CreateMap<CatalogTypeCreateDto, CatalogType>();
        CreateMap<CatalogTypeUpdateDto, CatalogType>();
    }
}