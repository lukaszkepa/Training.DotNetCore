using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Training.DotNetCore.BL.Model;
using Training.DotNetCore.DA.Repositories;

namespace Training.DotNetCore.BL.Services
{
    public class TrainingService : ITrainingService
    {
        private IMapper _mapper;
        private ITrainingRepository _repository;

        public TrainingService(IMapper mapper, ITrainingRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Model.Training> AddAsync(Model.Training training)
        {
            if (training == null)
            {
                throw new ArgumentNullException(nameof(training));
            }

            var model = _mapper.Map<DA.Model.Training>(training);
            var added = await _repository.AddAsync(model);
            return _mapper.Map<BL.Model.Training>(added);
        }

        public async Task<Model.Training> GetByIdAsync(int id)
        {
            var training = await _repository.GetByIdAsync(id);
            return _mapper.Map<BL.Model.Training>(training);
        }

        public async Task<IEnumerable<Model.Training>> GetAsync()
        {
            var trainings = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<BL.Model.Training>>(trainings);
        }

        public Task RemoveAsync(int id)
        {
            return _repository.RemoveAsync(id);
        }

        public async Task<Model.Training> UpdateAsync(int id, Model.Training training)
        {
            if (training == null)
            {
                throw new ArgumentNullException(nameof(training));
            }

            var model = _mapper.Map<DA.Model.Training>(training);
            var updated = await _repository.UpdateAsync(id, model);
            return _mapper.Map<BL.Model.Training>(updated);
        }
    }
}