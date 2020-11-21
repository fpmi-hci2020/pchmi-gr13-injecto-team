using System;
using System.Linq;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using TrainingTask.Common.Contract.Employee;
using TrainingTask.Common.Contract.Project;
using TrainingTask.Common.Contract.Task;
using TrainingTask.Core;
using TrainingTask.Web.Infrastructure;
using TrainingTask.Web.Model;

namespace TrainingTask.Web.Controllers
{
    [TypeFilter(typeof(ExceptionHandlingAttributeMvc))]
    public class TasksController : Controller
    {
        public const string Name = "Tasks";
        public const string IndexAction = "Index";
        public const string CreateAction = "Create";
        public const string EditAction = "Edit";
        public const string DeleteAction = "Delete";
        public const string SaveAction = "Save";

        private const string CreateView = "Create";

        private readonly CommandProcessor _commandProcessor;
        private readonly IMapper _mapper;

        public TasksController(CommandProcessor commandProcessor, IMapper mapper)
        {
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IActionResult Index()
        {
            var response =
                _commandProcessor.Process<GetAllTasksResponse, GetAllTasksRequest>(new GetAllTasksRequest());

            var viewTask = response.Tasks.Select(t =>
            {
                var task = _mapper.Map<TaskViewListModel>(t);
                task.ProjectShortName = _commandProcessor
                    .Process<GetProjectResponse, GetProjectRequest>(new GetProjectRequest {Id = t.ProjectId}).Project
                    .ShortName;

                return task;
            });

            return View(viewTask);
        }

        public IActionResult Edit(int id, string routeLink)
        {
            var response = _commandProcessor.Process<GetTaskResponse, GetTaskRequest>(new GetTaskRequest {Id = id});
            var responseAllEmployees =
                _commandProcessor
                    .Process<GetAllEmployeesResponse, GetAllEmployeesRequest>(new GetAllEmployeesRequest());
            var responseAllProjects =
                _commandProcessor.Process<GetAllProjectResponse, GetAllProjectRequest>(new GetAllProjectRequest());

            ViewBag.RouteLink = string.IsNullOrEmpty(routeLink) ? GetDefaultRouteLink() : routeLink;
            ViewBag.Projects = responseAllProjects.Projects.Select(p =>
                p.Id == response.Task.ProjectId
                    ? new SelectListItem(p.Name, p.Id.ToString(), true)
                    : new SelectListItem(p.Name, p.Id.ToString()));
            ViewBag.Staff = responseAllEmployees.Employees.Select(e => new SelectListItem(e.Name, e.Id.ToString()));

            return View(CreateView, _mapper.Map<TaskViewModel>(response.Task));
        }

        public IActionResult Delete(int id)
        {
            var responseTask = _commandProcessor.Process<GetTaskResponse, GetTaskRequest>(new GetTaskRequest {Id = id});
            _commandProcessor.Process<DeleteTaskResponse, DeleteTaskRequest>(new DeleteTaskRequest
            {
                Id = responseTask.Task.Id
            });

            return RedirectToAction(IndexAction);
        }

        public IActionResult Create(int? projectId, string routeLink)
        {
            if (projectId.HasValue)
            {
                var responseProject =
                    _commandProcessor.Process<GetProjectResponse, GetProjectRequest>(new GetProjectRequest
                        {Id = projectId.Value});
                ViewBag.Projects = new[]
                {
                    new SelectListItem(responseProject.Project.Name, responseProject.Project.Id.ToString())
                };
            }
            else
            {
                var responseAllProjects =
                    _commandProcessor.Process<GetAllProjectResponse, GetAllProjectRequest>(new GetAllProjectRequest());
                ViewBag.Projects =
                    responseAllProjects.Projects.Select(p => new SelectListItem(p.Name, p.Id.ToString()));
            }

            ViewBag.RouteLink = string.IsNullOrEmpty(routeLink) ? GetDefaultRouteLink() : routeLink;
            var responseAllEmployees =
                _commandProcessor
                    .Process<GetAllEmployeesResponse, GetAllEmployeesRequest>(new GetAllEmployeesRequest());
            ViewBag.Staff = responseAllEmployees.Employees.Select(e => new SelectListItem(e.Name, e.Id.ToString()));

            return View();
        }

        [HttpPost]
        public IActionResult Save(TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                if (task.Id.HasValue)
                {
                    var editTaskRequest = _mapper.Map<EditTaskRequest>(task);
                    editTaskRequest.Employees = task.EmployeesId.Select(id =>
                        _commandProcessor.Process<GetEmployeeResponse, GetEmployeeRequest>(
                                new GetEmployeeRequest {Id = id})
                            .Employee).ToList();

                    _commandProcessor.Process<EditTaskResponse, EditTaskRequest>(editTaskRequest);

                    return Redirect(string.IsNullOrEmpty(task.RouteLink) ? GetDefaultRouteLink() : task.RouteLink);
                }

                var createTaskRequest = _mapper.Map<CreateTaskRequest>(task);
                createTaskRequest.Employees = task.EmployeesId.Select(id =>
                    _commandProcessor.Process<GetEmployeeResponse, GetEmployeeRequest>(
                            new GetEmployeeRequest {Id = id})
                        .Employee).ToList();

                _commandProcessor.Process<CreateTaskResponse, CreateTaskRequest>(createTaskRequest);

                return Redirect(string.IsNullOrEmpty(task.RouteLink) ? GetDefaultRouteLink() : task.RouteLink);
            }

            var responseAllEmployees =
                _commandProcessor
                    .Process<GetAllEmployeesResponse, GetAllEmployeesRequest>(new GetAllEmployeesRequest());
            var responseAllProjects =
                _commandProcessor.Process<GetAllProjectResponse, GetAllProjectRequest>(new GetAllProjectRequest());

            ViewBag.RouteLink = string.IsNullOrEmpty(task.RouteLink) ? GetDefaultRouteLink() : task.RouteLink;
            ViewBag.Projects = responseAllProjects.Projects.Select(p =>
                p.Id == task.ProjectId
                    ? new SelectListItem(p.Name, p.Id.ToString(), true)
                    : new SelectListItem(p.Name, p.Id.ToString()));
            ViewBag.Staff = responseAllEmployees.Employees.Select(e => new SelectListItem(e.Name, e.Id.ToString()));

            return View(CreateView, task);
        }

        private string GetDefaultRouteLink()
        {
            return Url.Action(IndexAction);
        }
    }
}