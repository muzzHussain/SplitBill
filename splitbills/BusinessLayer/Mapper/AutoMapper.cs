using AutoMapper;
using DAL.Models;
using SharedLayer;


using SharedLayer.DTOs;

namespace BusinessLayer.Mapper
{
    public  class AutoMapper: Profile
    {
        public AutoMapper()
        {

            CreateMap<WalletRequestDTO, Group>();

            CreateMap<WalletRequestDTO, Group>().ReverseMap();
            CreateMap<User, SignUpDTO>().ReverseMap();
            CreateMap<UsersGroup, GroupRequestDTO>().ReverseMap();

        }
    }
}
