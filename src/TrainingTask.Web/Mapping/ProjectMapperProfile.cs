using System.Linq;

using AutoMapper;

using TrainingTask.Common.Contract.Project;
using TrainingTask.Common.DTO;
using TrainingTask.Web.Model;
using TrainingTask.Web.Model.WebApi.Project;

namespace TrainingTask.Web.Mapping
{
    internal class ProjectMapperProfile : Profile
    {
        public ProjectMapperProfile()
        {
            CreateMap<UpdateProjectItem, CreateProjectRequest>();
            CreateMap<UpdateProjectItem, EditProjectRequest>();
            CreateMap<Project, DeleteProjectRequest>();
            CreateMap<Project, ProjectListViewModel>();
            CreateMap<Project, ProjectViewModel>().ForMember(
                pr => pr.Tasks,
                opt => opt.MapFrom(
                    (order, orderDto, i, context) =>
                        order.Tasks.Select(t => context.Mapper.Map<TaskViewListModel>(t)))
            );
            CreateMap<ProjectViewModel, CreateProjectRequest>();
            CreateMap<ProjectViewModel, EditProjectRequest>();
        }
    }
}