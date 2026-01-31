using ApplicationLayer.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.UserDtos.Vaildator
{
    public class CreateValidation:AbstractValidator<RegisterUserDtos>
    {
        public CreateValidation()
        {

           RuleFor(i => i.Name)
            .MaximumLength(200)
            .WithMessage("يجب ان لا يزيد اسم المستخدم عن 200 حرف خطاء ")
            .NotEmpty()
            .NotNull()
            .WithMessage("لا يسمح بادخال قيمة فارغة ادخل اسم مستخدم صحيح");

            RuleFor(i => i.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("لا يسمح باستخدام قيمة إيميل فارغة")
                .EmailAddress()
                .WithMessage("صيغة إيميل غير صحيحة");

            RuleFor(i => i.PasswordHash)
              .MaximumLength(200)
              .WithMessage("يجب الا تزيد كلمة المرور عن 200 رمز")
              .MinimumLength(8)
              .WithMessage("يجب ان تحوي كلمة المرور اكثر من 8 رموز")
              .NotEmpty()
              .NotNull()
              .WithMessage("لا يسمح بادخال قيمة فارغة تحقق من كلمة المرور");

          
        }
    }
}
