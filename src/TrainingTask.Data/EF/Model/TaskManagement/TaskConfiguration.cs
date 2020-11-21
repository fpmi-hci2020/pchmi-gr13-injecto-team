using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TrainingTask.Data.EF.Model.TaskManagement
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("Task");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("Id").IsRequired();
            builder.Property(t => t.Work).HasColumnName("Work").IsRequired();
            builder.Property(t => t.StartDate).HasColumnName("StartDate").IsRequired();
            builder.Property(t => t.EndDate).HasColumnName("EndDate").IsRequired();
            builder.Property(t => t.Status).HasColumnName("State").IsRequired();
            builder.Property(t => t.Name).HasColumnName("Name").HasMaxLength(50).IsRequired();
            builder.Property(t => t.ProjectId).HasColumnName("ProjectId").IsRequired();
            builder.HasMany(_ => _.Employees).WithMany(_ => _.Tasks);

        }
    }
}