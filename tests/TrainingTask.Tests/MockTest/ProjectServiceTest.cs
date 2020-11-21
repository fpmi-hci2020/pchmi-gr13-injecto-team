using Moq;

using NUnit.Framework;

using TrainingTask.Common.Contract.Project;
using TrainingTask.Common.DTO;
using TrainingTask.Core.Service;

namespace TrainingTask.Tests.MockTest
{
    public class ProjectServiceTest : BaseMockTest
    {
        [Test]
        public void Add_Project_Test()
        {
            var service = new ProjectService(Mapper);
            var result = service.CreateProject(new CreateProjectRequest(), UnitOfWork);
            ProjectRepository.Verify(r => r.AddItem(It.IsAny<Project>()), Times.Once);
        }

        [Test]
        public void Delete_Project_Test()
        {
            var service = new ProjectService(Mapper);
            ProjectRepository.Setup(r => r.GetItem(It.IsAny<int>())).Returns(new Project
            {
                Tasks = new[] {new Task(), new Task()}
            });

            var result = service.DeleteProject(new DeleteProjectRequest(), UnitOfWork);

            EmployeeRepository.Verify(r => r.RemoveItem(It.IsAny<Employee>()), Times.Never);
            ProjectRepository.Verify(r => r.RemoveItem(It.IsAny<Project>()), Times.Once);
            TaskRepository.Verify(r => r.RemoveItem(It.IsAny<Task>()), Times.Exactly(2));
        }

        [Test]
        public void Edit_Project_Test()
        {
            var service = new ProjectService(Mapper);
            var result = service.EditProject(new EditProjectRequest(), UnitOfWork);
            EmployeeRepository.Verify(r => r.UpdateItem(It.IsAny<Employee>()), Times.Never);
            ProjectRepository.Verify(r => r.UpdateItem(It.IsAny<Project>()), Times.Once);
            TaskRepository.Verify(r => r.UpdateItem(It.IsAny<Task>()), Times.Never);
        }

        [Test]
        public void Get_Project_Test()
        {
            var service = new ProjectService(Mapper);
            ProjectRepository.Setup(r => r.GetItem(It.IsAny<int>())).Returns(new Project
            {
                Tasks = new[] {new Task(), new Task()}
            });
            var result = service.GetProject(new GetProjectRequest(), UnitOfWork);
            ProjectRepository.Verify(r => r.GetItem(It.IsAny<int>()), Times.Once);

            foreach (var task in result.Project.Tasks)
            {
            }

            TaskRepository.Verify(r => r.GetItem(It.IsAny<int>()), Times.Exactly(2));
        }

        [Test]
        public void GetAll_Projects_Test()
        {
            var service = new ProjectService(Mapper);
            var result = service.GetAllProjects(new GetAllProjectRequest(), UnitOfWork);
            ProjectRepository.Verify(r => r.GetAllItems(), Times.Once);
        }
    }
}