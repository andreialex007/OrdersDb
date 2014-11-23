using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Runtime.Serialization;

namespace OrdersDb.Domain.Exceptions
{
    public class ValidationException : OrdersDbCommonException
    {
        public List<DbValidationError> Errors { get; set; }
        public string ValidationSummary { get; set; }

        public ValidationException()
        {
            Errors = new List<DbValidationError>();
        }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ValidationException(string validationSummary)
            : this()
        {
            this.ValidationSummary = validationSummary;
        }

        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ValidationException(List<DbValidationError> errors)
        {
            Errors = errors;
        }
    }
}
