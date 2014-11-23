using System.ComponentModel;

namespace OrdersDb.Domain.Services._Common.Entities
{
    /// <summary>
    /// Перечисление возможных типов доступа
    /// </summary>
    public enum AccessType
    {
        [Description("Чтение")]
        Read = 1,
        [Description("Добавление")]
        Add = 2,
        [Description("Редактирование")]
        Update = 3,
        [Description("Удаление")]
        Delete = 4
    }
}
