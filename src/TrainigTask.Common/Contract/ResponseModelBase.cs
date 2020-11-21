using System.Collections.Generic;
using TrainingTask.Common.Errors;

namespace TrainingTask.Common.Contract
{
    public class ResponseModelBase
    {
        public string Message { get; set; }
        public IList<ErrorInfo> Errors { get; set; }
    }
}