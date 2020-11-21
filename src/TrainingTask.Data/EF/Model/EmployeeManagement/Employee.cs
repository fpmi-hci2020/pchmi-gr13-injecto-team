using System.Collections.Generic;
using TrainingTask.Data.EF.Model.TaskManagement;

namespace TrainingTask.Data.EF.Model.EmployeeManagement
{
    public class Employee
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Position { get; set; }

        public virtual IList<Task> Tasks { get; set; }
    }
}