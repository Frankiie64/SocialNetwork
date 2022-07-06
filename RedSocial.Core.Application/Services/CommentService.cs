using AutoMapper;
using Microsoft.AspNetCore.Http;
using RedSocial.Core.Application.Helper;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.Comments;
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
   public class CommentService : GenericService<SaveCommentVM, CommentVM, Comments>, ICommentService
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IPublicationRepository _publicationRepo;
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _mapper;
        private readonly UserVM user;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentService(IPublicationRepository publicationRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserRepository UserRepository,ICommentRepository commentRepo) : base(commentRepo, mapper)
        {
            _commentRepo = commentRepo;
            _UserRepository = UserRepository;
            _publicationRepo = publicationRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext.Session.Get<UserVM>("user");
        }

        public async Task<PublicationVM> GetByIdVM(int id)
        {
            var userList = await _publicationRepo.GetAllWithIncludeAsync(new List<string> { "Owner", "Coments" });
            List<User> owners = await _UserRepository.GetAllWithIncludeAsync(new List<string> { "Publications", "comments", "Friends" });           

            PublicationVM item = _mapper.Map<PublicationVM>(userList.Where(p => p.Id == id).FirstOrDefault());

            foreach (User owner in owners)
            {
                foreach (CommentVM comments in item.Coments)
                {
                    if (comments.UserId == owner.Id)
                    {
                        comments.Owner = _mapper.Map<UserVM>(owner);
                    }
                }

            }


            item.Coments.OrderByDescending(c => c.Creadted);

            return item;
        }

        public override async Task<SaveCommentVM> CreateAsync(SaveCommentVM vm)
        {
            vm.UserId = user.Id;

            return await base.CreateAsync(vm);
        }
       
    }
}
