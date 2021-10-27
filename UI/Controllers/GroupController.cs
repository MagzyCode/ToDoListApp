using BLL.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.ViewModel;

namespace UI.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IChecklistService _checklistService;

        public GroupController(IGroupService groupService, IChecklistService checklistService)
        {
            _groupService = groupService;
            _checklistService = checklistService;
        }

        [HttpGet]
        public IActionResult GetAllGroupsList(string userId)
        {
            var groupList = _groupService.GetAll().Where(i => i.UserId == userId).ToList();
            UserViewModel userViewModel = new() { Id = userId };
            GroupListViewModel groupViewModel = new() { User = userViewModel, Groups = groupList };
            return View("/Views/Group/AllGroupsList.cshtml", groupViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddGroup(IFormCollection collection)
        {
            var group = _groupService.CreateGroup(collection["userId"], collection["groupName"]);
            await _groupService.Add(group);
            var viewModelUser = TypeAdapter.Adapt<UserViewModel>(group.User);
            var list = _groupService.GetAll().Where(i => i.UserId == viewModelUser.Id).ToList();
            var groupViewModel = new GroupListViewModel()
            {
                User = viewModelUser,
                Groups = list
            };
            return View("/Views/Group/AllGroupsList.cshtml", groupViewModel);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ShowGroupChecklists(int id, string userId)
        {
            var group = _groupService.GetAll().Find(i => i.Id == id);
            var neededChecklists = _checklistService.GetAll().Where(i => i.UserId == userId && i.GroupId == id).ToList();
            var userViewModel = new UserViewModel() { Id = userId };
            var result = new GroupChecklistsListViewModel()
            {
                GroupName = group.GroupName,
                Checklists = neededChecklists,
                User = userViewModel
            };
            return View("/Views/Group/GroupChecklistsList.cshtml", result);
        }
    }
}
