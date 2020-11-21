using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainingTask.Web.Model
{
    public class TaskViewModel : IValidatableObject

    {
        public int? Id { get; set; }

        [Required] public int ProjectId { get; set; }

        [Required] public string Name { get; set; }

        [Required] [Range(0, int.MaxValue)] public int Work { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime EndDate { get; set; }

        public TaskState State { get; set; }

        public IEnumerable<int> EmployeesId { get; set; }

        public string RouteLink { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate < DateTime.Now)
            {
                yield return new ValidationResult("Start date must be no less than the current");
            }

            if (StartDate > EndDate)
            {
                yield return new ValidationResult("End date must be greater than end date");
            }
        }
    }
}