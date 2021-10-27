using DAL.Context;
using DAL.Interfaces;
using DAL.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GroupRepository : IRepository<Group>
    {
        private readonly ApplicationContext _applicationContext;

        public GroupRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }


        public async Task<int> Add(Group entity)
        {
            var group = _applicationContext.Groups.AddAsync(entity); //.AddAsync(entity);
            await _applicationContext.SaveChangesAsync();
            return entity.Id;
        }

        public List<Group> GetAll() => _applicationContext.Groups.AsNoTracking().ToList();
        //_applicationContext.Groups.AsNoTracking().Include(i => i.User).ToList();

        public async Task<Group> GetById(int id)
        {
            return await _applicationContext.Groups.Where(i => i.Id == id).FirstAsync();
        }

        public async Task Remove(int id)
        {
            var entity = _applicationContext.Groups.First(i => i.Id == id);
            _applicationContext.Groups.Remove(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task Update(Group entity)
        {
            _applicationContext.Groups.Update(entity);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
