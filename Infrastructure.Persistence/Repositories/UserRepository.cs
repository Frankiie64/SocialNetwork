using Microsoft.EntityFrameworkCore;
using RedSocial.Core.Application.Helper;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Core.Domain.Entities;
using RedSocial.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public override async Task<User> CreateAsync(User entity)
        {
            entity.Password = EncryptedPass.ComputeSha256Hash(entity.Password);
            return await base.CreateAsync(entity);
        } 

        public async Task<User> LoginAsync(LoginVM vm)
        {
            string pass = EncryptedPass.ComputeSha256Hash(vm.Password);

            var item = await _db.Set<User>().
                FirstOrDefaultAsync(user => user.Username == vm.Username && user.Password == pass);

            return item;
        }
        public async Task<User> FindUserAsync(SaveUserVM vm)
        {
            if (!string.IsNullOrWhiteSpace(vm.Username))
            {
                var item = await _db.Set<User>().
                    FirstOrDefaultAsync(user => user.Username.Trim().ToLower() == vm.Username.Trim().ToLower());

                if (item != null)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
