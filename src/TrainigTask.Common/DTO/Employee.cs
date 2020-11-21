using System.Collections.Generic;

namespace TrainingTask.Common.DTO
{
    public class Employee
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Position { get; set; }

        public IList<Task> Tasks { get; set; }
    }
}