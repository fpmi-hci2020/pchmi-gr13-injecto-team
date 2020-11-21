using System;

using AutoMapper;

using Moq;

using NUnit.Framework;

using TrainingTask.Common.DTO;
using TrainingTask.Data;

namespace TrainingTask.Tests.MockTest
{
    [TestFixture]
    public abstract class BaseMockTest
    {
        [SetUp]
        public void Setup()
        {
            EmployeeRepository = new Mock<IRepository<Employee>>();
            TaskRepository = new Mock<IRepository<Task>>();
            ProjectRepository = new Mock<IRepository<Project>>();
            UnitOfWork = new UnitOfWork(EmployeeRepository.Object, TaskRepository.Object, ProjectRepository.Object);
        }

        protected UnitOfWork UnitOfWork { get; private set; }

        protected Mock<IRepository<Employee>> EmployeeRepository { get; private set; }

        protected Mock<IRepository<Task>> TaskRepository { get; private set; }

        protected Mock<IRepository<Project>> ProjectRepository { get; private set; }

        protected IMapper Mapper { get; } =
            new MapperConfiguration(opt => opt.AddMaps(AppDomain.CurrentDomain.GetAssemblies())).CreateMapper();
    }
}