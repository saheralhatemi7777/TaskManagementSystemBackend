using ApplicationLayer.DTOs.Auth;
using ApplicationLayer.DTOs.UserDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.User.Request.Command
{
    public record LoginUserCommand(LoginUserDto _LoginUser) : IRequest<ResponseLogin> { }
    
    
}
