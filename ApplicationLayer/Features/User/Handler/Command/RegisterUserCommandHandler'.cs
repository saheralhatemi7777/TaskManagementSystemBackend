using ApplicationLayer.DTOs.BaseValidationDtos;
using ApplicationLayer.DTOs.UserDtos.Vaildator;
using ApplicationLayer.Features.User.Request.Command;
using ApplicationLayer.Features.User.Validator;
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
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, BaseValidtionDto>
    {
        private readonly IAuthReposetory _authRepository;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IAuthReposetory authReposetory, IMapper mapper)
        {
            _authRepository = authReposetory;
            _mapper = mapper;
        }

        public async Task<BaseValidtionDto> Handle(RegisterUserCommand request,CancellationToken cancellationToken)
        {
               var response = new BaseValidtionDto();
             
                //  التحقق من صحة البيانات
                var validation = new CreateValidation();
                var validationResult = await validation.ValidateAsync(request._RegisterUserDtos, cancellationToken);

                if (!validationResult.IsValid)
                {
                    response.success = false;
                    response.Message = "خطاء تحقق من صحة البيانات";
                    response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return response;
                }
                //  التحقق من عدم تكرار البريد الإلكتروني
                var existingUser = await _authRepository.GetUserByEmail(request._RegisterUserDtos.Email);
                if (existingUser != null)
                {
                   response.success = false;
                   response.Message = "البريد الإلكتروني مستخدم مسبقًا";
                   return response;
                }

                else
                {
                  //  تحويل DTO إلى Entity
                   var userEntity = _mapper.Map<Users>(request._RegisterUserDtos);

                  //  تشفير كلمة المرور
                  userEntity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request._RegisterUserDtos.PasswordHash);

                  //  حفظ المستخدم
                  await _authRepository.AddAsync(userEntity);
                  await _authRepository.SaveAsync();

                  response.success = true;
                  response.Message = "تم إنشاء المستخدم بنجاح";
                // response.Id = userEntity.Pk_Id;
              }
           
                return response;
        }
    }
}
