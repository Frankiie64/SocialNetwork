using Microsoft.AspNetCore.Mvc;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.RedSocial.Middlewares;
using RedSocial.Core.Application.Helper;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace WebApp.RedSocial.Controllers
{
    public class UserController : Controller
    {
        private readonly ValidSession _session;
        private readonly IUserService _service;
        public UserController(IUserService service,ValidSession session)
        {
            _service = service;
            _session = session;
        }
        public IActionResult Index()
        {
            if (_session.HaveUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(new LoginVM());
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginVM vm)
        {
            if (_session.HaveUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            UserVM user = await _service.Login(vm);


            if (user != null)
            {
                if (user.Active != false)
                {
                    HttpContext.Session.Set<UserVM>("user", user);
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
                else
                {                  
                    vm.validate = true;
                    ModelState.AddModelError("userValidation", "Debe de activar su cuenta, mediante las especificaciones enviadas a su correo.");
                }
            }
            else
            {
                vm.validate = true;
                ModelState.AddModelError("userValidation", "El usuario o la contraseña es incorrecto.");
            }

            return View(vm);
        }
        public IActionResult Register()
        {
            if (_session.HaveUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(new SaveUserVM());
        }
        [HttpPost]
        public async Task<IActionResult> Register(SaveUserVM vm)
        {
            if (_session.HaveUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.validateUser = false;
                return View(vm);
            }

            UserVM user = await _service.validUser(vm);

            if (user != null)
            {
                vm.validateUser = true;
                ModelState.AddModelError("userValidation", "Este usuario ya existe.");
                return View(vm);

            }
            else
            {

                SaveUserVM item = await _service.CreateAsync (vm);

                if (item.Id != 0)
                {
                    item.UrlPhoto = UploadPhoto.UploadFile(vm.File, "Profiles", item.Id);

                    await _service.UpdateAsync(item, item.Id);
                }

                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
        }
        public IActionResult Logout(LoginVM vm)
        {
          

            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async  Task<IActionResult> ValidEmail(int id)
        {
            if (_session.HaveUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            SaveUserVM saveVM = await _service.GetByIdSaveViewModelAsync(id);

            saveVM.Active = true;

            await _service.UpdateAsync(saveVM, id);

            return View(saveVM);
        }
        public ActionResult ForgetPass()
        {
            if (_session.HaveUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(new ForgetPassVM());
        }

        [HttpPost]
        public async Task<ActionResult> ForgetPass(ForgetPassVM passVM)
        {
            if (_session.HaveUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            SaveUserVM vm = new();

            vm.Username = passVM.Username;
            if (_session.HaveUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            UserVM user = await _service.validUser(vm);

            if (user == null)
            {
                passVM.validateUser = true;
                ModelState.AddModelError("userValidation", $"El usuario {passVM.Username}  no existe.");
                return View(passVM);
            }
            else
            {
                await _service.updatePass(user, user.Id);
                passVM.validateUser = false;
                ModelState.AddModelError("userValidation", $"Revise su Email, le enviamos una contraseña provisional.");
                return View(passVM);
            }

        }
       
    }
}
