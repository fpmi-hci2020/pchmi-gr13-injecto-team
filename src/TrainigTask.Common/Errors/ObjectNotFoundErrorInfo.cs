using System;

namespace TrainingTask.Common.Errors
{
    [Serializable]
    public class ObjectNotFoundErrorInfo : BusinessErrorInfo
    {
        public ObjectNotFoundErrorInfo()
            : this(Guid.Empty, string.Empty, string.Empty)
        {
        }

        public ObjectNotFoundErrorInfo(string message)
            : this(Guid.Empty, string.Empty, message)
        {
        }

        public ObjectNotFoundErrorInfo(Guid id)
            : this(id, string.Empty)
        {
        }

        public ObjectNotFoundErrorInfo(string key, string message)
            : this(Guid.Empty, key, message)
        {
        }

        public ObjectNotFoundErrorInfo(Guid id, string message)
            : this(id, string.Empty, message)
        {
            Id = id;
        }

        public ObjectNotFoundErrorInfo(Guid id, string key, string message)
            : base(key, message)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}