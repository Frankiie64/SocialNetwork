using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        public Task<List<Entity>> GetAllAsync();
        public Task<List<Entity>> GetAllWithIncludeAsync(List<string> properties);
        public Task<Entity> GetByIdAsync(int id);
        public Task<bool> UpdateAsync(Entity entity,int id);
        public Task<bool> DeleteAsync(Entity entity);
        public Task<Entity> CreateAsync(Entity entity);
    }
}
