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
    public class GroupService : IGroupService
    {
        private readonly IRepository<Group> _groupRepository;
        private readonly UserManager<User> _userManager;

        public GroupService(IRepository<Group> repository, UserManager<User> userManager)
        {
            _groupRepository = repository;
            _userManager = userManager;
        }

        public async Task<int> Add(GroupDto entity)
        {
            var group = Mapper.FromDto(entity);
            group.User = await _userManager.FindByIdAsync(entity.UserId);
            return await _groupRepository.Add(group);
        }
            
            

        

        public List<GroupDto> GetAll()
        {
            var list = _groupRepository.GetAll();
            var listOfDto = new List<GroupDto>();
            foreach (var item in list)
                listOfDto.Add(Mapper.ToDto(item));
            return listOfDto;
        }

        public async Task<GroupDto> GetById(int id)
        {
            var group = await _groupRepository.GetById(id);
            var result = Mapper.ToDto(group);
            return result;
        }

        public async Task Remove(int id) => await _groupRepository.Remove(id);

        public async Task Update(GroupDto entity) => await _groupRepository.Update(Mapper.FromDto(entity));

        public GroupDto CreateGroup(string userId, string groupName)
        {
            var user = _userManager.Users.FirstOrDefault(i => i.Id == userId);
            var userDto = TypeAdapter.Adapt<UserDto>(user);
            var group = new GroupDto()
            {
                GroupName = groupName,
                User = userDto,
                UserId = userDto.Id
            };
            return group;
        }
    }
}
