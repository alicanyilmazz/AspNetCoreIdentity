using AspNetCoreIdentityApp.Core.Entities;
using AspNetCoreIdentityApp.Core.ViewModels.Areas.Admin;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreIdentityApp.Service.DtoMappers
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<AppUser, UserViewModel>().ReverseMap();
        }
    }
}
