using DAL.Context;
using DAL.Interfaces;
using DAL.Repositories;
using DAL.Tables;
using DAL.Tables.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.ExtensionServices
{
    public static class DalServices
    {
        public static void AddDalServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddTransient<IRepository<Issue>, IssueRepository>();
            services.AddTransient<IRepository<Checklist>, ChecklistRepository>();
            services.AddTransient<IRepository<Group>, GroupRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
        }
    }
}
