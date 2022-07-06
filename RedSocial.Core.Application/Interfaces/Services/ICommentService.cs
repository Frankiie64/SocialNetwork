using RedSocial.Core.Application.ViewModels.Comments;
using RedSocial.Core.Application.ViewModels.Publication;
using RedSocial.Core.Domain.Entities;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface ICommentService: IGenericService<SaveCommentVM, CommentVM, Comments>
    {
        Task<PublicationVM> GetByIdVM(int id);
    }
}
