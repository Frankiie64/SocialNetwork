using Microsoft.EntityFrameworkCore;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedSocial.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly ApplicationDbContext _db;

        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public virtual async Task<Entity> CreateAsync(Entity entity)
        {
            await _db.Set<Entity>().AddAsync(entity);
            await Save();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(Entity entity)
        {
            _db.Set<Entity>().Remove(entity);
            return await Save();
        }
        public virtual async Task<bool> UpdateAsync(Entity entity, int id)
        {
            Entity entry = await _db.Set<Entity>().FindAsync(id);
            _db.Entry(entry).CurrentValues.SetValues(entity);
            return await Save();
        }

        public virtual async Task<List<Entity>> GetAllAsync()
        {
            return await _db.Set<Entity>().ToListAsync();
        }
        public virtual async Task<List<Entity>> GetAllWithIncludeAsync(List<string> properties)
        {
            var query = _db.Set<Entity>().AsQueryable();

            foreach (string property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }
        public virtual async Task<Entity> GetByIdAsync(int id)
        {
            return await _db.Set<Entity>().FindAsync(id);
        }
        private async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
