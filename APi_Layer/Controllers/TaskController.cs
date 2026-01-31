using ApplicationLayer.DTOs.TaskItemDto;
using ApplicationLayer.Features.TaskManiger.Request.Commands;
using ApplicationLayer.Features.TaskManiger.Request.Querys;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace APi_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        #region Injection_Constractor
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public TaskController(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }
        #endregion

        #region Query
        //HTTP:GET{}
        [HttpGet("GetTaskDataByUserId")]
        public async Task<ActionResult> GetTaskDataByUserId(int Pk_Id, [FromQuery] FilterTaskData filterTask)
        {
            var Command = new GetTaskByUserPk_IdQuery(Pk_Id, filterTask);
            var Response = await _mediator.Send(Command);
            if (Response is null) return BadRequest(Response);
            return Ok(Response);
        }


        [HttpGet("GetDetilsTaskById")]
        public async Task<ActionResult> GetDetilsTaskById(int Pk_Id)
        {
            var Command = new GetDeteilsTaskByIdQuery(Pk_Id);
            var Response =await _mediator.Send(Command);
            if (Response is null) return BadRequest("لا يوجد بيانات");
            return Ok(Response);

        }
        #endregion

        #region Commands
        //Http:POST{}
        [HttpPost("CreateNewTask")]
        public async Task<ActionResult> CreateNewTask(CreateNewTaskeDto create)
        {
            var Command = new CreateNewTaskCommand(create);
            var Response =  await _mediator.Send(Command);
            if(Response is null)return BadRequest(Response);
            return Ok(Response);
        }
       
        //HTTP:PUT{}
        [HttpPut("UpdateDeteilsTask")]
        public async Task<ActionResult>UpdateDeteilsTask([FromBody]UpdateTaskDto update)
        {
            var Command = _mapper.Map<UpdateDeteilsTaskCommand>(update);
            var Response =await _mediator.Send(Command);
            if (Response is null) return BadRequest(Response);
            return Ok(Response);
        }

        //HTTP:DELETE{}
        [HttpDelete("DeleteTask")]
        public async Task<ActionResult>DeleteTask(int Pk_Id)
        {
            var Command = new DeleteTaskCommand(Pk_Id);
            var Response =await _mediator.Send(Command);
            if (!Response) return BadRequest("لم يتم حذف المهمه");
            return Ok("تم حذف المهمه");
        }

        //Http:PUT{}
        [HttpPatch("UpdateStatusTasks")]
        public async Task<ActionResult>UpdateStatusTasks(int Id,UpdateStatusDto update)
        {
            update.TaskId = Id;
            var Command = _mapper.Map<UpdateStatusTaskCommand>(update);
            var Response =await _mediator.Send(Command);
            if (Response is null) return BadRequest(Response);
            return Ok(Response);
        }
        #endregion
    }
}
