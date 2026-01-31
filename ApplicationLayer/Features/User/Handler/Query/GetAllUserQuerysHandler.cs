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
    public class GetAllUserQuerysHandler : IRequestHandler<GetAllUserQuerys, List<GetUserDtos>>
    {
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllUserQuerysHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;

        }
        public async Task<List<GetUserDtos>> Handle(GetAllUserQuerys request, CancellationToken cancellationToken)
        {
            var query = await _unitOfWork._userReposetory.GetAllUserWithFilteringData(request._Filtering);
            if (query is null) throw new Exception();
            var Response = _mapper.Map<List<GetUserDtos>>(query);

            return Response;
        }
    }
}
