using BLL.Interfaces;
using BLL.Mapping;
using BLL.TDO;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UI.ViewModel;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _service;

        public AccountController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TypeAdapter.Adapt<UserDto>(model);
                UserDto user = TypeAdapter.Adapt<UserDto>(model);
                user.Id = Guid.NewGuid().ToString();
                user.UserName += Guid.NewGuid().ToString();
                await _service.Add(user);
                await _service.SignIn(user);
                return RedirectToAction("MyDayList", "Issue", user);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDto userDto = TypeAdapter.Adapt<UserDto>(model);
                userDto = await _service.PasswordSignInAsync(userDto); 
                if (userDto != null)
                    return RedirectToAction("MyDayList", "Issue", userDto);
                else
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _service.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout(int exitIndex = 0)
        {
            await _service.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
