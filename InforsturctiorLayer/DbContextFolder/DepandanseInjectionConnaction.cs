using ApplicationLayer.Features.User.Profiles;
using ApplicationLayer.Interfaces.Reposetory.Auth;
using ApplicationLayer.Interfaces.Reposetory.BaseRepo;
using ApplicationLayer.Interfaces.Reposetory.TaskRepo;
using ApplicationLayer.Interfaces.Reposetory.UserRepo;
using ApplicationLayer.Interfaces.UnitOfWork;
using AutoMapper;
using InforsturctiorLayer.Interfaces.Reposetory.AuthRepo;
using InforsturctiorLayer.Interfaces.Reposetory.BaseRepo;
using InforsturctiorLayer.Interfaces.Reposetory.TaskItemRepo;
using InforsturctiorLayer.Interfaces.Reposetory.UserRepo;
using InforsturctiorLayer.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InforsturctiorLayer.DbContextFolder
{
    public static class DepandanseInjectionConnaction
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // ==========================
            // 1️⃣ Repositories
            // ==========================
            services.AddScoped<IUserReposetory, UserReposetory>();
            services.AddScoped<ITaskitemReposetory, TaskItemReposetory>();
            services.AddScoped(typeof(IBaseReposetory<>), typeof(BaseReposetory<>));
            services.AddScoped<IAuthReposetory, AuthReposetory>();

            // ==========================
            // 2️⃣ Unit of Work
            // ==========================
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // ==========================
            // 3️⃣ AutoMapper
            // ==========================
            services.AddAutoMapper(Assembly.Load("ApplicationLayer"));

            services.AddAutoMapper(typeof(ProfileUser).Assembly);

            // ==========================
            // 4️⃣ MediatR
            // ==========================
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.Load("ApplicationLayer"));
            });

            return services;
        }
    }
}
