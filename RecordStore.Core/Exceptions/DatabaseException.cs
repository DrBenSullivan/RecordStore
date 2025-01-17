namespace RecordStore.Core.Exceptions
{
    public class DatabaseException<T> : Exception
    {
        public Type ValueType = typeof(T);
        public T Value { get; set; }

        public DatabaseException(T value) 
        { 
            Value = value;
        }

        public DatabaseException(T value, string message) : base(message)
        { 
            Value = value;
        }

        public DatabaseException(T value, string message, Exception? innerException) : base(message, innerException) 
        { 
            Value = value;
        }
    }
}
