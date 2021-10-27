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
    public class IssueRepository : IRepository<Issue>
    {
        private readonly ApplicationContext _applicationContext;

        public IssueRepository (ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<int> Add(Issue entity)
        {
            await _applicationContext.Issues.AddAsync(entity);
            await _applicationContext.SaveChangesAsync();
            return entity.Id;
        }

        public List<Issue> GetAll() => _applicationContext.Issues.AsNoTracking().ToList(); // .Include(i => i.)

        public async Task<Issue> GetById(int id)
        {
            return await _applicationContext.Issues.Where(i => i.Id == id).FirstAsync();
        }

        public async Task Remove(int id)
        {
            var entity = await _applicationContext.Issues.FirstOrDefaultAsync(i => i.Id == id);
            _applicationContext.Issues.Remove(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task Update(Issue entity)
        {
            _applicationContext.Issues.Update(entity);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
