using System;

namespace TrainingTask.Common.Errors
{
    [Serializable]
    public class SecurityErrorInfo : BusinessErrorInfo
    {
        public SecurityErrorInfo()
            : base()
        {
        }

        public SecurityErrorInfo(string message)
            : base(message)
        {
        }

        public SecurityErrorInfo(string key, string message)
            : base(key, message)
        {
        }
    }
}