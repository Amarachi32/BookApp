using AutoMapper;
using BussinessLogic.Models;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.MappingProfile
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            // Assuming your Author class has a property called "Name" that is of type string.
            CreateMap<AppUser, IdentityUser>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src));

            CreateMap<AddUpdateBookVM, Book>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id during mapping
           // .ForMember(dest => dest.Author, opt => opt.MapFrom(src => DateTime.Parse(src.Author)));
        }

    }
}
