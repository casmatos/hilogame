using AutoMapper;
using HiLoGame.Shared.DTO;
using HILoGame.Shared.Models;

namespace HILoGame.WebApi.Mapper
{
    public class ProfilePlayer : Profile
    {
        public ProfilePlayer()
        {
            CreateMap<Player, PlayerDTO>()
                .ReverseMap();
        }
    }
}
