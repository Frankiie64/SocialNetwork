using RedSocial.Core.Application.ViewModels.Friend;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IFriendsService : IGenericService<SaveFriendVM, FriendVM, Friends>
    {
    }
}
