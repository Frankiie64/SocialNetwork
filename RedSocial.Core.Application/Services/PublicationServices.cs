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
    public class PublicationServices : GenericService<SavePublicationVM, PublicationVM, Publications>, IPublicationService
    {
        private readonly IPublicationRepository _publicationRepo;
        private readonly IMapper _mapper;
        private readonly UserVM user;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public PublicationServices(IPublicationRepository publicationRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(publicationRepo, mapper)
        {
            _publicationRepo = publicationRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext.Session.Get<UserVM>("user");

        }
        public override async Task<SavePublicationVM> CreateAsync(SavePublicationVM vm)
        {
            vm.UserId = user.Id;

            return await base.CreateAsync(vm);
        }
        public async Task<List<PublicationVM>> GetAllViewModelWithInclude()
        {
            var userList = await _publicationRepo.GetAllWithIncludeAsync(new List<string> { "Owner", "Coments" });

            return userList.OrderByDescending(p=>p.Creadted).Where(p => p.UserId == user.Id).Select(vm => new PublicationVM
            {
                id = vm.Id,
                Title = vm.Title,
                UrlPhoto = vm.UrlPhoto,
                UserId = vm.UserId,
                Owner = _mapper.Map<UserVM>(vm.Owner),
                Creadted = vm.Creadted,
                Coments = _mapper.Map<List<CommentVM>>(vm.Coments)


            }).ToList();

        }
    }
}
