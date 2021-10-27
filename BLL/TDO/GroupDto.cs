namespace BLL.TDO
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public UserDto User { get; set; }
        public string UserId { get; set; }
    }
}
