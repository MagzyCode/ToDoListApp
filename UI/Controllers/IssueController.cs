using BLL.Interfaces;
using BLL.Mapping;
using BLL.TDO;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UI.ViewModel;

namespace UI.Controllers
{
    public class IssueController : Controller
    {
        private readonly IIssueService _service;
        private readonly IChecklistService _checklistService;

        public IssueController(IIssueService service, IChecklistService checklistService)
        {
            _service = service;
            _checklistService = checklistService;
        }


        [HttpGet]
        public IActionResult MyDayList(UserDto user)
        {
            var list = _service.GetMyDayList(user);
            var neededIssues = list.Where(i => i.UserId == user.Id && i.IncludeMyDay).
                ToList();
            var viewModelUser = TypeAdapter.Adapt<UserViewModel>(user);
            IssueListViewModel model = new() { Issues = neededIssues, User = viewModelUser };
            return View("/Views/Issue/MyDayList.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> MyDayList(IFormCollection collection)
        {
            var issue = _service.CreateIssue(collection["userId"], collection["issueText"]);
            await _service.Add(issue);
            var viewModelUser = TypeAdapter.Adapt<UserViewModel>(issue.User);
            var list = _service.GetMyDayList().Where(i => i.UserId == viewModelUser.Id).ToList();
            var listIssueViewModel = new IssueListViewModel()
            {
                User = viewModelUser,
                Issues = list,
            };
            return View(listIssueViewModel);
        }

        public IActionResult ReturnToMyDayList(DetailsViewModel viewModel)
        {
            var user = _service.GetAll().Where(i => i.Id == viewModel.Id).FirstOrDefault();
            var userId = user?.UserId;
            return RedirectToAction("MyDayList", "Issue", new UserDto() { Id = userId});
        }

        [HttpGet]
        public IActionResult GetMyDayList(string userId)
        {
            var userDto = new UserDto() { Id = userId };
            return MyDayList(userDto);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _service.GetById(id);
            return View(result);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var issue = _service.GetAll().Find(i => i.Id == id);
            var checklists = _checklistService.GetAll();
            var issueViewModel = TypeAdapter.Adapt<DetailsViewModel>(issue);
            var currentChecklistName = _checklistService.GetAll().Find(i => i.Id == issue.ChecklistId)?.ListName;
            issueViewModel.SelectedChecklist = currentChecklistName;
            issueViewModel.Checklists = checklists;
            return View(issueViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Details(DetailsViewModel model)
        {
            var issue = TypeAdapter.Adapt<IssueDto>(model);
            //  var path = await SaveFile(model.AttachedFile);
            var dbDtoIssue = _service.GetAll().Where(i => i.Id == issue.Id).FirstOrDefault();
            var issueChecklist = _checklistService
                .GetAll()
                .Find(i => i.UserId == dbDtoIssue.UserId && i.ListName == model.SelectedChecklist);
            issue.UserId = dbDtoIssue.UserId;
            // issue.AttachedFile = path;
            issue.ChecklistId = dbDtoIssue.ChecklistId ?? issueChecklist?.Id;


            await _service.Update(issue);
            var userDto = new UserDto() { Id = dbDtoIssue.UserId };
            // var str = dbDtoIssue.UserId
            // return View("/Views/Issue/MyDayList.cshtml", userDto);
            /// return RedirectToAction("GetMyDayList", "Issue", dbDtoIssue.UserId);
            return GetMyDayList(dbDtoIssue.UserId);
        }

        [HttpGet]
        public IActionResult AddIssue()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PlannedTaskList(string userId)
        {
            var userDto = new UserDto() { Id = userId };
            var list = _service.GetAll();
            var neededIssues = list
                .Where(i => i.UserId == userId && i.CompletionDate.HasValue)
                .OrderBy(i => i.CompletionDate)
                .ToList();
            var viewModelUser = TypeAdapter.Adapt<UserViewModel>(userDto);
            PlannedTaskListViewModel model = new() { Issues = neededIssues, User = viewModelUser };
            return View("/Views/Issue/PlannedTaskList.cshtml", model);
        }

        [HttpGet]
        public IActionResult AllIssueList(string userId)
        {
            var userDto = new UserDto() { Id = userId };
            var list = _service.GetAll();
            var neededIssues = list
                .Where(i => i.UserId == userId)
                .ToList();
            var viewModelUser = TypeAdapter.Adapt<UserViewModel>(userDto);
            AllIssueListViewModel model = new() { Issues = neededIssues, User = viewModelUser };
            return View("/Views/Issue/AllIssueList.cshtml", model);
        }
        
        public async Task<IActionResult> DeleteIssue(int id)
        {
            var issue = _service.GetAll().Find(i => i.Id == id);
            
            await _service.Remove(id);
            if (issue.Repetition > 0 && issue.CompletionDate != null)
            {
                await _service.Add(new IssueDto()
                {
                    IssueText = issue.IssueText,
                    CreationDate = issue.CreationDate.AddDays(issue.Repetition.Value),
                    Note = issue.Note,
                    Repetition = issue.Repetition,
                    ReminderDate = issue.ReminderDate,
                    IncludeMyDay = issue.IncludeMyDay,
                    AttachedFile = issue.AttachedFile,
                    UserId = issue.UserId,
                    ChecklistId = issue.ChecklistId,
                    Checklist = issue.Checklist,
                    User = issue.User
                });
            }
            return GetMyDayList(issue.UserId);
        }

        private async Task<string> SaveFile(IFormFile image)
        {
            string path = "";
            if (image != null)
            {
                path = "/files/Furniture/" + image.FileName;

                using var fileStream = new FileStream(@"F:\СУХАЧ\Меша 6 Сем\Курсовая\ToDoList\UI\wwwroot\" + path, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }
            else
                path = "/files/No_image.jpg";

            return path;
        }

        private byte[] GetByteArrayFromImage(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }
    }
}
