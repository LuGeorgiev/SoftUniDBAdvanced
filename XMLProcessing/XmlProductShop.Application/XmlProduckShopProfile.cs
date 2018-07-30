using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using XmlProductShop.DataProcessing.Dtos.Import;
using XMLProductShop.Models;

namespace XmlProductShop.Application
{
    public class XmlProduckShopProfile:Profile
    {
        public XmlProduckShopProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<CategoryDto, Category>();
        }
    }
}
