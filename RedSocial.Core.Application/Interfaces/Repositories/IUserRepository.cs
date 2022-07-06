using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Repositories
{
   public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> LoginAsync(LoginVM vm);
        Task<User> FindUserAsync(SaveUserVM vm);
    }
}
