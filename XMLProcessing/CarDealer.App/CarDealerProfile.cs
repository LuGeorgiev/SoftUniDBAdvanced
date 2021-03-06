﻿using AutoMapper;
using CarDealer.DataProcessing.Dtos.Import;
using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.App
{
    public class CarDealerProfile:Profile
    {
        public CarDealerProfile()
        {
            CreateMap<SupplierDto, Supplier>();
            CreateMap<PartDto, Part>();
            CreateMap<CarDto, Car>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}
