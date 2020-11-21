using System;
using System.Linq;

using AutoMapper;

using Moq;

using NUnit.Framework;

using TrainingTask.Common.Contract.Task;
using TrainingTask.Common.DTO;
using TrainingTask.Core.Service;
using TrainingTask.Data;

namespace TrainingTask.Tests.MockUnitTest
{
    public class TaskServiceTest
    {
        private readonly IMapper _mapper =
            new MapperConfiguration(opt => opt.AddMaps(AppDomain.CurrentDomain.GetAssemblies()))
                .CreateMapper();

        [Test]
        public void Empty_Repository_Test()
        {
            var service = new TaskService(_mapper);
            var taskRepository = new Mock<IRepository<Task>>();
            taskRepository.Setup(r => r.GetAllItems()).Returns(new Task[] { });
            var unitOfWork = new UnitOfWork(new Mock<IRepository<Employee>>().Object,
                taskRepository.Object,
                new Mock<IRepository<Project>>().Object);
            var result = service.GetAllTasks(new GetAllTasksRequest(), unitOfWork).Tasks;
            CollectionAssert.AreEqual(new Task[] { }, result);
        }

        [Test]
        public void One_Task_Repository_Test()
        {
            var service = new TaskService(_mapper);
            var taskRepository = new Mock<IRepository<Task>>();
            taskRepository.Setup(r => r.GetAllItems()).Returns(new[]
            {
                new Task()
            });
            var unitOfWork = new UnitOfWork(new Mock<IRepository<Employee>>().Object,
                taskRepository.Object,
                new Mock<IRepository<Project>>().Object);
            var result = service.GetAllTasks(new GetAllTasksRequest(), unitOfWork).Tasks;
            Assert.AreEqual(result.Count(), 1);
        }

        [Test]
        public void Concrete_Tasks_Repository_Test()
        {
            var tasks = new[]
            {
                new Task
                {
                    Id = 1,
                    Name = "asd1asd",
                    ProjectId = 1,
                    Work = 2,
                    StartDate = DateTime.Parse("04.04.2000"),
                    EndDate = DateTime.Parse("05.05.2000"),
                    Status = "InProgress",
                    Employees = new Employee[] { }
                },
                new Task
                {
                    Id = 2,
                    Name = "asd2asd",
                    ProjectId = 1,
                    Work = 10,
                    StartDate = DateTime.Parse("04.04.2000"),
                    EndDate = DateTime.Parse("05.05.2000"),
                    Status = "InProgress",
                    Employees = new Employee[] { }
                },
                new Task
                {
                    Id = 10,
                    Name = "asd3asd",
                    ProjectId = 1,
                    Work = 11,
                    StartDate = DateTime.Parse("04.04.2000"),
                    EndDate = DateTime.Parse("05.05.2000"),
                    Status = "InProgress",
                    Employees = new Employee[] { }
                },
                new Task
                {
                    Id = 11,
                    Name = "as4dasd",
                    ProjectId = 1,
                    Work = 12,
                    StartDate = DateTime.Parse("04.04.2000"),
                    EndDate = DateTime.Parse("05.05.2000"),
                    Status = "InProgress",
                    Employees = new Employee[] { }
                }
            };
            var service = new TaskService(_mapper);
            var taskRepository = new Mock<IRepository<Task>>();
            taskRepository.Setup(r => r.GetAllItems()).Returns(tasks);
            var unitOfWork = new UnitOfWork(new Mock<IRepository<Employee>>().Object,
                taskRepository.Object,
                new Mock<IRepository<Project>>().Object);
            var result = service.GetAllTasks(new GetAllTasksRequest(), unitOfWork).Tasks;
            CollectionAssert.AreEqual(tasks, result);
        }

        [Test]
        public void Get_Task_Test()
        {
            var tasks = new[]
            {
                new Task
                {
                    Id = 1,
                    Name = "asd1asd",
                    ProjectId = 1,
                    Work = 2,
                    StartDate = DateTime.Parse("04.04.2000"),
                    EndDate = DateTime.Parse("05.05.2000"),
                    Status = "InProgress",
                    Employees = new Employee[] { }
                },
                new Task
                {
                    Id = 2,
                    Name = "asd2asd",
                    ProjectId = 1,
                    Work = 10,
                    StartDate = DateTime.Parse("04.04.2000"),
                    EndDate = DateTime.Parse("05.05.2000"),
                    Status = "InProgress",
                    Employees = new Employee[] { }
                },
                new Task
                {
                    Id = 10,
                    Name = "asd3asd",
                    ProjectId = 1,
                    Work = 11,
                    StartDate = DateTime.Parse("04.04.2000"),
                    EndDate = DateTime.Parse("05.05.2000"),
                    Status = "InProgress",
                    Employees = new Employee[] { }
                },
                new Task
                {
                    Id = 11,
                    Name = "as4dasd",
                    ProjectId = 1,
                    Work = 12,
                    StartDate = DateTime.Parse("04.04.2000"),
                    EndDate = DateTime.Parse("05.05.2000"),
                    Status = "InProgress",
                    Employees = new Employee[] { }
                }
            };
            var service = new TaskService(_mapper);
            var taskRepository = new Mock<IRepository<Task>>();
            taskRepository.Setup(r => r.GetItem(It.Is<int>(x => x == 11))).Returns(tasks[3]);
            var unitOfWork = new UnitOfWork(new Mock<IRepository<Employee>>().Object,
                taskRepository.Object,
                new Mock<IRepository<Project>>().Object);
            var result = service.GetTask(new GetTaskRequest {Id = 11}, unitOfWork).Task;
            Assert.AreEqual(tasks[3], result);
        }
    }
}