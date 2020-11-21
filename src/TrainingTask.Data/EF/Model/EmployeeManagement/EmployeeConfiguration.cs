using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TrainingTask.Data.EF.Model.EmployeeManagement
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id").IsRequired();
            builder.Property(e => e.Name).HasColumnName("Name").HasMaxLength(50).IsRequired();
            builder.Property(e => e.Patronymic).HasColumnName("Patronymic").HasMaxLength(50).IsRequired();
            builder.Property(e => e.Position).HasColumnName("Position").HasMaxLength(50).IsRequired();
            builder.HasMany(_ => _.Tasks)
                .WithMany(_ => _.Employees);
        }
    }
}