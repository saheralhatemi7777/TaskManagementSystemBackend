using ApplicationLayer.DTOs.Auth;
using ApplicationLayer.DTOs.UserDtos;
using ApplicationLayer.Features.User.Request.Command;
using AutoMapper;
using DomenLayer.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.User.Profiles
{
    public class ProfileUser:Profile
    {
        public ProfileUser()
        {
            CreateMap<Users, GetUserDtos>()
                         .ForMember(u => u.UserId, obj => obj.MapFrom(u => u.Pk_Id))
                         .ForMember(u => u.Role,   obj => obj.MapFrom(u => u.Role.ToString()));
            //create account
            CreateMap<RegisterUserDtos, Users>();
            //response login maping
            CreateMap<Users, ResponseLogin>()
                                     .ForMember(u => u.UserId, obj => obj.MapFrom(u => u.Pk_Id));
            //Update User Map
            CreateMap<UpdateDetilsUserDto, UpdateDetilsUserCommand>();
            CreateMap<UpdateDetilsUserCommand, Users>();
            //
            CreateMap<ChengedPasswordDto, Users>();

        }
    }
}
