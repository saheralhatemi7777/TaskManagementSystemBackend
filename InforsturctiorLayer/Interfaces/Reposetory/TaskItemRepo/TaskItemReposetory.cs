using ApplicationLayer.DTOs.TaskItemDto;
using ApplicationLayer.Interfaces.Reposetory.TaskRepo;
using DomenLayer.TaskItemEntitys;
using InforsturctiorLayer.DbContextFolder;
using InforsturctiorLayer.Interfaces.Reposetory.BaseRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforsturctiorLayer.Interfaces.Reposetory.TaskItemRepo
{
    //inherating  From  BaseReposetory and OverWriteing ITaskitemReposetory
    public class TaskItemReposetory:BaseReposetory<TaskItem>,ITaskitemReposetory
    {

        public TaskItemReposetory(ApplicationDbContext context):base(context) { }

        public async Task<TaskItem?> GetDetileTaskByTaskId(int Pk_Id)
        {
            var Query = _context.taskItems.Include(u=>u.users).FirstOrDefaultAsync(t=>t.Pk_Id == Pk_Id);
            return await Query;
        }

        public async Task<IEnumerable<TaskItem>> GetTaskByUserPK_Id_WithFilteringTaskData(int Pk_Id,FilterTaskData filterTask)
        {
            var Query = _context.taskItems.Include(u => u.users).Where(u => u.users.Pk_Id == Pk_Id).AsQueryable();
            //Logec filtering TaskData
            if (!string.IsNullOrEmpty(filterTask.Title)) Query = Query.Where(t => t.Title.Contains(filterTask.Title));
            if (!string.IsNullOrEmpty(filterTask.Descreption)) Query = Query.Where(t => t.Description.Contains(filterTask.Descreption));
            if (!string.IsNullOrEmpty(filterTask.Status)) Query = Query.Where(t => t.Status==filterTask.Status);
            //Date
            if (filterTask.CreateAt.HasValue) Query = Query.Where(t => t.CreateAt == filterTask.CreateAt.Value.Date);
            if (filterTask.UpdateDate.HasValue) Query = Query.Where(t => t.UpdatedAt == filterTask.UpdateDate.Value.Date);
            //==
            return await Query.ToListAsync();
        }
    }
}
