using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training.DotNetCore.DA.Model;
using Microsoft.EntityFrameworkCore;

namespace Training.DotNetCore.DA.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private DotNetCoreTrainingContext _context;
        
        public TrainingRepository(DotNetCoreTrainingContext context)
        {
            _context = context;
        }

        public async Task<Model.Training> AddAsync(Model.Training training)
        {
            _context.Add(training);
            await _context.SaveChangesAsync();
            return training;
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _context.Trainings.FirstOrDefaultAsync(t => t.Id == id);
            if (entity != null) 
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Model.Training> GetByIdAsync(int id)
        {
            return await _context.Trainings.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Model.Training>> GetAsync()
        {
            return await _context.Trainings.ToListAsync();
        }

        public async Task<Model.Training> UpdateAsync(int id, Model.Training training)
        {
            var exist = await _context.Trainings.AnyAsync(t => t.Id == id);
            if (!exist)
            {
                return null;
            }
            training.Id = id;
            _context.Trainings.Update(training);
            await _context.SaveChangesAsync();
            return training;
        }
    }
}
