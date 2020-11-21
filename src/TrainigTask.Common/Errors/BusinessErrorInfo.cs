using System;

namespace TrainingTask.Common.Errors
{
    [Serializable]
    public class BusinessErrorInfo : ErrorInfo
    {
        public BusinessErrorInfo()
            : this(key: string.Empty, message: string.Empty)
        {
        }

        public BusinessErrorInfo(string message)
            : base(message)
        {
        }

        public BusinessErrorInfo(string key, string message)
            : base(key, message)
        {
        }
    }
}
