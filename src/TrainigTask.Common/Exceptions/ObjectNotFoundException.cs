using System;

namespace TrainingTask.Common.Exceptions
{
    [Serializable]
    public class ObjectNotFoundException : ApplicationBaseException
    {
        public string Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"></see> class.
        /// </summary>
        public ObjectNotFoundException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"></see> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error. </param>
        public ObjectNotFoundException(string message) : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"></see> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
        public ObjectNotFoundException(string message, Exception innerException) : base(message, innerException)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"></see> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
        /// <param name="id">Attempted id </param>
        public ObjectNotFoundException(string id, string message, Exception innerException = null) : base(message ?? $"Entity with id = {id} is not found.", innerException)
        {
            Id = id;
        }
    }
}