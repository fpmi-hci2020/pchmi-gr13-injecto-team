using System;
using System.Collections.Generic;

namespace TrainingTask.Web.Model.WebApi.Task
{
    public class UpdateTaskItem
    {
        public string Name { get; set; }

        public int Work { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TaskState Status { get; set; }

        public int ProjectId { get; set; }

        public IEnumerable<int> EmployeesId { get; set; }
    }
}