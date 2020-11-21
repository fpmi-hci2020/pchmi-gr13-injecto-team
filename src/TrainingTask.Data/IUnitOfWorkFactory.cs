namespace TrainingTask.Data
{
    public interface IUnitOfWorkFactory
    {
        UnitOfWork CreateUnitOfWork();
    }
}