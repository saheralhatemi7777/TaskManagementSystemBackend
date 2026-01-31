using ApplicationLayer.DTOs.UserDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.User.Request.Query
{
    public record GetAllUserQuerys(FilteringData _Filtering):IRequest<List<GetUserDtos>> { }
   
}
