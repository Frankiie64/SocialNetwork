using Microsoft.AspNetCore.Mvc;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.Friend;
using RedSocial.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.RedSocial.Middlewares;

namespace WebApp.RedSocial.Controllers
{
    public class FriendsController : Controller
    {
        private readonly IFriendsService _service;
        private readonly IUserService _serviceUser;
        private readonly ValidSession _session;

        public FriendsController(IFriendsService service, ValidSession session, IUserService serviceUser)
        {
            _service = service;
            _session = session;
            _serviceUser = serviceUser;

        }
        public async Task<IActionResult> Index()
        {
            if (!_session.HaveUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            ViewData["ModelError"] = "";
            return View(await _service.GetAllViewModelAsync());
        }

        public async Task<IActionResult> AddFriend(SaveUserVM vm)
        {
            if (!_session.HaveUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            ViewData["ModelError"] = "";

            UserVM user = await _serviceUser.validUser(vm);

            if (user == null)
            {
                ViewData["ModelError"] = "Este usuario no existe.";
                return View("Index", await _service.GetAllViewModelAsync());
            }


            SaveFriendVM friend = new();
            friend.FriendId = user.Id;

            SaveFriendVM result = await _service.CreateAsync(friend);

            if (result == null)
            {
                ViewData["ModelError"] = "Usuario invalido.";
                return View("Index", await _service.GetAllViewModelAsync());
            }

            return RedirectToRoute(new { controller = "Friends", action = "Index" });
        }
        public async Task<IActionResult> delete(int id)
        {
            if (!_session.HaveUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            ViewData["ModelError"] = "";

            await _service.DeleteAsync(id);

            return RedirectToRoute(new { controller = "Friends", action = "Index" });
        }
    }
}
