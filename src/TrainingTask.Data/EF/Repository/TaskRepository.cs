using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using TrainingTask.Common.DTO;
using TrainingTask.Common.Exceptions;
using TaskModel = TrainingTask.Common.DTO.Task;
using TaskEF = TrainingTask.Data.EF.Model.TaskManagement.Task;

namespace TrainingTask.Data.EF.Repository
{
    public class TaskRepository : IRepository<TaskModel>
    {
        private readonly TrainingTaskDbContext _context;
        private readonly IMapper _mapper;

        public TaskRepository(TrainingTaskDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<TaskModel> GetAllItems()
        {
            var taskDb = _context.Tasks
                .Include(t => t.Employees)
                .ToList();

            var tasks = taskDb.Select(e =>
            {
                var task = _mapper.Map<TaskModel>(e);
                task.Employees = e.Employees.Select(et => _mapper.Map<Employee>(et)).ToList();

                return task;
            });

            return tasks;
        }

        public TaskModel GetItem(int id)
        {
            var taskDb = _context.Tasks.Where(e => e.Id == id)
                .Include(e => e.Employees)
                .FirstOrDefault();

            var task = taskDb is null
                ? throw new ObjectNotFoundException(typeof(TaskEF).ToString())
                : _mapper.Map<TaskModel>(taskDb);

            task.Employees = taskDb.Employees.Select(et => _mapper.Map<Employee>(et)).ToList();

            return task;
        }

        public int AddItem(TaskModel item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var taskDb = _mapper.Map<TaskEF>(item);

            _context.Tasks.Add(taskDb);

            _context.SaveChanges();

            return taskDb.Id;
        }

        public int RemoveItem(TaskModel item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var taskDb = _context.Tasks.FirstOrDefault(e => e.Id == item.Id) ??
                         throw new ObjectNotFoundException(typeof(TaskEF).ToString());

            _context.Tasks.Remove(taskDb);

            return _context.SaveChanges();
        }

        public int UpdateItem(TaskModel item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var taskDb = _context.Tasks.FirstOrDefault(e => e.Id == item.Id) ??
                         throw new ObjectNotFoundException(typeof(TaskEF).ToString());

            _mapper.Map(item, taskDb);

            return _context.SaveChanges();
        }
    }
}