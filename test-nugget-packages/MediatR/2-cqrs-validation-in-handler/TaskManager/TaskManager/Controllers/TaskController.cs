using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using TaskManager.Domain.Operations.CreateTaskCommand;
using TaskManager.Domain.Operations.GetTaskQuery;
using TaskManager.Entities;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator ?? throw new NullReferenceException(nameof(mediator));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var response = await _mediator.Send(new GetTaskQuery { });
            if (response.IsFailed)
                return BadRequest(response.Errors);
            return Ok(response.Value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskEntity task)
        {
            var response = await _mediator.Send(new CreateTaskCommand { Task = task });
            if (response.IsFailed)
                return BadRequest(response.Errors);
            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}