using AutoMapper;
using RedSocial.Core.Application.Dtos.Email;
using RedSocial.Core.Application.Helper;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace RedSocial.Core.Application.Services
{
    public class UserServices: GenericService<SaveUserVM,UserVM,User>, IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public UserServices(IUserRepository userRepo, IMapper mapper, IEmailService emailService) : base(userRepo,mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _emailService = emailService;

        }
        public async Task<UserVM> Login(LoginVM vm)
        {
            User user = await _userRepo.LoginAsync(vm);

            if (user == null)
            {
                return null;
            }

            UserVM userVm = _mapper.Map<UserVM>(user);
            return userVm;
        }
        public override async Task<SaveUserVM> CreateAsync(SaveUserVM vm)
        {
            SaveUserVM userVm = await base.CreateAsync(vm);

            
            await _emailService.SendAsync(new EmailRequest
            {
                To = userVm.Email,
                Body = $"<a href='https://localhost:44314/User/ValidEmail/{userVm.Id}'>Confirmar cuenta</a>",
                Subject = "Confirmacion de Email"
            });
            
            return userVm;
        }
        public async Task<bool> updatePass(UserVM vm,int id)
        {
            vm.Password = GeneratePass.NewPassword();


            await _emailService.SendAsync(new EmailRequest
            {
                To = vm.Email,
                Body = $"Saludos, tu nueva contraseña es {vm.Password}",
                Subject = "Nueva contraseña"
            });

           vm.Password = EncryptedPass.ComputeSha256Hash(vm.Password);

            SaveUserVM user = _mapper.Map<SaveUserVM>(vm);

            return await base.UpdateAsync(user,id);
        }
        public async Task<UserVM> validUser(SaveUserVM vm)
        {
            User user = await _userRepo.FindUserAsync(vm);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserVM>(user);
            
        }       
       
    }
}
