using System;
using System.Collections.Generic;
using System.Linq;

using TrainingTask.Common.Contract;
using TrainingTask.Common.Contract.Employee;
using TrainingTask.Common.Contract.Project;
using TrainingTask.Common.Contract.Task;
using TrainingTask.Core.Service;
using TrainingTask.Data;

namespace TrainingTask.Core
{
    public class CommandProcessor
    {
        private readonly EmployeeService _employeeService;
        private readonly ProjectService _projectService;

        private readonly TaskService _taskService;

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public CommandProcessor(EmployeeService employeeService, IUnitOfWorkFactory factory,
            TaskService taskService, ProjectService projectService)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
            _unitOfWorkFactory = factory ?? throw new ArgumentNullException(nameof(factory));

            Requests = new List<RequestRegistration>();

            InitRegistrations();
        }

        internal List<RequestRegistration> Requests { get; }

        private void InitRegistrations()
        {
            Subscribe<GetAllEmployeesRequest, GetAllEmployeesResponse>(_employeeService.GetAllEmployees);
            Subscribe<GetEmployeeRequest, GetEmployeeResponse>(_employeeService.GetEmployee);
            Subscribe<CreateEmployeeRequest, CreateEmployeeResponse>(_employeeService.CreateEmployee);
            Subscribe<EditEmployeeRequest, EditEmployeeResponse>(_employeeService.EditEmployee);
            Subscribe<DeleteEmployeeRequest, DeleteEmployeeResponse>(_employeeService.DeleteEmployee);

            Subscribe<GetAllProjectRequest, GetAllProjectResponse>(_projectService.GetAllProjects);
            Subscribe<GetProjectRequest, GetProjectResponse>(_projectService.GetProject);
            Subscribe<CreateProjectRequest, CreateProjectResponse>(_projectService.CreateProject);
            Subscribe<EditProjectRequest, EditProjectResponse>(_projectService.EditProject);
            Subscribe<DeleteProjectRequest, DeleteProjectResponse>(_projectService.DeleteProject);

            Subscribe<GetAllTasksRequest, GetAllTasksResponse>(_taskService.GetAllTasks);
            Subscribe<GetTaskRequest, GetTaskResponse>(_taskService.GetTask);
            Subscribe<CreateTaskRequest, CreateTaskResponse>(_taskService.CreateTask);
            Subscribe<EditTaskRequest, EditTaskResponse>(_taskService.EditTask);
            Subscribe<DeleteTaskRequest, DeleteTaskResponse>(_taskService.DeleteTask);
        }

        public void Subscribe<TRequest, TResponse>(Func<TRequest, UnitOfWork, TResponse> handler)
            where TRequest : BaseRequest, new() where TResponse : BaseResponse, new()
        {
            Requests.Add(new RequestRegistration<TRequest, TResponse>(handler));
        }

        public TResponse Process<TResponse, TRequest>(TRequest request)
            where TResponse : BaseResponse, new()
            where TRequest : BaseRequest, new()
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                try
                {
                    var registration = Requests.FirstOrDefault(x =>
                        x.RequestType == typeof(TRequest).ToString() && x.ReplyType == typeof(TResponse).ToString());

                    if (registration is null)
                    {
                        throw new NotSupportedException();
                    }

                    return ((RequestRegistration<TRequest, TResponse>) registration).Handler(request, unitOfWork);
                }
                catch
                {
                    unitOfWork.Rollback();

                    throw;
                }
            }
        }
    }

    internal class RequestRegistration
    {
        public string RequestType { get; set; }

        public string ReplyType { get; set; }
    }

    internal class RequestRegistration<TRequest, TResponse> : RequestRegistration where TRequest : BaseRequest, new()
        where TResponse : BaseResponse, new()
    {
        public RequestRegistration(Func<TRequest, UnitOfWork, TResponse> handler)
        {
            RequestType = typeof(TRequest).ToString();
            ReplyType = typeof(TResponse).ToString();
            Handler = handler;
        }

        public Func<TRequest, UnitOfWork, TResponse> Handler { get; }
    }
}