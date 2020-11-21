using System;

namespace TrainingTask.Common.Exceptions
{
    [Serializable]
    public class ConcurrencyException : ApplicationBaseException
    {
        public string Id { get; }
        public int Version { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"></see> class.
        /// </summary>
        public ConcurrencyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"></see> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error. </param>
        public ConcurrencyException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"></see> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
        public ConcurrencyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"></see> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="id">Attempted entity id</param>
        /// <param name="version">Attempted entity version</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
        public ConcurrencyException(string id, int version, Exception innerException) : base($"Entity {id} with version {version} already modified", innerException)
        {
            Id = id;
            Version = version;
        }
    }
}