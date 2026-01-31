using ApplicationLayer.DTOs.UserDtos;
using ApplicationLayer.Features.User.Request.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.User.Validator
{
    public class UpdateDetileUserValidator:AbstractValidator<UpdateDetilsUserCommand>
    {

        public UpdateDetileUserValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .NotNull().WithMessage("يجب ادخال اسم مستخدم ")
                .MaximumLength(200).WithMessage("اسم مستخدم طويل لا يسمح به")
                .MinimumLength(10).WithMessage("يجب الا يقل اسم المستخدم عن 10 احرف");

            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull().WithMessage("يرجاء ادخال بيانات إيميل")
                .EmailAddress().WithMessage("يرجال ادخال صيغة إيميل صحيحة");

          
        }
    }
}
