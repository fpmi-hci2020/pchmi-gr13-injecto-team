namespace TrainingTask.Common.Contract.Employee
{
    public class EditEmployeeRequest : BaseRequest
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Position { get; set; }
    }
}