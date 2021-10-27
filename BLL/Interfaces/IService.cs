using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        public Task<int> Add(T entity);
        public Task<T> GetById(int id);
        public List<T> GetAll();
        public Task Update(T entity);
        public Task Remove(int id);

    }
}
