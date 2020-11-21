using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NUnit.Framework;
using TrainingTask.Common.Contract.Project;
using TrainingTask.Common.DTO;
using TrainingTask.Common.Exceptions;
using TrainingTask.Core.Service;
using TrainingTask.Tests.CoreTests.Util;

namespace TrainingTask.Tests.CoreTests.EFTest
{
    public class ProjectServiceTest : TestFixtureDbBase
    {
        private readonly ProjectService _service = new ProjectService(
            new MapperConfiguration(opt => opt.AddMaps(AppDomain.CurrentDomain.GetAssemblies())).CreateMapper());

        [Test]
        [Retry(10)]
        public void Create_Project()
        {
            var createProject = new CreateProjectRequest
            {
                Name = "Project45",
                ShortName = "Pr3",
                Description = "sdfdsfdasdsf"
            };

            var id = _service.CreateProject(createProject, UnitOfWork).Id;

            var expected = _service.GetProject(new GetProjectRequest {Id = id}, UnitOfWork).Project;

            TestUtil.AreEqual(expected, new Project
            {
                Id = id,
                Name = "Project45",
                ShortName = "Pr3",
                Description = "sdfdsfdasdsf"
            });
        }

        [Test]
        [Retry(10)]
        public void Delete_Project()
        {
            var createProject = new CreateProjectRequest
            {
                Name = "Project45",
                ShortName = "Pr3",
                Description = "sdfdsfdasdsf"
            };

            var id = _service.CreateProject(createProject, UnitOfWork).Id;

            _service.DeleteProject(new DeleteProjectRequest
            {
                Id = id
            }, UnitOfWork);

            Assert.Throws<ObjectNotFoundException>(() =>
                _service.GetProject(new GetProjectRequest {Id = id}, UnitOfWork));
        }

        [Test]
        [Retry(10)]
        public void Edit_Project()
        {
            var origTask = _service.GetAllProjects(new GetAllProjectRequest(), UnitOfWork).Projects
                .OrderBy(x => new Random().Next()).FirstOrDefault();

            var editProject = new EditProjectRequest
            {
                Id = origTask.Id,
                Name = origTask.Name,
                Description = origTask.Description,
                ShortName = origTask.ShortName
            };

            _service.EditProject(editProject, UnitOfWork);

            var actual = _service.GetProject(new GetProjectRequest {Id = origTask.Id}, UnitOfWork).Project;

            TestUtil.AreEqual(new Project
            {
                Id = origTask.Id,
                Name = origTask.Name,
                Description = origTask.Description,
                ShortName = origTask.ShortName,
                Tasks = new List<Task>()
            }, actual);
        }

        [Test]
        public void Get_All_Projects()
        {
            _service.GetAllProjects(new GetAllProjectRequest(), UnitOfWork);
        }

        [Test]
        public void Get_Project_By_Id()
        {
            var actual = _service.GetProject(new GetProjectRequest {Id = 1}, UnitOfWork).Project;

            TestUtil.AreEqual(new Project
            {
                Id = 1,
                Name = "Project1",
                ShortName = "Pr1",
                Description = "sdfdsfdsf",
                Tasks = new List<Task>()
            }, actual);
        }
    }
}