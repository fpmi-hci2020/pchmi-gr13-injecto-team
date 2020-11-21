using System;

namespace TrainingTask.Common.Errors
{
    [Serializable]
    public class NotAuthorizedErrorInfo : BusinessErrorInfo
    {
        public NotAuthorizedErrorInfo()
            : base()
        {
        }

        public NotAuthorizedErrorInfo(string message)
            : base(message)
        {
        }

        public NotAuthorizedErrorInfo(string key, string message)
            : base(key, message)
        {
        }
    }
}