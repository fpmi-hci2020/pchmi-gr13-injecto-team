using System;
using AutoMapper;
using TrainingTask.Common.Contract.Employee;
using TrainingTask.Common.DTO;
using TrainingTask.Data;

namespace TrainingTask.Core.Service
{
    public class EmployeeService
    {
        private readonly IMapper _mapper;

        public EmployeeService(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public GetAllEmployeesResponse GetAllEmployees(GetAllEmployeesRequest request, UnitOfWork context)
        {
            var response = new GetAllEmployeesResponse
            {
                Employees = context.Staff.GetAllItems()
            };

            return response;
        }

        public GetEmployeeResponse GetEmployee(GetEmployeeRequest request, UnitOfWork context)
        {
            var response = new GetEmployeeResponse
            {
                Employee = context.Staff.GetItem(request.Id)
            };

            return response;
        }

        public CreateEmployeeResponse CreateEmployee(CreateEmployeeRequest employee, UnitOfWork context)
        {
            var response = new CreateEmployeeResponse
            {
                Id = context.Staff.AddItem(_mapper.Map<Employee>(employee))
            };

            return response;
        }

        public EditEmployeeResponse EditEmployee(EditEmployeeRequest employee, UnitOfWork context)
        {
            var response = new EditEmployeeResponse
            {
                Count = context.Staff.UpdateItem(_mapper.Map<Employee>(employee))
            };

            return response;
        }

        public DeleteEmployeeResponse DeleteEmployee(DeleteEmployeeRequest employee, UnitOfWork context)
        {
            var response = new DeleteEmployeeResponse
            {
                Count = context.Staff.RemoveItem(_mapper.Map<Employee>(employee))
            };

            return response;
        }
    }
}