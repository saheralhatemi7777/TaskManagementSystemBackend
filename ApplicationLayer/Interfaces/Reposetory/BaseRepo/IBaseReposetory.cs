using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces.Reposetory.BaseRepo
{
    public interface IBaseReposetory<T> where T : class
    {
        #region Querys
        
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        #endregion

        #region Command
        Task<T>AddAsync(T item);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T id);
       #endregion

        Task<int> SaveAsync();
    }
}
