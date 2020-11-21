using System;

namespace TrainingTask.Common.Errors
{
    [Serializable]
    public class ConcurrencyAccessErrorInfo : BusinessErrorInfo
    {
        public ConcurrencyAccessErrorInfo()
            : base()
        {
        }

        public ConcurrencyAccessErrorInfo(string message)
            : base(message)
        {
        }

        public ConcurrencyAccessErrorInfo(string key, string message)
            : base(key, message)
        {
        }
    }
}