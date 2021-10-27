using BLL.TDO;

namespace BLL.Interfaces
{
    public interface IGroupService : IService<GroupDto>
    {
        public GroupDto CreateGroup(string userId, string groupName);
    }
}
