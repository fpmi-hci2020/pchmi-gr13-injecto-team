using Autofac;

using TrainingTask.Data.EF;

namespace TrainingTask.Data
{
    public class DependencyRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EFUnitOfWorkFactory>().As<IUnitOfWorkFactory>();
        }
    }
}