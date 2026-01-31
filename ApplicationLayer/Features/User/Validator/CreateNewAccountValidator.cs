using ApplicationLayer.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.User.Validator
{
    public class CreateNewAccountValidator:AbstractValidator<RegisterUserDtos>
    {
        public CreateNewAccountValidator()
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

            RuleFor(u => u.PasswordHash)
                .NotEmpty()
                .NotNull().WithMessage("يرجال ادخل كلمة مرور")
                .MinimumLength(9).WithMessage("يرجاء ادخال كلمة مرور اكبر من 9 ارقام")
                .MaximumLength(20).WithMessage("يجب الا تزيد كلمة المرور عن 20 حرفا");
        }
    }
}
