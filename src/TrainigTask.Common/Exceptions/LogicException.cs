using System;
using System.Collections.Generic;
using System.Linq;
using TrainingTask.Common.Errors;

namespace TrainingTask.Common.Exceptions
{
    [Serializable]
    public class LogicException : ApplicationBaseException
    {
        public List<ErrorInfo> Errors { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"></see> class.
        /// </summary>
        public LogicException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"></see> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error. </param>
        public LogicException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"></see> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
        public LogicException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"></see> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="errors">The error messages that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
        public LogicException(List<ErrorInfo> errors, Exception innerException = null) : base($"Logic error occured: {string.Join(", ", errors.Select(x => x.ToString()))}", innerException)
        {
            Errors = errors;
        }
    }
}