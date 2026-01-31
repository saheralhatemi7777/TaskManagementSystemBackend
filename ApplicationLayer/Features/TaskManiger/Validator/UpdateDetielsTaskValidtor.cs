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
    public class UpdateDetielsTaskValidtor:AbstractValidator<UpdateDeteilsTaskCommand>
    {
        public UpdateDetielsTaskValidtor()
        {
            RuleFor(t => t.Description)
                .NotEmpty()
                .NotNull().WithMessage("يجب ادخال تفاصيل المهمه لا يقبل ترك الحقل فارغا")
                .MaximumLength(5000).WithMessage("يجب الا تزيد طول المهمه عن خمسة الف حرفا");

            RuleFor(t => t.Title)
                .NotEmpty()
                .NotNull().WithMessage("يلزم ادخال عنوان المهمه ")
                .MaximumLength(2000).WithMessage("يجب الا يزيد عنوان المهمه عن الفين حرفا")
                .MinimumLength(5).WithMessage("يجب الا  يقل  طول المهمه عن خمسة احرف");
        }
    }
}
