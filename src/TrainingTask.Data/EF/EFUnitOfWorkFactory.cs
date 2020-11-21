using System;

using AutoMapper;

using TrainingTask.Data.EF.Repository;

namespace TrainingTask.Data.EF
{
    public class EFUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly TrainingTaskDbContext _context;

        private readonly IMapper _mapper;

        public EFUnitOfWorkFactory(TrainingTaskDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public UnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(new EmployeeRepository(_context, _mapper), new TaskRepository(_context, _mapper),
                new ProjectRepository(_context, _mapper));
        }
    }
}