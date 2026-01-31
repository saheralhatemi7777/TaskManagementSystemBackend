using ApplicationLayer.DTOs.BaseValidationDtos;
using ApplicationLayer.Features.TaskManiger.Request.Commands;
using ApplicationLayer.Features.TaskManiger.Validator;
using ApplicationLayer.Interfaces.UnitOfWork;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.TaskManiger.Handler.Commands
{
    public class UpdateStatusTaskCommandHandler:IRequestHandler<UpdateStatusTaskCommand,BaseValidtionDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateStatusTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<BaseValidtionDto> Handle(UpdateStatusTaskCommand request, CancellationToken cancellationToken)
        {
            var Response = new BaseValidtionDto();
            var Validation = new UpdateStatusTaskValidation();
            var Validtor = await Validation.ValidateAsync(request);
            if (!Validtor.IsValid)
            {
              Response.success = false;
              Response.Message = "حدث خطاء";
              Response.Errors = Validtor.Errors.Select(x => x.ErrorMessage).ToList();
              return Response;
            }
            var Command = await _unitOfWork._taskitemReposetory.GetByIdAsync(request.TaskId);
            if (Command is null)
            {
                Response.success = false;
                Response.Message = "لا يوجد بيانات تحقق من صحة الطلب";
                //Response.Errors = Validtor.Errors.Select(x => x.ErrorMessage).ToList();
                return Response;
            }
            else
            {
                _mapper.Map(request, Command);
                await _unitOfWork._taskitemReposetory.UpdateAsync(Command);
                await _unitOfWork.Save();
                Response.success = true;
                Response.Message = "تم تحديث حالة المهمهة";
                //Response.Errors = Validtor.Errors.Select(x => x.ErrorMessage).ToList();
            }
            return Response;
        }
    }
}
