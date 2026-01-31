using ApplicationLayer.DTOs.Auth;
using ApplicationLayer.DTOs.UserDtos;
using ApplicationLayer.Interfaces.Reposetory.BaseRepo;
using DomenLayer.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces.Reposetory.Auth
{
    public interface IAuthReposetory:IBaseReposetory<Users>
    {

        #region AuthRepo
        /// <summary>
        /// جلب البيانا حسب الايميل المحدد
        /// </summary>
        /// <param name="Email"></param>
        /// <returns>تعيد كائن فيه بيانات المستخدم</returns>
        Task<Users> GetUserByEmail(string Email);

        /// <summary>
        /// تسجيل دخول المستخدمين
        /// </summary>
        /// <param name="login"></param>
        /// <returns>تعيد بيانات المستخدم</returns>
        Task<ResponseLogin> LoginAsync(LoginUserDto login);
        /// <summary>
        /// تعديل كلمة مرور المستخدمين
        /// </summary>
        /// <param name="chenged"></param>
        /// <returns>؟؟</returns>
        Task<Users> ChingedPasswordAsync(ChengedPasswordDto chenged);
        #endregion
    }
}
