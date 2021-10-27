using BLL.TDO;

namespace BLL.Interfaces
{
    public interface IChecklistService : IService<ChecklistDto>
    {
        public ChecklistDto CreateChecklist(string userId, string checklistName);
    }
}
