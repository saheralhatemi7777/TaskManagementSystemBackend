using ApplicationLayer.Interfaces.Reposetory.Auth;
using ApplicationLayer.Interfaces.Reposetory.TaskRepo;
using ApplicationLayer.Interfaces.Reposetory.UserRepo;
using ApplicationLayer.Interfaces.UnitOfWork;
using InforsturctiorLayer.DbContextFolder;
using InforsturctiorLayer.Interfaces.Reposetory.AuthRepo;
using InforsturctiorLayer.Interfaces.Reposetory.TaskItemRepo;
using InforsturctiorLayer.Interfaces.Reposetory.UserRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforsturctiorLayer.Interfaces.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
           this._context = applicationDbContext;
           this. _taskitemReposetory =new TaskItemReposetory(_context);
           this._userReposetory =new UserReposetory(_context);
        }
        public IUserReposetory _userReposetory {  get; private set; }

        public ITaskitemReposetory _taskitemReposetory { get; private set; }


        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
