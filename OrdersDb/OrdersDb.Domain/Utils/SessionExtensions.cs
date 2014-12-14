using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Utils
{
    public static class SessionExtensions
    {
        public static string GetImagePath<TEntity>(this HttpSessionState sessionState, Expression<Func<TEntity, byte[]>> propertyLambda)
            where TEntity : EntityBase, new()
        {
            var imageId = string.Format("{0}.{1}", typeof(TEntity).Name, Common.GetPropertyName(propertyLambda));
            var fileName = string.IsNullOrEmpty(sessionState[imageId] as string)
               ? System.IO.Path.Combine(Common.GetTemporaryFolder(),
               string.Format("{0}{1}.jpg", DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss_"), System.IO.Path.GetRandomFileName()))
               : sessionState[imageId].ToString();
            sessionState[imageId] = fileName;
            return fileName;
        }

        public static void ClearImagePath<TEntity>(this HttpSessionState sessionState, Expression<Func<TEntity, byte[]>> propertyLambda)
        {
            var imageId = string.Format("{0}.{1}", typeof(TEntity).Name, Common.GetPropertyName(propertyLambda));
            sessionState[imageId] = string.Empty;
        }
    }
}
