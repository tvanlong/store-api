using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using storeAPI.DTOs;
using storeAPI.Models;
using Version = storeAPI.Models.Version;

namespace storeAPI.AutoMapper
{
    public class AppMapperProfile : Profile
    {
       public AppMapperProfile()
       {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Subcategory, SubcategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Version, VersionDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
       } 
    }
}