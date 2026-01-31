using ApplicationLayer.DTOs.TaskItemDto;
using ApplicationLayer.Features.TaskManiger.Request.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.TaskManiger.Validator
{
    public class UpdateStatusTaskValidation:AbstractValidator<UpdateStatusTaskCommand>
    {
        public UpdateStatusTaskValidation()
        {
            RuleFor(t=>t.TaskId).NotNull().WithMessage("تحقق من صحة الطلب لا تتوفر بيانات");
            RuleFor(t => t.Status)
                .NotEmpty()
                .NotNull().WithMessage("تحقق من صحة البيانات المدخلة");
        }
    }
}
