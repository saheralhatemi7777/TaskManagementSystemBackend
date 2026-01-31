using ApplicationLayer.DTOs.Auth;
using ApplicationLayer.DTOs.UserDtos;
using ApplicationLayer.Features.User.Request.Command;
using ApplicationLayer.Interfaces.Reposetory.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APi_Layer.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Authorize
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost(Name ="Login")]
        public async Task<ActionResult<ResponseLogin>> Login(LoginUserDto login)
        {
            var Command =new LoginUserCommand(login);
            var reponse =await _mediator.Send(Command);
            if (reponse is null) return BadRequest(reponse);
            return Ok(reponse);
        }

        //[Authorize(Roles = "User")]
        [HttpPost("CreateAcount")]
        public async Task<ActionResult> CreateAcount(RegisterUserDtos register)
        {
            var Command = new RegisterUserCommand(register);
            var Response = await _mediator.Send(Command);
            if(Response is null)return BadRequest(Response);
            return Ok(Response);
        }

        //[Authorize(Roles = "User")]

        [HttpPut("ChengedPassword")]
        public async Task<ActionResult<bool>> ChengedPassword(ChengedPasswordDto chenged)
        {
            var Command = new ChengedPasswordCommand(chenged);
            var Response =await _mediator.Send(Command);
            if(Response is null) return BadRequest(Response);
            return Ok(Response);
        }

        #endregion
    }
}
