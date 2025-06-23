using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using TaskManagement.Core.Abstractions;
using TaskManagementWebApi.Input;

namespace TaskManagementWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskManagementController : ControllerBase
    {
        private readonly ILogger<TaskManagementController> _logger;

        public TaskManagementController(ILogger<TaskManagementController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        [ProducesResponseType(typeof(HttpStatusCode), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromServices] IUserCreationUc userCreationUc, UserCreationInput input)
        {
            _logger.LogInformation($"{nameof(CreateUser)}. started.");

            var result = await userCreationUc.Create(input.UserName);

            if (!result.IsSuccess)
            {
                _logger.LogError("Error occurred while creating user. Error: {Error}", result.MessageError);
                _logger.LogInformation($"{nameof(CreateUser)}. Failed.");

                return StatusCode(result.HttpStatusCode, result.MessageError);
            }

            _logger.LogInformation($"{nameof(CreateUser)}. Finished successfully.");
            return Ok();
        }

        /// <summary>
        /// Create new task.
        /// </summary>
        [ProducesResponseType(typeof(HttpStatusCode), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpPost("create-task")]
        public async Task<IActionResult> CreateTask([FromServices] ITaskCreationUc taskCreationUc, TaskCreationInput input)
        {
            _logger.LogInformation($"{nameof(CreateTask)}. started.");

            var result = await taskCreationUc.Create(input.Title);

            if (!result.IsSuccess)
            {
                _logger.LogError("Error occurred while creating task. Error: {Error}", result.MessageError);
                _logger.LogInformation($"{nameof(CreateTask)}. Failed.");

                return StatusCode(result.HttpStatusCode, result.MessageError);
            }

            _logger.LogInformation($"{nameof(CreateTask)}. Finished successfully.");
            return Ok();
        }

        /// <summary>
        /// Start reassignment cycle.
        /// </summary>
        [ProducesResponseType(typeof(HttpStatusCode), StatusCodes.Status200OK)]
        [HttpPost("start-reassignment")]
        public async Task<IActionResult> StartReassignment([FromServices] IReassignmentUc reassignmentUc, TaskReassignmentInput input)
        {
                _logger.LogInformation($"{nameof(StartReassignment)}. started.");

            var result = await reassignmentUc.StartReassignment(input.CronExpression);

            if (!result.IsSuccess)
            {
                _logger.LogError("Error occurred while starting cycle. Error: {Error}", result.MessageError);
                _logger.LogInformation($"{nameof(StartReassignment)}. Failed.");

                return StatusCode(result.HttpStatusCode, result.CodeError);
            }

            _logger.LogInformation($"{nameof(StartReassignment)}. Finished successfully.");
            return Ok();
        }
    }
}
