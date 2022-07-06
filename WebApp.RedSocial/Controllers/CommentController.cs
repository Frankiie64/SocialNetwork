using Microsoft.AspNetCore.Mvc;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.Comments;
using RedSocial.Core.Application.ViewModels.Publication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.RedSocial.Middlewares;

namespace WebApp.RedSocial.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _service;
        private readonly ValidSession _session;

        public CommentController(ICommentService service, ValidSession session)
        {
            _service = service;
            _session = session;

        }

        public async Task<IActionResult> Index(int id)
        {
            if (!_session.HaveUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(await _service.GetByIdVM(id));
        }

        public async Task<IActionResult> AddComent(SaveCommentVM vm)
        {
            if (!_session.HaveUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            SaveCommentVM saveComment = await _service.CreateAsync(vm);

            return RedirectToRoute(new { controller = "Comment", action = "Index", id = vm.PublicationId });

        }
    }
}
