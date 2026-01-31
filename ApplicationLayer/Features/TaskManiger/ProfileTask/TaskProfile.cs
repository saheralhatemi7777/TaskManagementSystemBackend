using ApplicationLayer.DTOs.TaskItemDto;
using ApplicationLayer.Features.TaskManiger.Request.Commands;
using AutoMapper;
using DomenLayer.TaskItemEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.TaskManiger.ProfileTask
{
    public class TaskProfile:Profile
    {
        //Work Mapping bettwen TaskItem Entity and TaskDtos
        public TaskProfile()
        {
            //Createing Map
            CreateMap<CreateNewTaskeDto, TaskItem>();

            //Get Data Map
            CreateMap<TaskItem, GetTaskitemsDto>()
                .ForMember(u => u.UserName, obj => obj.MapFrom(u => u.users.Name))
                .ForMember(u => u.CreateAt, obj => obj.MapFrom(u => u.CreateAt.ToString()))
                .ForMember(u => u.UpdatedAt, obj => obj.MapFrom(u => u.UpdatedAt.ToString()));

            //Update Task Detils Map
            CreateMap<UpdateTaskDto, UpdateDeteilsTaskCommand>();
            CreateMap<UpdateDeteilsTaskCommand, TaskItem>();

            //Update Status Task Map
            CreateMap<UpdateStatusDto, UpdateStatusTaskCommand>();
            CreateMap<UpdateStatusTaskCommand, TaskItem>();
        }
    }
}
