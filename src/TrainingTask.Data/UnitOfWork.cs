using System;
using System.Transactions;

using TrainingTask.Common.DTO;

namespace TrainingTask.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly TransactionScope _scope;
        private bool _disposed;

        public UnitOfWork(IRepository<Employee> staff, IRepository<Task> tasks, IRepository<Project> projects)
        {
            _scope = new TransactionScope();
            Staff = staff ?? throw new ArgumentNullException(nameof(staff));
            Tasks = tasks ?? throw new ArgumentNullException(nameof(tasks));
            Projects = projects ?? throw new ArgumentNullException(nameof(projects));
        }

        public IRepository<Employee> Staff { get; }

        public IRepository<Task> Tasks { get; }

        public IRepository<Project> Projects { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _scope.Complete();

            _scope.Dispose();

            _disposed = true;
        }

        public void Rollback()
        {
            _scope.Dispose();

            _disposed = true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Save();
            }
        }

        ~UnitOfWork()
        {
            Rollback();
        }
    }
}