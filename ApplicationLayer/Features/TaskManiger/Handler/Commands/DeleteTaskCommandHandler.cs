using ApplicationLayer.DTOs.BaseValidationDtos;
using ApplicationLayer.Features.TaskManiger.Request.Commands;
using ApplicationLayer.Features.TaskManiger.Validator;
using ApplicationLayer.Interfaces.UnitOfWork;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.TaskManiger.Handler.Commands
{
    public class DeleteTaskCommandHandler:IRequestHandler<DeleteTaskCommand,bool>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteTaskCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            //==
            var Command =await _unitOfWork._taskitemReposetory.GetByIdAsync(request.Pk_Id);
            if (Command is null)
            {
                return false;
            }
            else
            {
                await _unitOfWork._taskitemReposetory.DeleteAsync(Command);
                await _unitOfWork.Save();
                return true;
            }
        }
    }
}
