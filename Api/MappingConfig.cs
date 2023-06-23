using AutoMapper;
using Infraestrutura.Models;
using PosgramAPI.Models.Dto;

namespace Api
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
             
            CreateMap<Postagem, PostagemDto>().ReverseMap();
        }
    }
}
