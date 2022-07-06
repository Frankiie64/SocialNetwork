using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserVM, UserVM, User>
    {
        Task<UserVM> Login(LoginVM vm);
        Task<UserVM> validUser(SaveUserVM vm);
        Task<bool> updatePass(UserVM vm, int id);

    }
}
