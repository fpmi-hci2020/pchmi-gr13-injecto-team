using System;
using System.Linq;

using AutoMapper;

using TrainingTask.Common.Contract.Task;
using TrainingTask.Common.DTO;
using TrainingTask.Web.Model;
using TrainingTask.Web.Model.WebApi.Task;

namespace TrainingTask.Web.Mapping
{
    public class TaskMapperProfile : Profile
    {
        public TaskMapperProfile()
        {
            CreateMap<UpdateTaskItem, CreateTaskRequest>().ForMember(t => t.Employees, opt => opt.Ignore());
            CreateMap<UpdateTaskItem, EditTaskRequest>().ForMember(t => t.Employees, opt => opt.Ignore());
            CreateMap<Task, DeleteTaskRequest>();
            CreateMap<Task, TaskViewListModel>().ForMember(e => e.ProjectShortName, opt => opt.Ignore())
                .ForMember(e => e.Employees, opt => opt.MapFrom(t => t.Employees.Select(e => e.Name)));
            CreateMap<Task, TaskViewModel>()
                .ForMember(t => t.State, opt => opt.MapFrom(t => Enum.Parse<TaskState>(t.Status)))
                .ForMember(t => t.EmployeesId, opt => opt.MapFrom(t => t.Employees.Select(e => e.Id)));
            CreateMap<TaskViewModel, EditTaskRequest>().ForMember(t => t.Employees, opt => opt.Ignore())
                .ForMember(t => t.Status, opt => opt.MapFrom(t => t.State.ToString()));
            CreateMap<TaskViewModel, CreateTaskRequest>().ForMember(t => t.Employees, opt => opt.Ignore())
                .ForMember(t => t.Status, opt => opt.MapFrom(t => t.State.ToString()));
        }
    }
}