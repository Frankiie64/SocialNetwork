using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedSocial.Core.Application.Helper;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.Publication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.RedSocial.Middlewares;
using WebApp.RedSocial.Models;

namespace WebApp.RedSocial.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPublicationService _service;
        private readonly ValidSession _session;

        public HomeController(IPublicationService service, ValidSession session)
        {
            _service = service;
            _session = session;

        }

        public async Task<IActionResult> Index()
        {
            if (!_session.HaveUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(await _service.GetAllViewModelWithInclude());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePublication(SavePublicationVM vm)
        {
            if (!_session.HaveUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            SavePublicationVM savePublication = await _service.CreateAsync(vm);

            if (savePublication.Id != 0 && savePublication != null && vm.File != null)
            {
                savePublication.UrlPhoto = UploadPhoto.UploadFile(vm.File, "Profile", savePublication.Id);

                await _service.UpdateAsync(savePublication,savePublication.Id);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });

        }

        [HttpPost]
        public async Task<IActionResult> EditPublication(SavePublicationVM vm)
        {
            if (!_session.HaveUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            SavePublicationVM publicationVM = await _service.GetByIdSaveViewModelAsync(vm.Id);

            vm.UserId = publicationVM.UserId;
            vm.UrlPhoto = publicationVM.UrlPhoto;
            vm.Creadted = publicationVM.Creadted;
            
            if (vm.File == null)
            {
                await _service.UpdateAsync(vm, vm.Id);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!string.IsNullOrWhiteSpace(publicationVM.UrlPhoto))
            {
                vm.UrlPhoto = UploadPhoto.UploadFile(vm.File, "Profile", vm.Id,  true, publicationVM.UrlPhoto);
            }
            else
            {
                vm.UrlPhoto = UploadPhoto.UploadFile(vm.File, "Profile", vm.Id);
            }
            await _service.UpdateAsync(vm,vm.Id);
            return RedirectToRoute(new { controller = "Home", action = "Index" });

        }
        public async Task<IActionResult> DeletePublication(int id)
        {
            if (!_session.HaveUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            string basePath = $"/Img/Publication/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foreach (FileInfo item in directoryInfo.GetFiles())
                {
                    item.Delete();
                }

                foreach (DirectoryInfo folder in directoryInfo.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }
            await _service.DeleteAsync(id);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
       
    }
}
