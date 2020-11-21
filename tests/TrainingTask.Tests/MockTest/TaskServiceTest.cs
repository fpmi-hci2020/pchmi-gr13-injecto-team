using Moq;

using NUnit.Framework;

using TrainingTask.Common.Contract.Task;
using TrainingTask.Common.DTO;
using TrainingTask.Core.Service;

namespace TrainingTask.Tests.MockTest
{
    public class TaskServiceTest : BaseMockTest
    {
        [Test]
        public void Add_Task_Test()
        {
            var service = new TaskService(Mapper);
            var result = service.CreateTask(new CreateTaskRequest(), UnitOfWork);
            TaskRepository.Verify(r => r.AddItem(It.IsAny<Task>()), Times.Once);
        }

        [Test]
        public void Delete_Task_Test()
        {
            var service = new TaskService(Mapper);

            var result = service.DeleteTask(new DeleteTaskRequest(), UnitOfWork);

            EmployeeRepository.Verify(r => r.RemoveItem(It.IsAny<Employee>()), Times.Never);
            ProjectRepository.Verify(r => r.RemoveItem(It.IsAny<Project>()), Times.Never);
            TaskRepository.Verify(r => r.RemoveItem(It.IsAny<Task>()), Times.Once);
        }

        [Test]
        public void Edit_Task_Test()
        {
            var service = new TaskService(Mapper);
            var result = service.EditTask(new EditTaskRequest(), UnitOfWork);
            EmployeeRepository.Verify(r => r.UpdateItem(It.IsAny<Employee>()), Times.Never);
            ProjectRepository.Verify(r => r.UpdateItem(It.IsAny<Project>()), Times.Never);
            TaskRepository.Verify(r => r.UpdateItem(It.IsAny<Task>()), Times.Once);
        }

        [Test]
        public void Get_Task_Test()
        {
            var service = new TaskService(Mapper);

            var result = service.GetTask(new GetTaskRequest(), UnitOfWork);

            TaskRepository.Verify(r => r.GetItem(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void GetAll_Tasks_Test()
        {
            var service = new TaskService(Mapper);
            var result = service.GetAllTasks(new GetAllTasksRequest(), UnitOfWork);
            TaskRepository.Verify(r => r.GetAllItems(), Times.Once);
        }
    }
}