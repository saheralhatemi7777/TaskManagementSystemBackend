using ApplicationLayer.Interfaces.Reposetory.Auth;
using ApplicationLayer.Interfaces.Reposetory.TaskRepo;
using ApplicationLayer.Interfaces.Reposetory.UserRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// User Repo
        /// </summary>
        IUserReposetory _userReposetory { get; }
        /// <summary>
        /// Task Repo
        /// </summary>
        ITaskitemReposetory _taskitemReposetory { get; }

        /// <summary>
        /// Saveing Data in Database
        /// </summary>
        /// <returns>؟؟</returns>
        Task<int> Save();
    }
}
