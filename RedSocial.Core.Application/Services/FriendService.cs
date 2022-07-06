using AutoMapper;
using Microsoft.AspNetCore.Http;
using RedSocial.Core.Application.Helper;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.Friend;
using RedSocial.Core.Application.ViewModels.Publication;
using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Services
{
    public class FriendService : GenericService<SaveFriendVM,FriendVM,Friends>, IFriendsService
    {
        private readonly IFriendsRepository _friendsRepo;
        private readonly IUserRepository _userRepo;
        private readonly IPublicationRepository _PublicationRepo;
        private readonly IMapper _mapper;
        private readonly UserVM user;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FriendService(IFriendsRepository friendsRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserRepository userRepo, IPublicationRepository PublicationRepo) : base(friendsRepo, mapper)
        {
            _friendsRepo = friendsRepo;
            _userRepo = userRepo;
            _PublicationRepo = PublicationRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext.Session.Get<UserVM>("user");
        }

        public override async Task<SaveFriendVM> CreateAsync(SaveFriendVM vm)
        {
            vm.UserId = user.Id;

           List<Friends> friends = await _friendsRepo.GetAllWithIncludeAsync(new List<string> { "User" });

            var RelationExist = friends.Where(p => p.FriendId == vm.FriendId && p.User.Id == vm.UserId).FirstOrDefault();

            if (RelationExist != null || user.Id == vm.FriendId)
            {
                return null;
            }

            await base.CreateAsync(vm);

            SaveFriendVM save = new();

            save.UserId = vm.FriendId;
            save.FriendId = vm.UserId;

            return await base.CreateAsync(save);
        }
        public override async Task<bool> DeleteAsync(int id)
        {
            Friends friend = await _friendsRepo.GetByIdAsync(id);


            var List = await _friendsRepo.GetAllWithIncludeAsync(new List<string> { "User" });

            Friends item = List.Where(f => f.UserId == friend.FriendId && f.FriendId == user.Id).FirstOrDefault();

            await _friendsRepo.DeleteAsync(item);

            return await _friendsRepo.DeleteAsync(friend);
        }
        public override async Task<List<FriendVM>> GetAllViewModelAsync()
        {
            var Friends = await _friendsRepo.GetAllWithIncludeAsync(new List<string> { "User" });

            Friends = Friends.Where(f => f.UserId == user.Id).ToList();

            List<FriendVM> ListMappped = _mapper.Map<List<FriendVM>>(Friends);
            List<PublicationVM> listPublication = _mapper.Map<List<PublicationVM>>(await _PublicationRepo.GetAllWithIncludeAsync(new List<string> { "Owner", "Coments" }));

            foreach (FriendVM item in ListMappped)
            {
                item.friend = _mapper.Map<UserVM>(await _userRepo.GetByIdAsync(item.FriendId));                                 
                item.ListPublicationFriend = listPublication.OrderByDescending(p => p.Creadted).Where(p => p.UserId == item.FriendId).ToList();                
            }

            return ListMappped;
        }
    }
}
