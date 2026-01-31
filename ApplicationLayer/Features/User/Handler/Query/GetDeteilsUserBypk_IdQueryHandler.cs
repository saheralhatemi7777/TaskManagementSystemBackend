using ApplicationLayer.DTOs.UserDtos;
using ApplicationLayer.Features.User.Request.Query;
using ApplicationLayer.Interfaces.UnitOfWork;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.User.Handler.Query
{
    public class GetDeteilsUserBypk_IdQueryHandler:IRequestHandler<GetDeteilsUserBypk_IdQuery,GetUserDtos>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDeteilsUserBypk_IdQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<GetUserDtos> Handle(GetDeteilsUserBypk_IdQuery request, CancellationToken cancellationToken)
        {
            var Command = await _unitOfWork._userReposetory.GetByIdAsync(request._Pk_Id);
            var Response = _mapper.Map<GetUserDtos>(Command);
            return Response;
        }
    }
}
