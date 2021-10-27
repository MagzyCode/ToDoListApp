using BLL.Interfaces;
using DAL.ExtensionServices;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Services.Extensions
{
    public static class BllServices
    {
        public static void AddBllServices(this IServiceCollection service, string connectionString)
        {

            service.AddDalServices(connectionString);
            service.AddTransient<IIssueService, IssueService>();
            service.AddTransient<IChecklistService, ChecklistService>();
            service.AddTransient<IGroupService, GroupService>();
            service.AddTransient<IUserService, UserService>();
        }
    }
}
