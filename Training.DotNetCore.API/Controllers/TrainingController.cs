using System.Collections.Generic;
using System.Threading.Tasks;
using Training.DotNetCore.BL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Training.DotNetCore.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private ITrainingService _service;
        public TrainingController(ITrainingService service, IOptions<MySettings> mySettingsAccessor)
        {
            _service = service;
            var mySettings = mySettingsAccessor.Value;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BL.Model.Training>), 200)]
        public Task<IEnumerable<BL.Model.Training>> Get()
        {
            return _service.GetAsync();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BL.Model.Training), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BL.Model.Training>> GetById(int id)
        {
            var training = await _service.GetByIdAsync(id);
            if (training == null)
            {
                return NotFound();
            }
            return training;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BL.Model.Training), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BL.Model.Training>> Add(BL.Model.Training model)
        {
            var created = await _service.AddAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(BL.Model.Training), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BL.Model.Training>> Update(int id, BL.Model.Training model)
        {
            var updated = await _service.UpdateAsync(id, model);
            if (updated == null) 
            {
                return NotFound();
            }
            return updated;
        }

        [HttpDelete("{id:int}")]
        public Task Remove(int id)
        {
            return _service.RemoveAsync(id);
        }
    }
}