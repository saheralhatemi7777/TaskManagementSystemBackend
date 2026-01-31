using ApplicationLayer.DTOs.BaseValidationDtos;
using ApplicationLayer.DTOs.TaskItemDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.TaskManiger.Request.Commands
{
    public class UpdateDeteilsTaskCommand: IRequest<BaseValidtionDto> 
    {
        public int Pk_Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;



        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        //Fk
        public int UserId { get; set; }
    }
}
