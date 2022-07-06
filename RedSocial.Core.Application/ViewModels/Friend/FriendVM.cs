using RedSocial.Core.Application.ViewModels.Publication;
using RedSocial.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.ViewModels.Friend
{
    public class FriendVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserVM User { get; set; }
        public int FriendId { get; set; }
        public UserVM friend { get; set; }
        public List<PublicationVM> ListPublicationFriend { get; set; }
    }
}
