using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainingTask.Web.Model
{
    public class ProjectViewModel
    {
        public int? Id { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string ShortName { get; set; }

        [Required] public string Description { get; set; }

        public IEnumerable<TaskViewListModel> Tasks { get; set; }
    }
}