using ApplicationLayer.DTOs.BaseValidationDtos;
using ApplicationLayer.DTOs.UserDtos.Vaildator;
using ApplicationLayer.Features.User.Request.Command;
using ApplicationLayer.Interfaces.Reposetory.Auth;
using ApplicationLayer.Interfaces.UnitOfWork;
using AutoMapper;
using DomenLayer.UserEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.User.Handler.Command
{
    public class ChengedPasswordCommandHandler:IRequestHandler<ChengedPasswordCommand, BaseValidtionDto>
    {

        private readonly IAuthReposetory _authReposetory;
        private readonly IMapper _mapper;
        public ChengedPasswordCommandHandler(IAuthReposetory authReposetory, IMapper mapper)
        {
            this._authReposetory = authReposetory;
            this._mapper = mapper;
        }

        public async Task<BaseValidtionDto> Handle(ChengedPasswordCommand request, CancellationToken cancellationToken)
        {
            var Response = new BaseValidtionDto();
            var Validation = new CheingedPasswordValidation();

            var Validator = await Validation.ValidateAsync(request.Chenged);
            if (!Validator.IsValid)
            {
                Response.success = false;
                Response.Message = "حدث خطاء";
                Response.Errors =Validator.Errors.Select(x => x.ErrorMessage).ToList();
                return Response;
            }

            var user =await _authReposetory.ChingedPasswordAsync(request.Chenged);
            if (user is null)
            {
                Response.success = false;
                Response.Message = "حدث خطاء لا يوجد بيانات تحقق من صحة الطلب ";
                Response.Errors = Validator.Errors.Select(x => x.ErrorMessage).ToList();
                return Response;
            }

            else
            {
                await _authReposetory.UpdateAsync(user);
                await _authReposetory.SaveAsync();
                Response.success = true;
                Response.Message = "تم تعديل كلمة المرور بنجاح";
                //Response.Errors = Validator.Errors.Select(x => x.ErrorMessage).ToList();
            }

            return Response;
        }


    }
}
