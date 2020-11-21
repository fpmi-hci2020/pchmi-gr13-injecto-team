using System;
using System.Collections.Generic;

namespace TrainingTask.Common.DTO
{
    public class Task
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Work { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public virtual int ProjectId { get; set; }

        public IList<Employee> Employees { get; set; }
    }
}