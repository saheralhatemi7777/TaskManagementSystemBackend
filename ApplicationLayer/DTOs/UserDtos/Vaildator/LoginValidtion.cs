using ApplicationLayer.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.UserDtos.Vaildator
{
    public class LoginValidtion:AbstractValidator<LoginUserDto>
    {
        public LoginValidtion()
        {
            RuleFor(l => l.Email)
                .NotEmpty()
                .NotNull().WithMessage("يجب ادخال  الإيميل")
                .EmailAddress().WithMessage("صيغة إيميل غير صحيحة");

            RuleFor(l => l.Password)
               .NotEmpty()
               .NotNull().WithMessage("يجب ادخال كلمة المرور");
               

        }
    }
}
