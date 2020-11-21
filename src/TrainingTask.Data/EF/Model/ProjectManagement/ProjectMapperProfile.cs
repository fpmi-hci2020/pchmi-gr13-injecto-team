using System.Linq;

using AutoMapper;

using TrainingTask.Common.DTO;

using ProjectEF = TrainingTask.Data.EF.Model.ProjectManagement.Project;
using ProjectModel = TrainingTask.Common.DTO.Project;

namespace TrainingTask.Data.EF.Model.ProjectManagement
{
    internal class ProjectMapperProfile : Profile
    {
        public ProjectMapperProfile()
        {
            CreateMap<ProjectEF, ProjectModel>()
                .ReverseMap();
        }
    }
}