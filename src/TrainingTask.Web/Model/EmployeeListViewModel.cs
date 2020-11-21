using System.ComponentModel.DataAnnotations;

namespace TrainingTask.Web.Model
{
    public class EmployeeListViewModel
    {
        public int? Id { get; set; }

        [MaxLength(30)] [Required] public string Surname { get; set; }

        [MaxLength(30)] [Required] public string Name { get; set; }

        [MaxLength(30)] [Required] public string Patronymic { get; set; }

        [MaxLength(30)] [Required] public string Position { get; set; }
    }
}