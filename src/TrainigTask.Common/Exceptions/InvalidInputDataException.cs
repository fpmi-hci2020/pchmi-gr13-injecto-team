using System;

namespace TrainingTask.Common.Exceptions
{
    [Serializable]
    public class InvalidInputDataException : ApplicationBaseException
    {
        public string Field { get; }

        public InvalidInputDataException()
        { }

        public InvalidInputDataException(string message)
            : base(message)
        { }

        public InvalidInputDataException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public InvalidInputDataException(string field, string message, Exception innerException = null)
            : base(message ?? $"Value of the field {field} is not valid.", innerException)
        {
            Field = field;
        }
    }
}