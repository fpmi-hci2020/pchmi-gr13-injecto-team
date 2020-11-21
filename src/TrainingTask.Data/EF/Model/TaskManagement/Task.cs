using System;
using System.Collections.Generic;
using TrainingTask.Data.EF.Model.EmployeeManagement;
using TrainingTask.Data.EF.Model.ProjectManagement;

namespace TrainingTask.Data.EF.Model.TaskManagement
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

        public virtual Project Project { get; set; }

        public virtual List<Employee> Employees { get; set; }
    }
}