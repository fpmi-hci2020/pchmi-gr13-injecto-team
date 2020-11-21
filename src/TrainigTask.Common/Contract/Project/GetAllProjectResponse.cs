using System.Collections.Generic;

namespace TrainingTask.Common.Contract.Project
{
    public class GetAllProjectResponse : BaseResponse
    {
        public IEnumerable<DTO.Project> Projects { get; set; }
    }
}