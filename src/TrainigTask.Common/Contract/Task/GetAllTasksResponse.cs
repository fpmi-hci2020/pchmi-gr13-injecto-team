using System.Collections.Generic;

namespace TrainingTask.Common.Contract.Task
{
    public class GetAllTasksResponse : BaseResponse
    {
        public IEnumerable<DTO.Task> Tasks { get; set; }
    }
}