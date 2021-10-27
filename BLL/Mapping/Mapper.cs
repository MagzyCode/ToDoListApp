using BLL.TDO;
using DAL.Tables;
using DAL.Tables.Users;
using Mapster;

namespace BLL.Mapping
{
    public static class Mapper
    {
        public static UserDto ToDto(User user)
        {
            if (user != null)
            {
                UserDto userDto = new()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                    Gender = user.Gender,
                    DayOfBorn = user.DayOfBorn
                };
                return userDto;
            }
            return default;
            
        }

        public static GroupDto ToDto(Group group)
        {
            if (group != null)
            {
                GroupDto groupDto = new()
                {
                    Id = group.Id,
                    GroupName = group.GroupName,
                    User = ToDto(group.User),
                    UserId = group.UserId
                };
                return groupDto;
            }
            return default;
        }

        public static ChecklistDto ToDto(Checklist checklist)
        {
            if (checklist != null)
            {
                ChecklistDto checklistDto = new ChecklistDto()
                {
                    Id = checklist.Id,
                    ListName = checklist.ListName,
                    Group = ToDto(checklist.Group),
                    User = ToDto(checklist.User),
                    UserId = checklist.UserId,
                    GroupId = checklist.GroupId
                };
                return checklistDto;
            }
            return default;
            
        }

        public static IssueDto ToDto(Issue issue)
        {
            if (issue != null)
            {
                IssueDto issueDto = new()
                {
                    Id = issue.Id,
                    IssueText = issue.IssueText,
                    CreationDate = issue.CreationDate,
                    Note = issue.Note,
                    Repetition = issue.Repetition,
                    ReminderDate = issue.ReminderDate,
                    IncludeMyDay = issue.IncludeMyDay,
                    CompletionDate = issue.CompletionDate,
                    AttachedFile = issue.AttachedFile,
                    UserId = issue.UserId,
                    ChecklistId = issue.ChecklistId,
                    Checklist = ToDto(issue.Checklist),
                    User = ToDto(issue.User)
                };
                return issueDto;
            }
            return default;
        }

        public static User FromDto(UserDto userDto)
        {
            if (userDto != null)
            {
                User user = new()
                {
                    Id = userDto.Id,
                    UserName = userDto.UserName,
                    Email = userDto.Email,
                    Password = userDto.Password,
                    Gender = userDto.Gender,
                    DayOfBorn = userDto.DayOfBorn
                };
                return user;
            }
            return default;
            
        }

        public static Group FromDto(GroupDto groupDto)
        {
            if (groupDto != null)
            {
                Group group = new()
                {
                    Id = groupDto.Id,
                    GroupName = groupDto.GroupName,
                    User = FromDto(groupDto.User),
                    UserId = groupDto.UserId
                };
                return group;
            }
            return default;
        }

        public static Checklist FromDto(ChecklistDto checklistDto)
        {
            if (checklistDto != null)
            {
                Checklist checklist = new()
                {
                    Id = checklistDto.Id,
                    ListName = checklistDto.ListName,
                    Group = FromDto(checklistDto.Group),
                    User = FromDto(checklistDto.User),
                    UserId = checklistDto.UserId,
                    GroupId = checklistDto.GroupId
                };
                return checklist;
            }
            return default;
        }

        public static Issue FromDto(IssueDto issueDto)
        {
            if (issueDto != null)
            {
                Issue issue = new()
                {
                    Id = issueDto.Id,
                    IssueText = issueDto.IssueText,
                    CreationDate = issueDto.CreationDate,
                    Note = issueDto.Note,
                    Repetition = issueDto.Repetition,
                    ReminderDate = issueDto.ReminderDate,
                    IncludeMyDay = issueDto.IncludeMyDay,
                    CompletionDate = issueDto.CompletionDate,
                    AttachedFile = issueDto.AttachedFile,
                    Checklist = TypeAdapter.Adapt<Checklist>(issueDto.Checklist),
                    User = TypeAdapter.Adapt<User>(issueDto.User)
                };
                return issue;
            }
            return default;
           
        }

    }
}
