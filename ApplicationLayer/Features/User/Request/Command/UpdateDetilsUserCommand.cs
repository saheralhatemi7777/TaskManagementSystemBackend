using ApplicationLayer.DTOs.BaseValidationDtos;
using DomenLayer.UserEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.User.Request.Command
{
    public class UpdateDetilsUserCommand:IRequest<BaseValidtionDto>
    {
       
        public int Pk_Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

    }
}
