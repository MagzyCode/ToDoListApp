using BLL.TDO;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService : IService<UserDto>
    {
        public Task<UserDto> PasswordSignInAsync(UserDto user);
        public Task SignIn(UserDto user);
        public Task SignOut();
    }
}
