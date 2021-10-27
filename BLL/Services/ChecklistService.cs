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
    public class ChecklistService : IChecklistService
    {
        private readonly IRepository<Checklist> _checklistRepository;
        private readonly UserManager<User> _userManager;

        public ChecklistService(IRepository<Checklist> repository, UserManager<User> userManager)
        {
            _checklistRepository = repository;
            _userManager = userManager;
        }

        public async Task<int> Add(ChecklistDto entity)
        {
            var checklist = Mapper.FromDto(entity);
            checklist.User = await _userManager.FindByIdAsync(entity.UserId);
            return await _checklistRepository.Add(checklist);
        }
            
          

        public List<ChecklistDto> GetAll()
        {
            var list = _checklistRepository.GetAll();
            var listOfDto = new List<ChecklistDto>();
            foreach (var item in list)
                listOfDto.Add(Mapper.ToDto(item));
            return listOfDto;
        }

        public async Task<ChecklistDto> GetById(int id)
        {
            var checklist = await _checklistRepository.GetById(id);
            var result = Mapper.ToDto(checklist);
            return result;
        }

        public async Task Remove(int id) => await _checklistRepository.Remove(id);

        public async Task Update(ChecklistDto entity) => await _checklistRepository.Update(Mapper.FromDto(entity));

        public ChecklistDto CreateChecklist(string userId, string checklistName)
        {
            var user = _userManager.Users.FirstOrDefault(i => i.Id == userId);
            var userDto = TypeAdapter.Adapt<UserDto>(user);
            var checklist = new ChecklistDto()
            {
                ListName = checklistName,
                User = userDto,
                UserId = userDto.Id
            };
            return checklist;
        }

    }
}
