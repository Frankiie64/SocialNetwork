using AutoMapper;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Services
{
    public class GenericService<SaveViewModel,ViewModel,Model> : IGenericService<SaveViewModel, ViewModel, Model>
        where SaveViewModel : class
        where ViewModel : class
        where Model : class
    {
        private readonly IGenericRepository<Model> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Model> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<bool> UpdateAsync(SaveViewModel vm, int id)
        {
            Model entity = _mapper.Map<Model>(vm);

            return await _repository.UpdateAsync(entity, id);
        }

        public virtual async Task<SaveViewModel> CreateAsync(SaveViewModel vm)
        {
            Model entity = _mapper.Map<Model>(vm);

            entity = await _repository.CreateAsync(entity);

            SaveViewModel saveViewModel = _mapper.Map<SaveViewModel>(entity);
            return saveViewModel;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var model = await _repository.GetByIdAsync(id);
            return await _repository.DeleteAsync(model);
        }

        public virtual async Task<SaveViewModel> GetByIdSaveViewModelAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            SaveViewModel vm = _mapper.Map<SaveViewModel>(entity);
            return vm;
        }

        public virtual async Task<List<ViewModel>> GetAllViewModelAsync()
        {
            var entityList = await _repository.GetAllAsync();

            return _mapper.Map<List<ViewModel>>(entityList);
        }
    }
}
