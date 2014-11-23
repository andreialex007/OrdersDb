using System;
using System.Runtime.Serialization;

namespace OrdersDb.Domain.Exceptions
{
    /// <summary>
    /// Общее исключение для всех исключений относящихся к текущему приложению
    /// </summary>
    public class OrdersDbCommonException : Exception
    {
        public OrdersDbCommonException()
        {
        }

        public OrdersDbCommonException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public OrdersDbCommonException(string message)
            : base(message)
        {
        }

        protected OrdersDbCommonException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
