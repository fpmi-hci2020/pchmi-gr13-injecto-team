using System.Collections.Generic;

namespace TrainingTask.Common.DTO
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public IEnumerable<Task> Tasks { get; set; }
    }
}