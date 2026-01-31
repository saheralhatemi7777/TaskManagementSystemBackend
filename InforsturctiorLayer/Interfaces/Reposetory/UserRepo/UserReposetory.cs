using ApplicationLayer.DTOs.UserDtos;
using ApplicationLayer.Interfaces.Reposetory.UserRepo;
using DomenLayer.UserEntity;
using InforsturctiorLayer.DbContextFolder;
using InforsturctiorLayer.Interfaces.Reposetory.BaseRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforsturctiorLayer.Interfaces.Reposetory.UserRepo
{
    public class UserReposetory : BaseReposetory<Users>,IUserReposetory
    {
        public UserReposetory(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Users>> GetAllUserWithFilteringData(FilteringData filtering)
        {
            var Query = _context.users.AsQueryable();
            //فلترة
            if (!string.IsNullOrEmpty(filtering.UserName))Query =Query.Where(u=>u.Name.Contains(filtering.UserName));
            if (!string.IsNullOrEmpty(filtering.Email)) Query = Query.Where(u => u.Email.Contains(filtering.Email));
            if (filtering.CreatedAt.HasValue)Query = Query.Where(u => u.CreateAt.Date == filtering.CreatedAt.Value.Date);
            //==
            return await Query.ToListAsync();

        }
    }
}
