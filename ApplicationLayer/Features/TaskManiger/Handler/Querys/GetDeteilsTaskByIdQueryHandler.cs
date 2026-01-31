using ApplicationLayer.DTOs.TaskItemDto;
using ApplicationLayer.Features.TaskManiger.Request.Querys;
using ApplicationLayer.Interfaces.UnitOfWork;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.TaskManiger.Handler.Querys
{
    public class GetDeteilsTaskByIdQueryHandler:IRequestHandler<GetDeteilsTaskByIdQuery,GetTaskitemsDto>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDeteilsTaskByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<GetTaskitemsDto> Handle(GetDeteilsTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var Command = await _unitOfWork._taskitemReposetory.GetDetileTaskByTaskId(request.Id);
            var Response = _mapper.Map<GetTaskitemsDto>(Command);
            return Response;
        }
    }
}
