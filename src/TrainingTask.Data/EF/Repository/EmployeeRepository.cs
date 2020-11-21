using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using TrainingTask.Common.DTO;
using TrainingTask.Common.Exceptions;
using EmployeeModel = TrainingTask.Common.DTO.Employee;
using EmployeeEF = TrainingTask.Data.EF.Model.EmployeeManagement.Employee;

namespace TrainingTask.Data.EF.Repository
{
    public class EmployeeRepository : IRepository<EmployeeModel>
    {
        private readonly TrainingTaskDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(TrainingTaskDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<EmployeeModel> GetAllItems()
        {
            var employeesDb = _context.Staff
                .Include(e => e.Tasks)
                .ToList();

            var employees = employeesDb.Select(e =>
            {
                var employee = _mapper.Map<EmployeeModel>(e);
                employee.Tasks = e.Tasks.Select(et => _mapper.Map<Task>(et)).ToList();

                return employee;
            });

            return employees;
        }

        public EmployeeModel GetItem(int id)
        {
            var employeeDb = _context.Staff.Where(e => e.Id == id)
                .Include(e => e.Tasks)
                .FirstOrDefault();

            var employee = employeeDb is null
                ? throw new ObjectNotFoundException(typeof(EmployeeEF).ToString())
                : _mapper.Map<EmployeeModel>(employeeDb);

            employee.Tasks = employeeDb.Tasks.Select(et => _mapper.Map<Task>(et)).ToList();

            return employee;
        }

        public int AddItem(EmployeeModel item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var employeeDb = _mapper.Map<EmployeeEF>(item);

            _context.Staff.Add(employeeDb);

            _context.SaveChanges();

            return employeeDb.Id;
        }

        public int RemoveItem(EmployeeModel item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var employeeDb = _context.Staff.FirstOrDefault(e => e.Id == item.Id) ??
                             throw new ObjectNotFoundException(typeof(EmployeeEF).ToString());

            _context.Staff.Remove(employeeDb);

            return _context.SaveChanges();
        }

        public int UpdateItem(EmployeeModel item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var employeeDb = _context.Staff.FirstOrDefault(e => e.Id == item.Id) ??
                             throw new ObjectNotFoundException(typeof(EmployeeEF).ToString());

            _mapper.Map(item, employeeDb);

            return _context.SaveChanges();
        }
    }
}