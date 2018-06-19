using System.Collections.Generic;
using System.Threading.Tasks;

namespace Training.DotNetCore.BL.Services
{
    public interface ITrainingService
    {
        Task<Model.Training> GetByIdAsync(int id);

        Task<IEnumerable<Model.Training>> GetAsync();

        Task<Model.Training> AddAsync(Model.Training training);

        Task<Model.Training> UpdateAsync(int id, Model.Training training);

        Task RemoveAsync(int id);
    }
}