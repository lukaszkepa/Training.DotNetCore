using System.Collections.Generic;
using System.Threading.Tasks;

namespace Training.DotNetCore.DA.Repositories
{
    public interface ITrainingRepository
    {
        Task<IEnumerable<Model.Training>> GetAsync();
        Task<Model.Training> GetByIdAsync(int id);
        Task<Model.Training> AddAsync(Model.Training training);
        Task<Model.Training> UpdateAsync(int id, Model.Training training);
        Task RemoveAsync(int id);
    }
}