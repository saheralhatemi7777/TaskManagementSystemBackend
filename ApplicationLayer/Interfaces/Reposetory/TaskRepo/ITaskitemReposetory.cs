using ApplicationLayer.DTOs.TaskItemDto;
using ApplicationLayer.Interfaces.Reposetory.BaseRepo;
using DomenLayer.TaskItemEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces.Reposetory.TaskRepo
{
    public interface ITaskitemReposetory:IBaseReposetory<TaskItem>
    {
        /// <summary>
        /// عرض بيانات المهمه مع فلترة شامله للبيانات
        /// </summary>
        /// <param name="Pk_Id"></param>
        /// <param name="filterTask"></param>
        /// <returns>يعيد بيانات المهام</returns>
        Task<IEnumerable<TaskItem>> GetTaskByUserPK_Id_WithFilteringTaskData(int Pk_Id,FilterTaskData filterTask);

        /// <summary>
        /// عرض بيانات المهمه حسب الرقم المعرف
        /// </summary>
        /// <param name="Pk_Id">رقم معرف المهمه</param>
        /// <returns>تعيد كان فيه بيانات المهمه</returns>
        Task<TaskItem> GetDetileTaskByTaskId(int Pk_Id);
    }
}
