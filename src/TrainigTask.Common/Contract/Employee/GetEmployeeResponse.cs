namespace TrainingTask.Common.Contract.Employee
{
    public class GetEmployeeResponse : BaseResponse
    {
        public DTO.Employee Employee { get; set; }
    }
}