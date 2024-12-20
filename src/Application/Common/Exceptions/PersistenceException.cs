using System.Runtime.Serialization;

namespace Application.Common.Exceptions;
[Serializable]
public class PersistenceException : ServiceException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PersistenceException"/> class.
    /// </summary>
    public PersistenceException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PersistenceException"/> class
    /// with a specified error message.
    /// </summary>
    ///
    /// <param name="message">The message that describes the error.</param>
    public PersistenceException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PersistenceException"/> class
    /// with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    ///
    /// <param name="message">
    /// The error message that explains the reason for the exception.
    /// </param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or a null reference if no inner exception is
    /// specified.
    /// </param>
    public PersistenceException(string message, Exception innerException) : base(message, innerException) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PersistenceException"/> class with serialized data.
    /// </summary>
    ///
    /// <param name="info">
    /// The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.
    /// </param>
    /// <param name="context">
    /// The <see cref="StreamingContext"/> that contains contextual information about the source or destination.
    /// </param>
    protected PersistenceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}