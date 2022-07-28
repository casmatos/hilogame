using AutoMapper;
using HiLoGame.Shared.DTO;
using HILoGame.Shared.Models;

namespace HILoGame.WebApi.Mapper
{
    public class ProfileRoom : Profile
    {
        public ProfileRoom()
        {
            CreateMap<Room, RoomDTO>()
                .ReverseMap();

        }
    }
}
