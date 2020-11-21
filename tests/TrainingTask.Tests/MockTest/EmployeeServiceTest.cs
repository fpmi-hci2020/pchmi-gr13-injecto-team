using Moq;

using NUnit.Framework;

using TrainingTask.Common.Contract.Employee;
using TrainingTask.Common.DTO;
using TrainingTask.Core.Service;

namespace TrainingTask.Tests.MockTest
{
    public class EmployeeServiceTest : BaseMockTest
    {
        [Test]
        public void Add_Employee_Test()
        {
            var service = new EmployeeService(Mapper);
            var result = service.CreateEmployee(new CreateEmployeeRequest(), UnitOfWork);
            EmployeeRepository.Verify(r => r.AddItem(It.IsAny<Employee>()), Times.Once);
        }

        [Test]
        public void Delete_Employee_Test()
        {
            var service = new EmployeeService(Mapper);

            var result = service.DeleteEmployee(new DeleteEmployeeRequest(), UnitOfWork);
            EmployeeRepository.Verify(r => r.RemoveItem(It.IsAny<Employee>()), Times.Once);
            ProjectRepository.Verify(r => r.RemoveItem(It.IsAny<Project>()), Times.Never);
            TaskRepository.Verify(r => r.RemoveItem(It.IsAny<Task>()), Times.Never);
        }

        [Test]
        public void Edit_Employee_Test()
        {
            var service = new EmployeeService(Mapper);
            var result = service.EditEmployee(new EditEmployeeRequest(), UnitOfWork);
            EmployeeRepository.Verify(r => r.UpdateItem(It.IsAny<Employee>()), Times.Once);
            ProjectRepository.Verify(r => r.UpdateItem(It.IsAny<Project>()), Times.Never);
            TaskRepository.Verify(r => r.UpdateItem(It.IsAny<Task>()), Times.Never);
        }

        [Test]
        public void GetAll_Employee_Test()
        {
            var service = new EmployeeService(Mapper);
            var result = service.GetAllEmployees(new GetAllEmployeesRequest(), UnitOfWork);
            EmployeeRepository.Verify(r => r.GetAllItems(), Times.Once);
        }
    }
}