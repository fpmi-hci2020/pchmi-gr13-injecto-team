using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Common.Contract.Project;
using TrainingTask.Core;
using TrainingTask.Web.Infrastructure;
using TrainingTask.Web.Model.WebApi.Project;

namespace TrainingTask.Web.Controllers.WebApi
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProjectsController : ControllerBase
    {
        private readonly CommandProcessor _commandProcessor;
        private readonly IMapper _mapper;

        public ProjectsController(CommandProcessor commandProcessor, IMapper mapper)
        {
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetAllProjects()
        {
            var responseAllProjects =
                _commandProcessor.Process<GetAllProjectResponse, GetAllProjectRequest>(new GetAllProjectRequest());

            return Ok(responseAllProjects.Projects);
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public IActionResult GetProject(int id)
        {
            var responseProject =
                _commandProcessor.Process<GetProjectResponse, GetProjectRequest>(new GetProjectRequest {Id = id});

            return Ok(responseProject.Project);
        }

        [HttpPost]
        public IActionResult AddProject(UpdateProjectItem project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response =
                _commandProcessor.Process<CreateProjectResponse, CreateProjectRequest>(
                    _mapper.Map<CreateProjectRequest>(project));

            var location = $"/api/projects/{response.Id}";

            return Created(location, project);
        }

        [HttpPut]
        [Route("{id:int:min(1)}")]
        public IActionResult EditProject(int id, UpdateProjectItem project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var request = _mapper.Map<EditProjectRequest>(project);
            request.Id = id;

            _commandProcessor.Process<EditProjectResponse, EditProjectRequest>(request);

            return Ok();
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public IActionResult DeleteProject(int id)
        {
            var responseProject =
                _commandProcessor.Process<GetProjectResponse, GetProjectRequest>(new GetProjectRequest {Id = id});

            _commandProcessor.Process<DeleteProjectResponse, DeleteProjectRequest>(
                _mapper.Map<DeleteProjectRequest>(responseProject.Project));

            return Ok();
        }
    }
}