using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Common.Contract.Employee;
using TrainingTask.Core;
using TrainingTask.Web.Model.WebApi.Employee;

namespace TrainingTask.Web.Controllers.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly CommandProcessor _commandProcessor;
        private readonly IMapper _mapper;

        public StaffController(CommandProcessor commandProcessor, IMapper mapper)
        {
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            var responseAllEmployees =
                _commandProcessor
                    .Process<GetAllEmployeesResponse, GetAllEmployeesRequest>(new GetAllEmployeesRequest());

            return Ok(responseAllEmployees.Employees);
        }


        [HttpGet]
        [Route("{id:int:min(1)}")]
        public IActionResult GetEmployee(int id)
        {
            var response = _commandProcessor
                .Process<GetEmployeeResponse, GetEmployeeRequest>(new GetEmployeeRequest {Id = id});

            return Ok(response.Employee);
        }

        [HttpPost]
        public IActionResult AddEmployee(UpdateEmployeeItem employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response =
                _commandProcessor.Process<CreateEmployeeResponse, CreateEmployeeRequest>(
                    _mapper.Map<CreateEmployeeRequest>(employee));
            var location = $"/api/staff/{response.Id}";

            return Created(location, employee);
        }

        [HttpPut]
        [Route("{id:int:min(1)}")]
        public IActionResult UpdateEmployee(int id, UpdateEmployeeItem employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var request = _mapper.Map<EditEmployeeRequest>(employee);
            request.Id = id;
            _commandProcessor.Process<EditEmployeeResponse, EditEmployeeRequest>(request);

            return Ok();
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public IActionResult DeleteEmployee(int id)
        {
            _commandProcessor.Process<DeleteEmployeeResponse, DeleteEmployeeRequest>(
                new DeleteEmployeeRequest {Id = id});

            return Ok();
        }
    }
}