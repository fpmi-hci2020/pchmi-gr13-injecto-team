using System;
using System.Linq;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using TrainingTask.Common.Contract.Employee;
using TrainingTask.Common.Contract.Task;
using TrainingTask.Core;
using TrainingTask.Web.Infrastructure;
using TrainingTask.Web.Model.WebApi.Task;

namespace TrainingTask.Web.Controllers.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly CommandProcessor _commandProcessor;
        private readonly IMapper _mapper;

        public TasksController(CommandProcessor commandProcessor, IMapper mapper)
        {
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var response =
                _commandProcessor.Process<GetAllTasksResponse, GetAllTasksRequest>(new GetAllTasksRequest());

            return Ok(response.Tasks);
        }



        [HttpGet]
        [Route("{id:int:min(1)}")]
        public IActionResult GetTask(int id)
        {
            var response = _commandProcessor.Process<GetTaskResponse, GetTaskRequest>(new GetTaskRequest {Id = id});

            return Ok(response.Task);
        }

        [HttpPost]
        public IActionResult AddTask(UpdateTaskItem task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var request = _mapper.Map<CreateTaskRequest>(task);
            request.Employees = task.EmployeesId.Select(id =>
                _commandProcessor
                    .Process<GetEmployeeResponse, GetEmployeeRequest>(new GetEmployeeRequest {Id = id})
                    .Employee).ToList();

            _commandProcessor.Process<CreateTaskResponse, CreateTaskRequest>(request);

            return Ok();
        }

        [HttpPut]
        [Route("{id:int:min(1)}")]
        public IActionResult UpdateTask(int id, UpdateTaskItem task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var request = _mapper.Map<EditTaskRequest>(task);
            request.Id = id;
            request.Employees = task.EmployeesId.Select(employeeId =>
                _commandProcessor
                    .Process<GetEmployeeResponse, GetEmployeeRequest>(new GetEmployeeRequest {Id = employeeId})
                    .Employee).ToList();

            _commandProcessor.Process<EditTaskResponse, EditTaskRequest>(request);

            return Ok();
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public IActionResult DeleteTask(int id)
        {
            var responseTask = _commandProcessor.Process<GetTaskResponse, GetTaskRequest>(new GetTaskRequest {Id = id});

            _commandProcessor.Process<DeleteTaskResponse, DeleteTaskRequest>(
                _mapper.Map<DeleteTaskRequest>(responseTask.Task));

            return Ok();
        }
    }
}