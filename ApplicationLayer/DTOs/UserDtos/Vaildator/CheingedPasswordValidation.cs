using ApplicationLayer.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.UserDtos.Vaildator
{
    public class CheingedPasswordValidation : AbstractValidator<ChengedPasswordDto>
    {
        public CheingedPasswordValidation()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull().WithMessage("لا يوجد بيانات إيميل تحقق من صحة الطلب")
                .EmailAddress().WithMessage("صيغة ايميل غير صحيحة");
            RuleFor(u => u.NewPassword)
               .MaximumLength(20).WithMessage("يجب الا تزيد كلمة المرور عن 20 رمز")
               .MinimumLength(8).WithMessage("يجب الا تقل كلمة المرور عن 8 رمز")
               .NotEmpty()
               .NotNull().WithMessage("لا يوجد بيانات ");
        }
    }
}
