using ApplicationLayer.DTOs.UserDtos;
using ApplicationLayer.Interfaces.Reposetory.BaseRepo;
using DomenLayer.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces.Reposetory.UserRepo
{
    public interface IUserReposetory:IBaseReposetory<Users>
    {
        /// <summary>
        /// عرض بيانات المستخدمين مع فلترة شامه للبيانات
        /// </summary>
        /// <param name="filtering"> كائن الفلترة</param>
        /// <returns>تعيد بيانات المستخدمين</returns>
        Task<IEnumerable<Users>> GetAllUserWithFilteringData(FilteringData filtering);
    }
}
