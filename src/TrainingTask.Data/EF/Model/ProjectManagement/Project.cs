using System.Collections.Generic;

using TrainingTask.Data.EF.Model.TaskManagement;

namespace TrainingTask.Data.EF.Model.ProjectManagement
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}