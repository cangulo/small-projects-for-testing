using Microsoft.AspNetCore.Mvc;
using ObjetivesTracker.Contracts.Models;
using ObjetivesTracker.Contracts.Services;
using System;
using System.Collections.Generic;

namespace ObjectivesTracker.API.Controllers
{
    [Route("api/[controller]")]
    public class ObjectivesController : ControllerBase
    {
        private readonly IObjectiveService _objetiveService;

        public ObjectivesController(IObjectiveService objetiveService)
        {
            _objetiveService = objetiveService ?? throw new ArgumentNullException(nameof(objetiveService));
        }

        [HttpGet]
        public IEnumerable<Objective> Get() => _objetiveService.GetObjectives();

        [HttpGet("{id}")]
        public Objective Get(int id) => _objetiveService.GetObjectiveById(id);

        [HttpPost]
        public void Post([FromBody] Objective value)
        {
            _objetiveService.CreateObjective(value);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Objective value)
        {
            if (id != value.ObjectiveId)
            {
                return BadRequest();
            }
            _objetiveService.UpdateObjective(value);
            return Accepted();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
            => _objetiveService.DeleteObjectiveById(id);
    }
}