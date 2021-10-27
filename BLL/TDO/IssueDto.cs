using System;

namespace BLL.TDO
{
    public class IssueDto
    {
        public int Id { get; set; }
        public string IssueText { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string Note { get; set; }
        public int? Repetition { get; set; } = 0;
        public DateTime? ReminderDate { get; set; }
        public bool IncludeMyDay { get; set; } = false;
        public DateTime? CompletionDate { get; set; }
        public string AttachedFile { get; set; }
        public string UserId { get; set; }
        public int? ChecklistId { get; set; }
        public ChecklistDto Checklist { get; set; }
        public UserDto User { get; set; }
    }
}
