using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OrdersDb.Domain.Wrappers
{
    public interface IObjectContext
    {
        ObjectContext ObjectContext { get; }
        DbConnection Connection { get; }
        string DefaultContainerName { get; set; }
        MetadataWorkspace MetadataWorkspace { get; }
        ObjectStateManager ObjectStateManager { get; }
        int? CommandTimeout { get; set; }
        ObjectContextOptions ContextOptions { get; }
        TransactionHandler TransactionHandler { get; }
        void AcceptAllChanges();
        void AddObject(string entitySetName, object entity);
        void LoadProperty(object entity, string navigationProperty);
        void LoadProperty(object entity, string navigationProperty, MergeOption mergeOption);
        void LoadProperty<TEntity>(TEntity entity, Expression<Func<TEntity, object>> selector);
        void LoadProperty<TEntity>(TEntity entity, Expression<Func<TEntity, object>> selector, MergeOption mergeOption);
        void ApplyPropertyChanges(string entitySetName, object changed);
        TEntity ApplyCurrentValues<TEntity>(string entitySetName, TEntity currentEntity) where TEntity : class;
        TEntity ApplyOriginalValues<TEntity>(string entitySetName, TEntity originalEntity) where TEntity : class;
        void AttachTo(string entitySetName, object entity);
        void Attach(IEntityWithKey entity);
        EntityKey CreateEntityKey(string entitySetName, object entity);
        ObjectSet<TEntity> CreateObjectSet<TEntity>() where TEntity : class;
        ObjectSet<TEntity> CreateObjectSet<TEntity>(string entitySetName) where TEntity : class;
        ObjectQuery<T> CreateQuery<T>(string queryString, params ObjectParameter[] parameters);
        void DeleteObject(object entity);
        void Detach(object entity);
        void Dispose();
        object GetObjectByKey(EntityKey key);
        void Refresh(RefreshMode refreshMode, IEnumerable collection);
        void Refresh(RefreshMode refreshMode, object entity);
        Task RefreshAsync(RefreshMode refreshMode, IEnumerable collection);
        Task RefreshAsync(RefreshMode refreshMode, IEnumerable collection, CancellationToken cancellationToken);
        Task RefreshAsync(RefreshMode refreshMode, object entity);
        Task RefreshAsync(RefreshMode refreshMode, object entity, CancellationToken cancellationToken);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges(bool acceptChangesDuringSave);
        int SaveChanges(SaveOptions options);
        Task<int> SaveChangesAsync(SaveOptions options);
        Task<int> SaveChangesAsync(SaveOptions options, CancellationToken cancellationToken);
        void DetectChanges();
        bool TryGetObjectByKey(EntityKey key, out object value);
        ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, params ObjectParameter[] parameters);
        ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, MergeOption mergeOption, params ObjectParameter[] parameters);
        ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, ExecutionOptions executionOptions, params ObjectParameter[] parameters);
        int ExecuteFunction(string functionName, params ObjectParameter[] parameters);
        void CreateProxyTypes(IEnumerable<Type> types);
        T CreateObject<T>() where T : class;
        int ExecuteStoreCommand(string commandText, params object[] parameters);
        int ExecuteStoreCommand(TransactionalBehavior transactionalBehavior, string commandText, params object[] parameters);
        Task<int> ExecuteStoreCommandAsync(string commandText, params object[] parameters);
        Task<int> ExecuteStoreCommandAsync(TransactionalBehavior transactionalBehavior, string commandText, params object[] parameters);
        Task<int> ExecuteStoreCommandAsync(string commandText, CancellationToken cancellationToken, params object[] parameters);

        Task<int> ExecuteStoreCommandAsync(TransactionalBehavior transactionalBehavior, string commandText, CancellationToken cancellationToken,
            params object[] parameters);

        ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, params object[] parameters);
        ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, ExecutionOptions executionOptions, params object[] parameters);
        ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, string entitySetName, MergeOption mergeOption, params object[] parameters);
        ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, params object[] parameters);
        Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, params object[] parameters);
        Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, CancellationToken cancellationToken, params object[] parameters);
        Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, ExecutionOptions executionOptions, params object[] parameters);

        Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, ExecutionOptions executionOptions, CancellationToken cancellationToken,
            params object[] parameters);

        Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, params object[] parameters);

        Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, CancellationToken cancellationToken,
            params object[] parameters);

        ObjectResult<TElement> Translate<TElement>(DbDataReader reader);
        ObjectResult<TEntity> Translate<TEntity>(DbDataReader reader, string entitySetName, MergeOption mergeOption);
        void CreateDatabase();
        void DeleteDatabase();
        bool DatabaseExists();
        string CreateDatabaseScript();
        event EventHandler SavingChanges;
        event ObjectMaterializedEventHandler ObjectMaterialized;
    }
}