using System;

namespace OrdersDb.Domain.Services._Common.Entities
{
    /// <summary>
    /// Аттрибут для индикации экшена как экшена с ограниченным доступом
    /// </summary>
    public class AccessTypeAttribute : Attribute
    {
        public AccessType AccessType;

        public AccessTypeAttribute(AccessType accessType)
        {
            AccessType = accessType;
        }
    }
}