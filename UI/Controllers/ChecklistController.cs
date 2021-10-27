using BLL.Interfaces;
using BLL.TDO;
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
    public class ChecklistController : Controller
    {
        private readonly IChecklistService _checklistService;
        private readonly IIssueService _issueService;

        public ChecklistController(IChecklistService checklistService, IIssueService issueService)
        {
            _checklistService = checklistService;
            _issueService = issueService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllChecklists(string userId)
        {
            var allChecklists = _checklistService.GetAll();
            var userChecklists = allChecklists.Where(i => i.UserId == userId).ToList();
            // UserDto userDto = new() { Id = userId };
            UserViewModel userViewModel = new() { Id = userId };
            ChecklistListViewModel checklistViewModel = new() { User = userViewModel, Checklists = userChecklists };
            return View("/Views/Checklist/AllChecklistList.cshtml", checklistViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllChecklists(IFormCollection collection)
        {
            var checklist = _checklistService.CreateChecklist(collection["userId"], collection["checklistName"]);
            await _checklistService.Add(checklist);
            var viewModelUser = TypeAdapter.Adapt<UserViewModel>(checklist.User);
            var list = _checklistService.GetAll().Where(i => i.UserId == viewModelUser.Id).ToList();
            var checklistsViewModel = new ChecklistListViewModel()
            {
                User = viewModelUser,
                Checklists = list
            };
            return View("/Views/Checklist/AllChecklistList.cshtml", checklistsViewModel);
        }

        [HttpGet]
        public IActionResult ShowChecklistIssues(int id, string userId)
        {
            var checklist = _checklistService.GetAll().Find(i => i.Id == id);
            var neededIssues = _issueService.GetAll().Where(i => i.UserId == userId && i.ChecklistId == id).ToList();
            var result = new ChecklistIssuesListViewModel()
            {
                ChecklistName = checklist.ListName,
                Issues = neededIssues,
            };
            return View("/Views/Checklist/ChecklistIssuesList.cshtml", result);
        }


    }
}
