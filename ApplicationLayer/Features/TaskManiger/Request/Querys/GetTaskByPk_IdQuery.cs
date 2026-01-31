using ApplicationLayer.DTOs.BaseValidationDtos;
using ApplicationLayer.DTOs.TaskItemDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.TaskManiger.Request.Querys
{
    public record GetTaskByUserPk_IdQuery(int Pk_Id,FilterTaskData FilterTask) : IRequest<List<GetTaskitemsDto>> { }
    
}
