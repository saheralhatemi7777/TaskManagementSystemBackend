using ApplicationLayer.DTOs.TaskItemDto;
using ApplicationLayer.Features.TaskManiger.Request.Querys;
using ApplicationLayer.Interfaces.UnitOfWork;
using AutoMapper;
using MediatR;

namespace ApplicationLayer.Features.TaskManiger.Handler.Querys
{
    public class GetTaskByPk_IdQueryHandler : IRequestHandler<GetTaskByUserPk_IdQuery, List<GetTaskitemsDto>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetTaskByPk_IdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<List<GetTaskitemsDto>> Handle(GetTaskByUserPk_IdQuery request, CancellationToken cancellationToken)
        {
            var Command = await _unitOfWork._taskitemReposetory.GetTaskByUserPK_Id_WithFilteringTaskData(request.Pk_Id,request.FilterTask);
            var Response = _mapper.Map<List<GetTaskitemsDto>>(Command);
            return Response;
        }
    }
}
