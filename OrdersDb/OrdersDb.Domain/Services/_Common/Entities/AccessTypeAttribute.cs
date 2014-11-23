using System;

namespace OrdersDb.Domain.Services._Common.Entities
{
    /// <summary>
    /// �������� ��� ��������� ������ ��� ������ � ������������ ��������
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