using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NUnit.Framework;
using TrainingTask.Common.Contract.Task;
using TrainingTask.Common.DTO;
using TrainingTask.Common.Exceptions;
using TrainingTask.Core.Service;
using TrainingTask.Tests.CoreTests.Util;

namespace TrainingTask.Tests.CoreTests.EFTest
{
    public class TaskServiceTest : TestFixtureDbBase
    {
        private readonly TaskService _service =
            new TaskService(new MapperConfiguration(opt => opt.AddMaps(AppDomain.CurrentDomain.GetAssemblies()))
                .CreateMapper());

        [Test]
        [Retry(10)]
        public void Create_Task()
        {
            var employees = new List<Employee>();

            var createTest = new CreateTaskRequest
            {
                Name = "CreateName",
                Work = 4,
                Status = "TestStatus",
                StartDate = DateTime.Parse("5/22/2020 1:01:00 AM"),
                EndDate = DateTime.Parse("3/5/2021 1:01:00 AM"),
                ProjectId = 1,
                Employees = employees
            };

            var id = _service.CreateTask(createTest, UnitOfWork).Id;

            var expected = _service.GetTask(new GetTaskRequest {Id = id}, UnitOfWork).Task;

            TestUtil.AreEqual(expected, new Task
            {
                Id = id,
                Name = createTest.Name,
                StartDate = createTest.StartDate,
                EndDate = createTest.EndDate,
                ProjectId = createTest.ProjectId,
                Status = createTest.Status,
                Work = createTest.Work,
                Employees = employees
            });
        }

        [Test]
        [Retry(10)]
        public void Delete_Task()
        {
            var expected = _service.GetAllTasks(new GetAllTasksRequest(), UnitOfWork).Tasks
                .OrderBy(x => new Random().Next()).FirstOrDefault();

            _service.DeleteTask(new DeleteTaskRequest
            {
                Id = expected.Id
            }, UnitOfWork);

            Assert.Throws<ObjectNotFoundException>(() =>
                _service.GetTask(new GetTaskRequest {Id = expected.Id}, UnitOfWork));
        }

        [Test]
        [Retry(10)]
        public void Edit_Task()
        {
            var origTask = _service.GetAllTasks(new GetAllTasksRequest(), UnitOfWork).Tasks
                .OrderBy(x => new Random().Next()).FirstOrDefault();

            var editTask = new EditTaskRequest
            {
                Id = origTask.Id,
                Name = "CreateName",
                Work = 4,
                Status = "TestStatus",
                StartDate = DateTime.Parse("5/22/2020 1:01:00 AM"),
                EndDate = DateTime.Parse("3/5/2021 1:01:00 AM"),
                ProjectId = 1,
                Employees = new List<Employee>()
            };

            _service.EditTask(editTask, UnitOfWork);

            var actual = _service.GetTask(new GetTaskRequest {Id = origTask.Id}, UnitOfWork).Task;

            TestUtil.AreEqual(new Task
            {
                Id = origTask.Id,
                Name = "CreateName",
                Work = 4,
                Status = "TestStatus",
                StartDate = DateTime.Parse("5/22/2020 1:01:00 AM"),
                EndDate = DateTime.Parse("3/5/2021 1:01:00 AM"),
                ProjectId = 1,
                Employees = new List<Employee>()
            }, actual);
        }

        [Test]
        public void Get_All_Tasks()
        {
            _service.GetAllTasks(new GetAllTasksRequest(), UnitOfWork);
        }

        [Test]
        public void Get_Task_By_Id()
        {
            var actual = _service.GetTask(new GetTaskRequest {Id = 36}, UnitOfWork).Task;

            TestUtil.AreEqual(new Task
            {
                Id = 36,
                Name = "DeleteTask",
                ProjectId = 1,
                Work = 7,
                Status = "InProcess",
                StartDate = DateTime.Parse("5/22/2020 1:01:00 AM"),
                EndDate = DateTime.Parse("3/5/2021 1:01:00 AM"),
                Employees = actual.Employees
            }, actual);
        }

        [Test]
        public void Get_Task_Employe_List_Test()
        {
            var actual = _service.GetTask(new GetTaskRequest {Id = 36}, UnitOfWork).Task;
            var expected = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "asd",
                    Surname = "asd",
                    Position = "asd",
                    Patronymic = "dsa"
                },
                new Employee
                {
                    Id = 27,
                    Name = "EditTest",
                    Surname = "EditTest",
                    Position = "EditTest",
                    Patronymic = "EditTest"
                }
            };

            foreach (var employee in actual.Employees.Select((e, index) => new {Employee = e, Index = index}))
            {
                TestUtil.AreEqual(employee.Employee, expected[employee.Index]);
            }
        }
    }
}