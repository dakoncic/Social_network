using AutoMapper;
using FunAndGames.Models;
using ModelsLayer.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunAndGames.AutoMapper
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<CreatePostModelDTO, CreatePostModel>()
                .ReverseMap();
        }
    }
}
