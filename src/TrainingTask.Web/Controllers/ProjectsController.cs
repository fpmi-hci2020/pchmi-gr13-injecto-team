using System;
using System.Linq;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using TrainingTask.Common.Contract.Project;
using TrainingTask.Core;
using TrainingTask.Web.Infrastructure;
using TrainingTask.Web.Model;

namespace TrainingTask.Web.Controllers
{
    [TypeFilter(typeof(ExceptionHandlingAttributeMvc))]
    public class ProjectsController : Controller
    {
        public const string ControllerName = "Projects";
        public const string EditAction = "Edit";
        public const string DeleteAction = "Delete";
        public const string SaveAction = "Save";
        public const string IndexAction = "Index";
        private const string CreateView = "Create";

        private readonly CommandProcessor _commandProcessor;
        private readonly IMapper _mapper;

        public ProjectsController(CommandProcessor commandProcessor, IMapper mapper)
        {
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IActionResult Index()
        {
            var responseAllProjects =
                _commandProcessor.Process<GetAllProjectResponse, GetAllProjectRequest>(new GetAllProjectRequest());

            var viewProjects = responseAllProjects.Projects.Select(p => _mapper.Map<ProjectListViewModel>(p));

            return View(viewProjects);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewBag.ProjectId = id;
            var responseProject =
                _commandProcessor.Process<GetProjectResponse, GetProjectRequest>(new GetProjectRequest {Id = id});

            return View(CreateView, _mapper.Map<ProjectViewModel>(responseProject.Project));
        }

        public IActionResult Delete(int id)
        {
            var responseProject =
                _commandProcessor.Process<GetProjectResponse, GetProjectRequest>(new GetProjectRequest {Id = id});

            var project = responseProject.Project;

            _commandProcessor.Process<DeleteProjectResponse, DeleteProjectRequest>(new DeleteProjectRequest
            {
                Id = project.Id
            });

            return RedirectToAction(IndexAction);
        }

        [HttpPost]
        public IActionResult Save(ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                if (project.Id.HasValue)
                {
                    _commandProcessor.Process<EditProjectResponse, EditProjectRequest>(
                        _mapper.Map<EditProjectRequest>(project));

                    return RedirectToAction(IndexAction);
                }

                _commandProcessor.Process<CreateProjectResponse, CreateProjectRequest>(
                    _mapper.Map<CreateProjectRequest>(project));

                return RedirectToAction(IndexAction);
            }

            return View(CreateView, project);
        }
    }
}