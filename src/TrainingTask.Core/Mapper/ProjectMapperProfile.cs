using AutoMapper;

using TrainingTask.Common.Contract.Project;
using TrainingTask.Common.DTO;

namespace TrainingTask.Core.Mapper
{
    internal class ProjectMapperProfile : Profile
    {
        public ProjectMapperProfile()
        {
            CreateMap<CreateProjectRequest, Project>();
            CreateMap<EditProjectRequest, Project>();
            CreateMap<DeleteProjectRequest, Project>();
        }
    }
}