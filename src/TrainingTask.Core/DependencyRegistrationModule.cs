using Autofac;

using TrainingTask.Core.Service;

namespace TrainingTask.Core
{
    public class DependencyRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProjectService>();
            builder.RegisterType<TaskService>();
            builder.RegisterType<EmployeeService>();
            builder.RegisterType<CommandProcessor>();
        }
    }
}