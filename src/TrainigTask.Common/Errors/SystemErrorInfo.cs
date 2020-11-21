﻿using System;

namespace TrainingTask.Common.Errors
{
    [Serializable]
    public class SystemErrorInfo : ErrorInfo
    {
        public SystemErrorInfo()
            : this(string.Empty, string.Empty)
        {
        }

        public SystemErrorInfo(string message)
            : base(message)
        {
        }

        public SystemErrorInfo(string key, string message)
            : base(key, message)
        {
        }
    }
}