using System.Collections.Generic;

namespace TrainingTask.Common.Exceptions
{
    [System.Serializable]
    public class ValidationDependenciesException : ApplicationBaseException
    {
        public ValidationDependenciesException(List<string> codes) :
            base($"Validation rules {string.Join(", ", codes ?? new List<string>())} have cyclical dependencies.")
        { }

        public ValidationDependenciesException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public ValidationDependenciesException(string message) : base(message)
        {
        }

        public ValidationDependenciesException()
        {
        }
    }
}