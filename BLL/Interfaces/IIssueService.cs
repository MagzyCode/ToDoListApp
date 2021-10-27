using BLL.TDO;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IIssueService : IService<IssueDto>
    {
        public List<IssueDto> GetMyDayList();
        public List<IssueDto> GetMyDayList(UserDto userDto);
        public IssueDto CreateIssue(string id, string issueText);
    }
}
