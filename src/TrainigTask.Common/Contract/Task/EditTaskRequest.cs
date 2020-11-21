using System;
using System.Collections.Generic;

namespace TrainingTask.Common.Contract.Task
{
    public class EditTaskRequest : BaseRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Work { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public int ProjectId { get; set; }

        public virtual IEnumerable<DTO.Employee> Employees { get; set; }
    }
}