using System;
using System.Linq;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using TrainingTask.Common.Contract.Employee;
using TrainingTask.Core;
using TrainingTask.Web.Infrastructure;
using TrainingTask.Web.Model;

namespace TrainingTask.Web.Controllers
{
    [TypeFilter(typeof(ExceptionHandlingAttributeMvc))]
    public class StaffController : Controller
    {
        public const string IndexAction = "Index";
        public const string CreateAction = "Create";
        public const string EditAction = "Edit";
        public const string DeleteAction = "Delete";
        public const string SaveAction = "Save";

        private const string CreateView = "Create";

        private readonly CommandProcessor _commandProcessor;
        private readonly IMapper _mapper;

        public StaffController(CommandProcessor commandProcessor, IMapper mapper)
        {
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IActionResult Index()
        {
            var responseAllEmployees =
                _commandProcessor
                    .Process<GetAllEmployeesResponse, GetAllEmployeesRequest>(new GetAllEmployeesRequest());

            var viewEmploy = responseAllEmployees.Employees.Select(e => _mapper.Map<EmployeeListViewModel>(e));

            return View(viewEmploy);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            var response = _commandProcessor
                .Process<GetEmployeeResponse, GetEmployeeRequest>(new GetEmployeeRequest {Id = id});

            return View(CreateView, _mapper.Map<EmployeeListViewModel>(response.Employee));
        }

        [HttpPost]
        public IActionResult Save(EmployeeListViewModel employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.Id.HasValue)
                {
                    _commandProcessor.Process<EditEmployeeResponse, EditEmployeeRequest>(
                        _mapper.Map<EditEmployeeRequest>(employee));

                    return RedirectToAction(IndexAction);
                }

                _commandProcessor.Process<CreateEmployeeResponse, CreateEmployeeRequest>(
                    _mapper.Map<CreateEmployeeRequest>(employee));

                return RedirectToAction(IndexAction);
            }

            return View(CreateView, employee);
        }

        public IActionResult Delete(int id)
        {
            var response = _commandProcessor
                .Process<GetEmployeeResponse, GetEmployeeRequest>(new GetEmployeeRequest {Id = id});
            var employee = response.Employee;

            _commandProcessor.Process<DeleteEmployeeResponse, DeleteEmployeeRequest>(new DeleteEmployeeRequest
            {
                Id = employee.Id
            });

            return RedirectToAction(IndexAction);
        }
    }
}