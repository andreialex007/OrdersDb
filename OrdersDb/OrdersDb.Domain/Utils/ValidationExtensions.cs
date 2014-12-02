using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using OrdersDb.Domain.Services._Common.Entities;
using ValidationException = OrdersDb.Domain.Exceptions.ValidationException;

namespace OrdersDb.Domain.Utils
{
    public static class ValidationExtensions
    {
        /// <summary>
        /// Производит валидацию сущности и указанных дочерних сущностей
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="childEntitiesAction"></param>
        /// <returns></returns>
        public static List<DbValidationError> Validate<T>(this T entity,
            params Expression<Func<T, IEnumerable<EntityBase>>>[] childEntitiesAction) where T : EntityBase
        {
            var validationErrors = new List<DbValidationError>();
            validationErrors.AddRange(entity.GetValidationErrors());

            foreach (var action in childEntitiesAction)
            {
                var childErrors = action.Compile()
                    .Invoke(entity)
                    .SelectMany(x => x.Validate());
                validationErrors.AddRange(childErrors);
            }
            return validationErrors;
        }

        /// <summary>
        /// Возвращает список ошибок валидации
        /// </summary>
        /// <typeparam name="T">Тип сущности для валидации</typeparam>
        /// <param name="entityBase">экземпляр сущности для валидации</param>
        /// <param name="propertiesToValidate"></param>
        /// <returns>Список ошибок валидации</returns>
        public static IEnumerable<DbValidationError> GetValidationErrors<T>(this T entityBase, params Expression<Func<T, object>>[] propertiesToValidate) where T : EntityBase
        {
            return propertiesToValidate.Length != 0
                ? GetErrorsForProperties(entityBase, propertiesToValidate)
                : GetErrorsForObject(entityBase);
        }

        private static IEnumerable<DbValidationError> GetErrorsForObject<T>(this T entityBase) where T : EntityBase
        {
            //Собираем все ошибки для кажого аттрибута
            var validationContext = new ValidationContext(entityBase, null, null);
            var errors = new List<ValidationResult>();
            Validator.TryValidateObject(entityBase, validationContext, errors, true);
            return errors.SelectMany(x => x.MemberNames.Select(m => new DbValidationError(m, x.ErrorMessage)));
        }

        private static IEnumerable<DbValidationError> GetErrorsForProperties<T>(this T entityBase,
            params Expression<Func<T, object>>[] propertiesToValidate) where T : EntityBase
        {

            //Собираем все ошибки для кажого аттрибута
            var validationErrors = new List<DbValidationError>();
            foreach (var action in propertiesToValidate)
            {
                var propertyName = string.Empty;
                var body = action.Body as MemberExpression;
                if (body != null)
                    propertyName = body.Member.Name;

                var expression = action.Body as UnaryExpression;
                if (expression != null)
                    propertyName = ((MemberExpression)expression.Operand).Member.Name;

                var propertyValue = action.Compile().Invoke(entityBase);
                var validationContext = new ValidationContext(entityBase)
                {
                    MemberName = propertyName
                };
                var errors = new List<ValidationResult>();
                Validator.TryValidateProperty(propertyValue, validationContext, errors);
                validationErrors.AddRange(errors.Select(x => new DbValidationError(propertyName, x.ErrorMessage)));
            }
            return validationErrors;
        }

        /// <summary>
        /// Выбрасывает ValidationException если список имеет ошибки
        /// </summary>
        /// <param name="errors"></param>
        public static void ThrowIfHasErrors(this IEnumerable<DbValidationError> errors)
        {
            var arrorsList = errors.ToList();
            if (arrorsList.Any())
                throw new ValidationException(arrorsList);
        }
    }
}
