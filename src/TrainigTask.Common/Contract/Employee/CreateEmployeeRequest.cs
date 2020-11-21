namespace TrainingTask.Common.Contract.Employee
{
    public class CreateEmployeeRequest : BaseRequest
    {
        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Position { get; set; }
    }
}