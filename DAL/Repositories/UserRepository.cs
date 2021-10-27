using DAL.Context;
using DAL.Interfaces;
using DAL.Tables.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly ApplicationContext _applicationContext;
        private readonly UserManager<User> _userManager;

        public UserRepository(ApplicationContext applicationContext,
            UserManager<User> userManager)
        {
            _applicationContext = applicationContext;
            _userManager = userManager;
        }

        public async Task<int> Add(User entity)
        {
            //entity.UserName = (string)entity.Email.Clone();
            var result = await _userManager.CreateAsync(entity);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    var g = error;
                    // ModelState.AddModelError(string.Empty, error.Description);
                }
            }


            return 0;
            //var user = _applicationContext.Users.Add(entity);
            //_applicationContext.SaveChanges();
            //return user.Entity.Id;
            // return default;
        }



        public List<User> GetAll() => default; /*_applicationContext.Users.ToList();*/

        public Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(int id)
        {
            //var entity = _applicationContext.Users.First(i => i.Id == id);
            //_applicationContext.Users.Remove(entity);
            //_applicationContext.SaveChanges();
        }

        public async Task Update(User entity)
        {
            //var user = _applicationContext.Users.Update(entity);
            //_applicationContext.SaveChanges();
        }
    }
}
