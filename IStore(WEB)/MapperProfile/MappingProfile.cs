using AutoMapper;
using Domain.EF_Models;
using IStore_WEB_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IStore_WEB_.MapperProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentViewModel>().
                ForMember("DateShort", opt => opt.MapFrom(x => x.Date.ToShortDateString())).
                ForMember("TimeShort", opt => opt.MapFrom(x => x.Date.ToShortTimeString())).
                ForMember("Nick", opt => opt.MapFrom(x => x.User.Email));
        }
    }
}
