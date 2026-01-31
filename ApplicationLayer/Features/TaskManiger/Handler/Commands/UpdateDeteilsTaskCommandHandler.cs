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
    public class UpdateDeteilsTaskCommandHandler : IRequestHandler<UpdateDeteilsTaskCommand, BaseValidtionDto>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateDeteilsTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<BaseValidtionDto> Handle(UpdateDeteilsTaskCommand request, CancellationToken cancellationToken)
        {
            var Response = new BaseValidtionDto();
            var Validation = new UpdateDetielsTaskValidtor();
            var Validator = await Validation.ValidateAsync(request);
            if (!Validator.IsValid)
            {
                Response.success = false;
                Response.Message = "حدث خطاء اثناء التعديل";
                Response.Errors = Validator.Errors.Select(x => x.ErrorMessage).ToList();
                return Response;
            }
            var Command = await _unitOfWork._taskitemReposetory.GetByIdAsync(request.Pk_Id);
            if (Command is null)
            {
                Response.success = false;
                Response.Message = "لا يوجد بيانات تحقق من صحة الطلب";
                Response.Errors = Validator.Errors.Select(x => x.ErrorMessage).ToList();
                return Response;
            }
            else
            {
                _mapper.Map(request, Command);
                await _unitOfWork._taskitemReposetory.UpdateAsync(Command);
                await _unitOfWork._taskitemReposetory.SaveAsync();
                Response.success = true;
                Response.Message = "تم تعديل البيانات بنجاح";
                //Response.Errors = Validator.Errors.Select(x => x.ErrorMessage).ToList();
                return Response;
            }
        }
    }
}
