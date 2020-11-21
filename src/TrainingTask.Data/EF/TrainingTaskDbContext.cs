using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TrainingTask.Data.EF.Model.EmployeeManagement;
using TrainingTask.Data.EF.Model.ProjectManagement;
using TrainingTask.Data.EF.Model.TaskManagement;

namespace TrainingTask.Data.EF
{
    public sealed class TrainingTaskDbContext : DbContext
    {
        public TrainingTaskDbContext(DbContextOptions<TrainingTaskDbContext> option) : base(option)
        {
        }

        public DbSet<Employee> Staff { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Project> Projects { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasDefaultSchema("dbo");
            base.OnModelCreating(modelBuilder);
        }
    }
}