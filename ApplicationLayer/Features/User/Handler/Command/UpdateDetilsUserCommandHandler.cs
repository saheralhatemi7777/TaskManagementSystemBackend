using ApplicationLayer.DTOs.BaseValidationDtos;
using ApplicationLayer.Features.User.Request.Command;
using ApplicationLayer.Features.User.Validator;
using ApplicationLayer.Interfaces.UnitOfWork;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.User.Handler.Command
{
    public class UpdateDetilsUserCommandHandler:IRequestHandler<UpdateDetilsUserCommand,BaseValidtionDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateDetilsUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<BaseValidtionDto> Handle(UpdateDetilsUserCommand request, CancellationToken cancellationToken)
        {

            var Response = new BaseValidtionDto();
            var Validation = new UpdateDetileUserValidator();

            var Validator = await Validation.ValidateAsync(request);
            if (!Validator.IsValid)
            {
                Response.success = false;
                Response.Message = "حدث خطاء";
                Response.Errors = Validator.Errors.Select(x => x.ErrorMessage).ToList();
                return Response;
            }
            var Command =await _unitOfWork._userReposetory.GetByIdAsync(request.Pk_Id);
            if (Command is null)
            {
                Response.success = false;
                Response.Message = "طلب خاطئ لا يوجد بيانات";
                //Response.Errors = Validator.Errors.Select(x => x.ErrorMessage).ToList();
                return Response;
            }
            else
            {
                _mapper.Map(request, Command);
                await _unitOfWork._userReposetory.UpdateAsync(Command);
                await _unitOfWork.Save();
                Response.success = true;
                Response.Message = "تم تعديل بياناتك بنجاح";
               return Response;
            }
        }
    }
}
