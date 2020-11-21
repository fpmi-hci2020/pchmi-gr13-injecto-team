using System.Collections.Generic;

namespace TrainingTask.Data
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAllItems();

        T GetItem(int id);

        int AddItem(T item);

        int RemoveItem(T item);

        int UpdateItem(T item);
    }
}