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
using OrdersDb.Domain.Wrappers;

namespace OrdersDb.Data
{
    public class ObjectContextWrapper : IObjectContext
    {
        private readonly ObjectContext _objectContext;

        public ObjectContextWrapper(ObjectContext objectContext)
        {
            _objectContext = objectContext;
        }

        public ObjectContext ObjectContext
        {
            get { return _objectContext; }
        }

        public void AcceptAllChanges()
        {
            ObjectContext.AcceptAllChanges();
        }

        public void AddObject(string entitySetName, object entity)
        {
            ObjectContext.AddObject(entitySetName, entity);
        }

        public void LoadProperty(object entity, string navigationProperty)
        {
            ObjectContext.LoadProperty(entity, navigationProperty);
        }

        public void LoadProperty(object entity, string navigationProperty, MergeOption mergeOption)
        {
            ObjectContext.LoadProperty(entity, navigationProperty, mergeOption);
        }

        public void LoadProperty<TEntity>(TEntity entity, Expression<Func<TEntity, object>> selector)
        {
            ObjectContext.LoadProperty(entity, selector);
        }

        public void LoadProperty<TEntity>(TEntity entity, Expression<Func<TEntity, object>> selector, MergeOption mergeOption)
        {
            ObjectContext.LoadProperty(entity, selector, mergeOption);
        }

        public void ApplyPropertyChanges(string entitySetName, object changed)
        {
            ObjectContext.ApplyPropertyChanges(entitySetName, changed);
        }

        public TEntity ApplyCurrentValues<TEntity>(string entitySetName, TEntity currentEntity) where TEntity : class
        {
            return ObjectContext.ApplyCurrentValues(entitySetName, currentEntity);
        }

        public TEntity ApplyOriginalValues<TEntity>(string entitySetName, TEntity originalEntity) where TEntity : class
        {
            return ObjectContext.ApplyOriginalValues(entitySetName, originalEntity);
        }

        public void AttachTo(string entitySetName, object entity)
        {
            ObjectContext.AttachTo(entitySetName, entity);
        }

        public void Attach(IEntityWithKey entity)
        {
            ObjectContext.Attach(entity);
        }

        public EntityKey CreateEntityKey(string entitySetName, object entity)
        {
            return ObjectContext.CreateEntityKey(entitySetName, entity);
        }

        public ObjectSet<TEntity> CreateObjectSet<TEntity>() where TEntity : class
        {
            return ObjectContext.CreateObjectSet<TEntity>();
        }

        public ObjectSet<TEntity> CreateObjectSet<TEntity>(string entitySetName) where TEntity : class
        {
            return ObjectContext.CreateObjectSet<TEntity>(entitySetName);
        }

        public ObjectQuery<T> CreateQuery<T>(string queryString, params ObjectParameter[] parameters)
        {
            return ObjectContext.CreateQuery<T>(queryString, parameters);
        }

        public void DeleteObject(object entity)
        {
            ObjectContext.DeleteObject(entity);
        }

        public void Detach(object entity)
        {
            ObjectContext.Detach(entity);
        }

        public void Dispose()
        {
            ObjectContext.Dispose();
        }

        public object GetObjectByKey(EntityKey key)
        {
            return ObjectContext.GetObjectByKey(key);
        }

        public void Refresh(RefreshMode refreshMode, IEnumerable collection)
        {
            ObjectContext.Refresh(refreshMode, collection);
        }

        public void Refresh(RefreshMode refreshMode, object entity)
        {
            ObjectContext.Refresh(refreshMode, entity);
        }

        public Task RefreshAsync(RefreshMode refreshMode, IEnumerable collection)
        {
            return ObjectContext.RefreshAsync(refreshMode, collection);
        }

        public Task RefreshAsync(RefreshMode refreshMode, IEnumerable collection, CancellationToken cancellationToken)
        {
            return ObjectContext.RefreshAsync(refreshMode, collection, cancellationToken);
        }

        public Task RefreshAsync(RefreshMode refreshMode, object entity)
        {
            return ObjectContext.RefreshAsync(refreshMode, entity);
        }

        public Task RefreshAsync(RefreshMode refreshMode, object entity, CancellationToken cancellationToken)
        {
            return ObjectContext.RefreshAsync(refreshMode, entity, cancellationToken);
        }

        public int SaveChanges()
        {
            return ObjectContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return ObjectContext.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return ObjectContext.SaveChangesAsync(cancellationToken);
        }

        public int SaveChanges(bool acceptChangesDuringSave)
        {
            return ObjectContext.SaveChanges(acceptChangesDuringSave);
        }

        public int SaveChanges(SaveOptions options)
        {
            return ObjectContext.SaveChanges(options);
        }

        public Task<int> SaveChangesAsync(SaveOptions options)
        {
            return ObjectContext.SaveChangesAsync(options);
        }

        public Task<int> SaveChangesAsync(SaveOptions options, CancellationToken cancellationToken)
        {
            return ObjectContext.SaveChangesAsync(options, cancellationToken);
        }

        public void DetectChanges()
        {
            ObjectContext.DetectChanges();
        }

        public bool TryGetObjectByKey(EntityKey key, out object value)
        {
            return ObjectContext.TryGetObjectByKey(key, out value);
        }

        public ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, params ObjectParameter[] parameters)
        {
            return ObjectContext.ExecuteFunction<TElement>(functionName, parameters);
        }

        public ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, MergeOption mergeOption, params ObjectParameter[] parameters)
        {
            return ObjectContext.ExecuteFunction<TElement>(functionName, mergeOption, parameters);
        }

        public ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, ExecutionOptions executionOptions, params ObjectParameter[] parameters)
        {
            return ObjectContext.ExecuteFunction<TElement>(functionName, executionOptions, parameters);
        }

        public int ExecuteFunction(string functionName, params ObjectParameter[] parameters)
        {
            return ObjectContext.ExecuteFunction(functionName, parameters);
        }

        public void CreateProxyTypes(IEnumerable<Type> types)
        {
            ObjectContext.CreateProxyTypes(types);
        }

        public T CreateObject<T>() where T : class
        {
            return ObjectContext.CreateObject<T>();
        }

        public int ExecuteStoreCommand(string commandText, params object[] parameters)
        {
            return ObjectContext.ExecuteStoreCommand(commandText, parameters);
        }

        public int ExecuteStoreCommand(TransactionalBehavior transactionalBehavior, string commandText, params object[] parameters)
        {
            return ObjectContext.ExecuteStoreCommand(transactionalBehavior, commandText, parameters);
        }

        public Task<int> ExecuteStoreCommandAsync(string commandText, params object[] parameters)
        {
            return ObjectContext.ExecuteStoreCommandAsync(commandText, parameters);
        }

        public Task<int> ExecuteStoreCommandAsync(TransactionalBehavior transactionalBehavior, string commandText, params object[] parameters)
        {
            return ObjectContext.ExecuteStoreCommandAsync(transactionalBehavior, commandText, parameters);
        }

        public Task<int> ExecuteStoreCommandAsync(string commandText, CancellationToken cancellationToken, params object[] parameters)
        {
            return ObjectContext.ExecuteStoreCommandAsync(commandText, cancellationToken, parameters);
        }

        public Task<int> ExecuteStoreCommandAsync(TransactionalBehavior transactionalBehavior, string commandText, CancellationToken cancellationToken,
            params object[] parameters)
        {
            return ObjectContext.ExecuteStoreCommandAsync(transactionalBehavior, commandText, cancellationToken, parameters);
        }

        public ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, params object[] parameters)
        {
            return ObjectContext.ExecuteStoreQuery<TElement>(commandText, parameters);
        }

        public ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, ExecutionOptions executionOptions, params object[] parameters)
        {
            return ObjectContext.ExecuteStoreQuery<TElement>(commandText, executionOptions, parameters);
        }

        public ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, string entitySetName, MergeOption mergeOption, params object[] parameters)
        {
            return ObjectContext.ExecuteStoreQuery<TElement>(commandText, entitySetName, mergeOption, parameters);
        }

        public ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, params object[] parameters)
        {
            return ObjectContext.ExecuteStoreQuery<TElement>(commandText, entitySetName, executionOptions, parameters);
        }

        public Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, params object[] parameters)
        {
            return ObjectContext.ExecuteStoreQueryAsync<TElement>(commandText, parameters);
        }

        public Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, CancellationToken cancellationToken, params object[] parameters)
        {
            return ObjectContext.ExecuteStoreQueryAsync<TElement>(commandText, cancellationToken, parameters);
        }

        public Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, ExecutionOptions executionOptions, params object[] parameters)
        {
            return ObjectContext.ExecuteStoreQueryAsync<TElement>(commandText, executionOptions, parameters);
        }

        public Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, ExecutionOptions executionOptions, CancellationToken cancellationToken,
            params object[] parameters)
        {
            return ObjectContext.ExecuteStoreQueryAsync<TElement>(commandText, executionOptions, cancellationToken, parameters);
        }

        public Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, params object[] parameters)
        {
            return ObjectContext.ExecuteStoreQueryAsync<TElement>(commandText, entitySetName, executionOptions, parameters);
        }

        public Task<ObjectResult<TElement>> ExecuteStoreQueryAsync<TElement>(string commandText, string entitySetName, ExecutionOptions executionOptions, CancellationToken cancellationToken,
            params object[] parameters)
        {
            return ObjectContext.ExecuteStoreQueryAsync<TElement>(commandText, entitySetName, executionOptions, cancellationToken, parameters);
        }

        public ObjectResult<TElement> Translate<TElement>(DbDataReader reader)
        {
            return ObjectContext.Translate<TElement>(reader);
        }

        public ObjectResult<TEntity> Translate<TEntity>(DbDataReader reader, string entitySetName, MergeOption mergeOption)
        {
            return ObjectContext.Translate<TEntity>(reader, entitySetName, mergeOption);
        }

        public void CreateDatabase()
        {
            ObjectContext.CreateDatabase();
        }

        public void DeleteDatabase()
        {
            ObjectContext.DeleteDatabase();
        }

        public bool DatabaseExists()
        {
            return ObjectContext.DatabaseExists();
        }

        public string CreateDatabaseScript()
        {
            return ObjectContext.CreateDatabaseScript();
        }

        public DbConnection Connection
        {
            get { return ObjectContext.Connection; }
        }

        public string DefaultContainerName
        {
            get { return ObjectContext.DefaultContainerName; }
            set { ObjectContext.DefaultContainerName = value; }
        }

        public MetadataWorkspace MetadataWorkspace
        {
            get { return ObjectContext.MetadataWorkspace; }
        }

        public ObjectStateManager ObjectStateManager
        {
            get { return ObjectContext.ObjectStateManager; }
        }

        public int? CommandTimeout
        {
            get { return ObjectContext.CommandTimeout; }
            set { ObjectContext.CommandTimeout = value; }
        }

        public ObjectContextOptions ContextOptions
        {
            get { return ObjectContext.ContextOptions; }
        }

        public TransactionHandler TransactionHandler
        {
            get { return ObjectContext.TransactionHandler; }
        }

        public event EventHandler SavingChanges
        {
            add
            {
                ObjectContext.SavingChanges += value;
            }
            remove
            {
                ObjectContext.SavingChanges -= value;
            }
        }

        public event ObjectMaterializedEventHandler ObjectMaterialized
        {
            add { ObjectContext.ObjectMaterialized += value; }
            remove { ObjectContext.ObjectMaterialized -= value; }
        }
    }
}
