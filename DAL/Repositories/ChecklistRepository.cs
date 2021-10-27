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
    public class ChecklistRepository : IRepository<Checklist>
    {
        private readonly ApplicationContext _applicationContext;

        public ChecklistRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<int> Add(Checklist entity)
        {
            var checklist = await _applicationContext.Checklists.AddAsync(entity);
            await _applicationContext.SaveChangesAsync();
            return checklist.Entity.Id;
        }

        public List<Checklist> GetAll() => _applicationContext.Checklists.ToList();
        //_applicationContext.Checklists.Include(i => i.Group).ToList();

        public async Task<Checklist> GetById(int id)
        {
            return await _applicationContext.Checklists.Where(i => i.Id == id).FirstAsync();
        }

        public async Task Remove(int id)
        {
            var entity = await _applicationContext.Checklists.FirstAsync(i => i.Id == id);
            _applicationContext.Checklists.Remove(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task Update(Checklist entity)
        {
            _applicationContext.Checklists.Update(entity);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
