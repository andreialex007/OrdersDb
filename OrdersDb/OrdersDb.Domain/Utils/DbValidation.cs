using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrdersDb.Domain.Utils
{
    public static class DbValidation
    {
        public static DbValidationError ErrorFor<TSource>(Expression<Func<TSource, object>> propertyLambda, string errorText) where TSource : new()
        {
            var propertyName = new TSource().GetPropertyName(propertyLambda);
            return new DbValidationError(propertyName, errorText);
        }
    }
}
