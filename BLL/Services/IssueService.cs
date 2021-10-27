using BLL.Interfaces;
using BLL.Mapping;
using BLL.TDO;
using DAL.Interfaces;
using DAL.Tables;
using DAL.Tables.Users;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class IssueService : IIssueService
    {
        private readonly IRepository<Issue> _issueRepository;
        private readonly UserManager<User> _userManager;

        public IssueService(IRepository<Issue> repository, UserManager<User> userManager)
        {
            _issueRepository = repository;
            _userManager = userManager;
        }


        public async Task<int> Add(IssueDto entity)
        {
            var model = Mapper.FromDto(entity);
            model.User = await _userManager.FindByIdAsync(entity.User?.Id ?? entity.UserId);
            return await _issueRepository.Add(model);
        }

        public IssueDto CreateIssue(string id, string issueText)
        {
            var user = _userManager.Users.FirstOrDefault(i => i.Id == id);
            var userDto = TypeAdapter.Adapt<UserDto>(user);
            var issue = new IssueDto()
            {
                IssueText = issueText,
                UserId = userDto.Id,
                User = userDto,
                CreationDate = DateTime.Now,
                IncludeMyDay = true
            };
            return issue;

        }

        public List<IssueDto> GetAll()
        {
            var list = _issueRepository.GetAll();
            var listOfDto = new List<IssueDto>();
            foreach (var item in list)
            {
                var issueDto = TypeAdapter.Adapt<IssueDto>(item);
                listOfDto.Add(issueDto);
            }
            return listOfDto;
        }

        public async Task<IssueDto> GetById(int id)
        {
            var issue = await _issueRepository.GetById(id);
            var result = Mapper.ToDto(issue);
            return result;
        }

        public List<IssueDto> GetMyDayList()
        {
            var entities = _issueRepository.GetAll().Where(i => i.IncludeMyDay).ToList();
            return TypeAdapter.Adapt<List<IssueDto>>(entities);
        }

        public List<IssueDto> GetMyDayList(UserDto userDto)
        {
            var entities = _issueRepository.GetAll().Where(i => i.IncludeMyDay).ToList();
            var entityUser = TypeAdapter.Adapt<User>(userDto);
            foreach (var item in entities)
            {
                if (item.UserId == userDto.Id)
                    item.User = entityUser; 
            }
            return TypeAdapter.Adapt<List<IssueDto>>(entities);
        }

        public async Task Remove(int id) => await _issueRepository.Remove(id);

        public async Task Update(IssueDto entity) 
        {
            var issueEntity = TypeAdapter.Adapt<Issue>(entity);  
            await _issueRepository.Update(issueEntity);
        }
    }
}
