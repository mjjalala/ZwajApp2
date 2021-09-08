﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwajApp.API.Dtos;
using ZwajApp.API.Models;

namespace ZwajApp.API.Helpers
{
    public class AutoMapperProfiles :Profile
    {
        public  AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
            .ForMember(dest => dest.PhotoURL, opt => { opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url); })
            .ForMember(dest => dest.Age, opt => { opt.ResolveUsing(src => src.DateofBirth.CalculateAge()); });

            CreateMap<User, UserForDetailsDto>()
            .ForMember(dest => dest.PhotoURL, opt => { opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url); })
            .ForMember(dest => dest.Age, opt => { opt.ResolveUsing(src => src.DateofBirth.CalculateAge()); });

            CreateMap<Photo, PhotoForDetailsDto>();
        }

    }
}
