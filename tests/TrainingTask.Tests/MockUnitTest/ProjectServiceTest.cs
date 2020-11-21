using System;
using System.Linq;

using AutoMapper;

using Moq;

using NUnit.Framework;

using TrainingTask.Common.Contract.Project;
using TrainingTask.Common.DTO;
using TrainingTask.Core.Service;
using TrainingTask.Data;

namespace TrainingTask.Tests.MockUnitTest
{
    public class ProjectServiceTest
    {
        private readonly IMapper _mapper =
            new MapperConfiguration(opt => opt.AddMaps(AppDomain.CurrentDomain.GetAssemblies()))
                .CreateMapper();

        [Test]
        public void Empty_Repository_Test()
        {
            var service = new ProjectService(_mapper);
            var projectRepository = new Mock<IRepository<Project>>();
            projectRepository.Setup(r => r.GetAllItems()).Returns(new Project[] { });
            var unitOfWork = new UnitOfWork(new Mock<IRepository<Employee>>().Object,
                new Mock<IRepository<Task>>().Object,
                projectRepository.Object);
            var result = service.GetAllProjects(new GetAllProjectRequest(), unitOfWork).Projects;
            CollectionAssert.AreEqual(new Project[] { }, result);
        }

        [Test]
        public void One_Project_Repository_Test()
        {
            var service = new ProjectService(_mapper);
            var projectRepository = new Mock<IRepository<Project>>();
            projectRepository.Setup(r => r.GetAllItems()).Returns(new[]
            {
                new Project()
            });
            var unitOfWork = new UnitOfWork(new Mock<IRepository<Employee>>().Object,
                new Mock<IRepository<Task>>().Object,
                projectRepository.Object);
            var result = service.GetAllProjects(new GetAllProjectRequest(), unitOfWork).Projects;
            Assert.AreEqual(result.Count(), 1);
        }

        [Test]
        public void Concrete_Projects_Repository_Test()
        {
            var projects = new[]
            {
                new Project
                {
                    Id = 1,
                    Description = "sad1asd",
                    Name = "da1sd",
                    ShortName = "dasd1asdas",
                    Tasks = new Task[] { }
                },
                new Project
                {
                    Id = 10,
                    Description = "s2dasd",
                    Name = "d2asd",
                    ShortName = "dasd2asdas",
                    Tasks = new Task[] { }
                },
                new Project
                {
                    Id = 11,
                    Description = "sada3sd",
                    Name = "das3d",
                    ShortName = "dasd3asdas",
                    Tasks = new Task[] { }
                },
                new Project
                {
                    Id = 100,
                    Description = "sa4dasd",
                    Name = "da4sd",
                    ShortName = "dasda5sdas",
                    Tasks = new Task[] { }
                }
            };
            var service = new ProjectService(_mapper);
            var projectRepository = new Mock<IRepository<Project>>();
            projectRepository.Setup(r => r.GetAllItems()).Returns(projects);
            var unitOfWork = new UnitOfWork(new Mock<IRepository<Employee>>().Object,
                new Mock<IRepository<Task>>().Object,
                projectRepository.Object);
            var result = service.GetAllProjects(new GetAllProjectRequest(), unitOfWork).Projects;
            CollectionAssert.AreEqual(projects, result);
        }

        [Test]
        public void Get_Project_Test()
        {
            var projects = new[]
            {
                new Project
                {
                    Id = 1,
                    Description = "sad1asd",
                    Name = "da1sd",
                    ShortName = "dasd1asdas",
                    Tasks = new Task[] { }
                },
                new Project
                {
                    Id = 10,
                    Description = "s2dasd",
                    Name = "d2asd",
                    ShortName = "dasd2asdas",
                    Tasks = new Task[] { }
                },
                new Project
                {
                    Id = 11,
                    Description = "sada3sd",
                    Name = "das3d",
                    ShortName = "dasd3asdas",
                    Tasks = new Task[] { }
                },
                new Project
                {
                    Id = 100,
                    Description = "sa4dasd",
                    Name = "da4sd",
                    ShortName = "dasda5sdas",
                    Tasks = new Task[] { }
                }
            };
            var service = new ProjectService(_mapper);
            var projectRepository = new Mock<IRepository<Project>>();
            projectRepository.Setup(r => r.GetItem(It.Is<int>(x => x == 11))).Returns(projects[2]);
            var unitOfWork = new UnitOfWork(new Mock<IRepository<Employee>>().Object,
                new Mock<IRepository<Task>>().Object,
                projectRepository.Object);
            var result = service.GetProject(new GetProjectRequest {Id = 11}, unitOfWork).Project;
            Assert.AreEqual(projects[2], result);
        }
    }
}