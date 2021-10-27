using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Mapping;
using BLL.TDO;
using DAL.Interfaces;
using DAL.Tables;
using DAL.Tables.Users;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserService(IRepository<User> repository, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _userRepository = repository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<int> Add(UserDto entity)
        {
            User user = Mapper.FromDto(entity);
            await _userRepository.Add(user);
            return 0;
        }

        public List<UserDto> GetAll()
        {
            var list = _userRepository.GetAll();
            var listOfDto = new List<UserDto>();
            foreach (var item in list)
                listOfDto.Add(Mapper.ToDto(item));
            return listOfDto;
        }

        public async Task Remove(int id) => await _userRepository.Remove(id);

        public async Task<UserDto> PasswordSignInAsync(UserDto user)
        {
            User entityUser = await _userManager.FindByEmailAsync(user.Email);
            if (entityUser == null || entityUser.Password != user.Password)
                return null;
            //TypeAdapter.Adapt<User>(user);
            var userDto = TypeAdapter.Adapt<UserDto>(entityUser);
            if (entityUser?.Password == user?.Password)
                await _signInManager.SignInAsync(entityUser, false);
            return userDto;
        }
        
        public async Task Update(UserDto entity) => await _userRepository.Update(Mapper.FromDto(entity));

        public async Task SignIn(UserDto user)
        {
            User entityUser = await _userManager.FindByEmailAsync(user.Email);
            await _signInManager.SignInAsync(entityUser, false);
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public Task<UserDto> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
