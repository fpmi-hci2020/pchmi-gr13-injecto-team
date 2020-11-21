using System;
using System.Linq;
using AutoMapper;
using NUnit.Framework;
using TrainingTask.Common.Contract.Employee;
using TrainingTask.Common.DTO;
using TrainingTask.Common.Exceptions;
using TrainingTask.Core.Service;
using TrainingTask.Tests.CoreTests.Util;

namespace TrainingTask.Tests.CoreTests.EFTest
{
    public class EmployeeServiceTest : TestFixtureDbBase
    {
        private readonly EmployeeService _service = new EmployeeService(
            new MapperConfiguration(opt => opt.AddMaps(AppDomain.CurrentDomain.GetAssemblies())).CreateMapper());

        [Test]
        [Retry(10)]
        public void Create_Employee()
        {
            var createEmployee = new CreateEmployeeRequest
            {
                Name = "testCreateName",
                Patronymic = "testCreatepatronomic",
                Position = "testCreatePosition",
                Surname = "testCreatePosition"
            };

            var id = _service.CreateEmployee(createEmployee, UnitOfWork).Id;

            var expected = _service.GetEmployee(new GetEmployeeRequest {Id = id}, UnitOfWork).Employee;

            TestUtil.AreEqual(expected, new Employee
            {
                Id = id,
                Name = createEmployee.Name,
                Patronymic = createEmployee.Patronymic,
                Position = createEmployee.Position,
                Surname = createEmployee.Surname
            });
        }

        [Test]
        [Retry(10)]
        public void Delete_Employee()
        {
            var expected = _service.GetAllEmployees(new GetAllEmployeesRequest(), UnitOfWork).Employees
                .OrderBy(x => new Random().Next()).FirstOrDefault();

            _service.DeleteEmployee(new DeleteEmployeeRequest
            {
                Id = expected.Id
            }, UnitOfWork);

            Assert.Throws<ObjectNotFoundException>(() =>
                _service.GetEmployee(new GetEmployeeRequest {Id = expected.Id}, UnitOfWork));
        }

        [Test]
        [Retry(10)]
        public void Edit_Employee()
        {
            var origEmployee = _service.GetAllEmployees(new GetAllEmployeesRequest(), UnitOfWork).Employees
                .OrderBy(x => new Random().Next()).FirstOrDefault();

            var editEmployee = new EditEmployeeRequest
            {
                Id = origEmployee.Id,
                Name = origEmployee.Name + "1",
                Patronymic = origEmployee.Patronymic + "1",
                Position = origEmployee.Position + "1",
                Surname = origEmployee.Surname + "1"
            };

            _service.EditEmployee(editEmployee, UnitOfWork);

            TestUtil.AreEqual(
                new Employee
                {
                    Id = editEmployee.Id,
                    Name = editEmployee.Name,
                    Patronymic = editEmployee.Patronymic,
                    Position = editEmployee.Position,
                    Surname = editEmployee.Surname
                }, _service.GetEmployee(new GetEmployeeRequest {Id = origEmployee.Id}, UnitOfWork).Employee);
        }

        [Test]
        public void Get_All_Employee()
        {
            _service.GetAllEmployees(new GetAllEmployeesRequest(), UnitOfWork);
        }

        [Test]
        public void Get_Employee_By_Id()
        {
            var employee = _service.GetEmployee(new GetEmployeeRequest {Id = 1}, UnitOfWork);

            TestUtil.AreEqual(employee.Employee, new Employee
            {
                Id = 1,
                Name = "asd",
                Surname = "asd",
                Patronymic = "dsa",
                Position = "asd"
            });
        }
    }
}