using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Dtos;
using DatingApp.Models;

namespace DatingApp.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Users, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.Age, otp => otp.MapFrom(d => d.DateOfBirth.CalculateAge(DateTime.Today)));
            CreateMap<Users, UserForDetailedDto>()
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
            .ForMember(dest => dest.Age, otp => otp.MapFrom(d => d.DateOfBirth.CalculateAge(DateTime.Today)));
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto, Users>();
            CreateMap<Photo, PhotoForPhotoReturnDto>();
            CreateMap<PhotoForPhotoCreationDto, Photo>();
        }

    }
}
