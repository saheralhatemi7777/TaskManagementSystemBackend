using ApplicationLayer.DTOs.UserDtos;
using ApplicationLayer.Features.User.Request.Command;
using ApplicationLayer.Features.User.Request.Query;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace APi_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UserController(IMediator mediator,IMapper mapper)
        {
            this._mediator=mediator;
            this._mapper=mapper;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet  (Name  = "GetAllUsers")]
        public async Task<ActionResult<IEnumerable<GetUserDtos>>> GetAllUsers([FromQuery]FilteringData filtering)
        {
            var Command = new GetAllUserQuerys(filtering);
            var Response =await _mediator.Send(Command);
            if (Response is null) return BadRequest();
            return Ok(Response);
        }

        [HttpGet("GetUserBy_PK_Id")]
        public async Task<ActionResult<GetUserDtos>> GetUserBy_PK_Id(int Id)
        {
            var Command = new GetDeteilsUserBypk_IdQuery(Id);
            var Response = await _mediator.Send(Command);
            if (Response is null) return BadRequest("خطاء لا يوجد بيانات");
            return Ok(Response);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPut("UpdateDetilesUser")]
        public async Task<ActionResult> UpdateDetilesUser(UpdateDetilsUserDto update)
        {
            // جلب الـ userId من التوكن
            var userIdFromToken = User.FindFirst("id")?.Value;

            //لا يمسح للمستخدم حتى وان كان Admin بتغير بيانات المستخدم امان عالي للبيانات
            if (!User.IsInRole("Admin") && userIdFromToken != update.Pk_Id.ToString())
            {
                return Unauthorized("لا يمكنك تعديل بيانات مستخدم آخر");
            }

            
            var command = _mapper.Map<UpdateDetilsUserCommand>(update);
            var response = await _mediator.Send(command);

            if (response == null)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
