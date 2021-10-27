namespace BLL.TDO
{
    public class ChecklistDto
    {
        public int Id { get; set; }
        public string ListName { get; set; }
        public int? GroupId { get; set; }
        public GroupDto Group { get; set; }
        public string UserId { get; set; }
        public UserDto User { get; set; }

        public override string ToString() => ListName;
    }
}
