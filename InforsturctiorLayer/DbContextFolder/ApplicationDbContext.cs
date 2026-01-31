using DomenLayer.TaskItemEntitys;
using DomenLayer.UserEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforsturctiorLayer.DbContextFolder
{
    public class ApplicationDbContext:DbContext
    {

        public DbSet<Users> users {  get; set; }
        public DbSet<TaskItem> taskItems { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {


            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);

            ///RealshonShips With User Table
            modelBuilder.Entity<TaskItem>()
                .HasOne(t=>t.users)
                .WithMany(t=>t.TaskItems)
                .HasForeignKey(t=>t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

         }
        }
    }
