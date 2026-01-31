using ApplicationLayer.DTOs.BaseValidationDtos;
using ApplicationLayer.DTOs.UserDtos;
using ApplicationLayer.DTOs.UserDtos.Vaildator;
using ApplicationLayer.Features.User.Request.Command;
using ApplicationLayer.Interfaces.Reposetory.Auth;
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
    public class LoginUserCommandHandler:IRequestHandler<LoginUserCommand,ResponseLogin>
    {

        private readonly IAuthReposetory _authReposetory;
        private readonly IMapper _mapper;
        public LoginUserCommandHandler(IAuthReposetory authReposetory, IMapper mapper)
        {
            this._authReposetory = authReposetory;
            this._mapper = mapper;
        }

        public async Task<ResponseLogin> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var Response = new BaseValidtionDto();
            var Validtion = new LoginValidtion();
            var Validator = await Validtion.ValidateAsync(request._LoginUser, cancellationToken);
            if (!Validator.IsValid)
            {
                Response.Errors = Validator.Errors.Select(e => e.ErrorMessage).ToList();
            }
            //==Hashing Password
            var user = await _authReposetory.LoginAsync(request._LoginUser);

            if (user == null)
                throw new Exception("User not found");

            // ثم أرجع Response أو أنشئ JWT
            var response = _mapper.Map<ResponseLogin>(user);
            return response;

        }
    }
}
