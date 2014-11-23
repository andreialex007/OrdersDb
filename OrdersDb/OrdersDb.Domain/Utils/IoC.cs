using System;

namespace OrdersDb.Domain.Utils
{
    public static class IoC
    {
        public static Func<Type, object> ResolvingExpression;

        public static T Resolve<T>() where T : class
        {
            return ResolvingExpression(typeof(T)) as T;
        }
    }
}
