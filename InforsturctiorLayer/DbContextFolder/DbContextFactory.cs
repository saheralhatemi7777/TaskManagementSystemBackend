using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforsturctiorLayer.DbContextFolder
{
    public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer(
                "Data Source=(localdb)\\ProjectModels;Initial Catalog=TaskDb;Integrated Security=True;", // Disable encryption for LocalDB
                sqlOptions =>
                {
                    // Retry على مشاكل الاتصال المؤقتة
                    sqlOptions.EnableRetryOnFailure(
                     maxRetryCount: 5,
                     maxRetryDelay: TimeSpan.FromSeconds(10),
                     errorNumbersToAdd: null);
                });

            return new ApplicationDbContext(optionsBuilder.Options);

        }
    }
}
