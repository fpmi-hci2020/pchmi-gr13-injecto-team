using System;
using System.Linq;

using AutoMapper;

using TrainingTask.Common.Contract.Project;
using TrainingTask.Common.DTO;
using TrainingTask.Data;

namespace TrainingTask.Core.Service
{
    public class ProjectService
    {
        private readonly IMapper _mapper;

        public ProjectService(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public GetAllProjectResponse GetAllProjects(GetAllProjectRequest request, UnitOfWork context)
        {
            var response = new GetAllProjectResponse
            {
                Projects = context.Projects.GetAllItems()
            };

            return response;
        }

        public GetProjectResponse GetProject(GetProjectRequest request, UnitOfWork context)
        {
            var response = new GetProjectResponse();
            var project = context.Projects.GetItem(request.Id);
            project.Tasks = project.Tasks?.Select(t => context.Tasks.GetItem(t.Id));

            response.Project = project;

            return response;
        }

        public CreateProjectResponse CreateProject(CreateProjectRequest project, UnitOfWork context)
        {
            var response = new CreateProjectResponse {Id = context.Projects.AddItem(_mapper.Map<Project>(project))};

            return response;
        }

        public EditProjectResponse EditProject(EditProjectRequest project, UnitOfWork context)
        {
            var response = new EditProjectResponse {Count = context.Projects.UpdateItem(_mapper.Map<Project>(project))};

            return response;
        }

        public DeleteProjectResponse DeleteProject(DeleteProjectRequest project, UnitOfWork context)
        {
            var response = new DeleteProjectResponse();

            var projectDb = context.Projects.GetItem(project.Id);

            foreach (var task in projectDb.Tasks)
            {
                context.Tasks.RemoveItem(task);
            }

            response.Count = context.Projects.RemoveItem(_mapper.Map<Project>(project));

            return response;
        }
    }
}