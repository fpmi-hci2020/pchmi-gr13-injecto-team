namespace TrainingTask.Common.Contract.Task
{
    public class GetTaskResponse : BaseResponse
    {
        public DTO.Task Task { get; set; }
    }
}