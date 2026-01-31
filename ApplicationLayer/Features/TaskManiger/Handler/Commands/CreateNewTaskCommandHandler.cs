using ApplicationLayer.DTOs.BaseValidationDtos;
using ApplicationLayer.Features.TaskManiger.Request.Commands;
using ApplicationLayer.Features.TaskManiger.Validator;
using ApplicationLayer.Interfaces.UnitOfWork;
using AutoMapper;
using DomenLayer.TaskItemEntitys;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.TaskManiger.Handler.Commands
{
    public class CreateNewTaskCommandHandler:IRequestHandler<CreateNewTaskCommand,BaseValidtionDto>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateNewTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<BaseValidtionDto> Handle(CreateNewTaskCommand request, CancellationToken cancellationToken)
        {
            var Response = new BaseValidtionDto();
            var Validation = new CreateNewTaskValidator();
            var Validator =await Validation.ValidateAsync(request._Create, cancellationToken);
            if (!Validator.IsValid)
            {
                Response.success = false;
                Response.Message = "حدث خطاء اثناء الاضافة";
                Response.Errors = Validator.Errors.Select(x => x.ErrorMessage).ToList();
                return Response;
            }
            else
            {
                var Command = _mapper.Map<TaskItem>(request._Create);
                await _unitOfWork._taskitemReposetory.AddAsync(Command);
                await _unitOfWork.Save();
                Response.success = true;
                Response.Message = "تم اضافة المهمه بنجاح";
                //Response.Errors = Validator.Errors.Select(x => x.ErrorMessage).ToList();
                return Response;
            }
        }
    }
}
