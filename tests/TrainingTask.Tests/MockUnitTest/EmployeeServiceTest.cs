using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Moq;

using NUnit.Framework;

using TrainingTask.Common.Contract.Employee;
using TrainingTask.Common.DTO;
using TrainingTask.Core.Service;
using TrainingTask.Data;

namespace TrainingTask.Tests.MockUnitTest
{
    public class EmployeeServiceTest
    {
        private readonly IMapper _mapper =
            new MapperConfiguration(opt => opt.AddMaps(AppDomain.CurrentDomain.GetAssemblies()))
                .CreateMapper();

        [Test]
        public void Empty_Repository_Test()
        {
            var service = new EmployeeService(_mapper);
            var employeeRepository = new Mock<IRepository<Employee>>();
            employeeRepository.Setup(r => r.GetAllItems()).Returns(new Employee[] { });
            var unitOfWork = new UnitOfWork(employeeRepository.Object, new Mock<IRepository<Task>>().Object,
                new Mock<IRepository<Project>>().Object);
            var result = service.GetAllEmployees(new GetAllEmployeesRequest(), unitOfWork).Employees;
            CollectionAssert.AreEqual(new Employee[] { }, result);
        }

        [Test]
        public void One_Employee_Repository_Test()
        {
            var service = new EmployeeService(_mapper);
            var employeeRepository = new Mock<IRepository<Employee>>();
            employeeRepository.Setup(r => r.GetAllItems()).Returns(new[]
            {
                new Employee()
            });
            var unitOfWork = new UnitOfWork(employeeRepository.Object, new Mock<IRepository<Task>>().Object,
                new Mock<IRepository<Project>>().Object);
            var result = service.GetAllEmployees(new GetAllEmployeesRequest(), unitOfWork).Employees;
            Assert.AreEqual(result.Count(), 1);
        }

        [Test]
        public void Concrete_Employees_Repository_Test()
        {
            var employees = new[]
            {
                new Employee
                {
                    Id = 1,
                    Name = "Name1",
                    Patronymic = "patronommic1",
                    Position = "Position1",
                    Surname = "Surname1",
                    Tasks = new List<Task>()
                },
                new Employee
                {
                    Id = 10,
                    Name = "Nam2e1",
                    Patronymic = "patron3ommic1",
                    Position = "Posi4tion1",
                    Surname = "Sur5name1",
                    Tasks = new List<Task>()
                },
                new Employee
                {
                    Id = 3,
                    Name = "Na43me1",
                    Patronymic = "patron2ommic1",
                    Position = "Posi5tion1",
                    Surname = "Surna2me1",
                    Tasks = new List<Task>()
                },
                new Employee
                {
                    Id = 11110,
                    Name = "Name41",
                    Patronymic = "p5atronommic1",
                    Position = "Pos2ition1",
                    Surname = "Surn5ame1",
                    Tasks = new List<Task>()
                }
            };
            var service = new EmployeeService(_mapper);
            var employeeRepository = new Mock<IRepository<Employee>>();
            employeeRepository.Setup(r => r.GetAllItems()).Returns(employees);
            var unitOfWork = new UnitOfWork(employeeRepository.Object, new Mock<IRepository<Task>>().Object,
                new Mock<IRepository<Project>>().Object);
            var result = service.GetAllEmployees(new GetAllEmployeesRequest(), unitOfWork).Employees;
            CollectionAssert.AreEqual(employees, result);
        }

        [Test]
        public void Get_Employee_Test()
        {
            var employees = new[]
            {
                new Employee
                {
                    Id = 1,
                    Name = "Name1",
                    Patronymic = "patronommic1",
                    Position = "Position1",
                    Surname = "Surname1",
                    Tasks = new List<Task>()
                },
                new Employee
                {
                    Id = 10,
                    Name = "Nam2e1",
                    Patronymic = "patron3ommic1",
                    Position = "Posi4tion1",
                    Surname = "Sur5name1",
                    Tasks = new List<Task>()
                },
                new Employee
                {
                    Id = 3,
                    Name = "Na43me1",
                    Patronymic = "patron2ommic1",
                    Position = "Posi5tion1",
                    Surname = "Surna2me1",
                    Tasks = new List<Task>()
                },
                new Employee
                {
                    Id = 11110,
                    Name = "Name41",
                    Patronymic = "p5atronommic1",
                    Position = "Pos2ition1",
                    Surname = "Surn5ame1",
                    Tasks = new List<Task>()
                }
            };
            var service = new EmployeeService(_mapper);
            var employeeRepository = new Mock<IRepository<Employee>>();
            employeeRepository.Setup(r => r.GetItem(It.Is<int>(x => x == 11110))).Returns(employees[3]);
            var unitOfWork = new UnitOfWork(employeeRepository.Object, new Mock<IRepository<Task>>().Object,
                new Mock<IRepository<Project>>().Object);
            var result = service.GetEmployee(new GetEmployeeRequest {Id = 11110}, unitOfWork).Employee;
            Assert.AreEqual(employees[3], result);
        }
    }
}