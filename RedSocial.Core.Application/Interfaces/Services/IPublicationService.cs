using RedSocial.Core.Application.ViewModels.Publication;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IPublicationService : IGenericService<SavePublicationVM, PublicationVM, Publications>
    {
        Task<List<PublicationVM>> GetAllViewModelWithInclude();

    }
}
