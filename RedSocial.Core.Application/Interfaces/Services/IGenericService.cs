using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel, Model>
        where SaveViewModel : class
        where ViewModel : class
        where Model : class
    {
       Task<bool> UpdateAsync(SaveViewModel vm, int id);

        Task<SaveViewModel> CreateAsync(SaveViewModel vm);

        Task<bool> DeleteAsync(int id);

        Task<SaveViewModel> GetByIdSaveViewModelAsync(int id);

        Task<List<ViewModel>> GetAllViewModelAsync();
    }
}
