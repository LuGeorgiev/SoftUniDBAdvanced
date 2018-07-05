using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MiniORM
{
    internal class ChangeTracker<TEntity>
        where TEntity: class, new()
    {
        private readonly List<TEntity> allEntities;
        private readonly List<TEntity> added;
        private readonly List<TEntity> removed;

        public ChangeTracker(IEnumerable<TEntity> entities)
        {
            this.allEntities = CloneEntities(entities.ToList()).ToList();

            this.added = new List<TEntity>();
            this.removed = new List<TEntity>();
        }

        //TODO
        private static IEnumerable<TEntity> CloneEntities(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<TEntity> AllEntities => this.allEntities.AsReadOnly();
        public IReadOnlyCollection<TEntity> Added => this.added.AsReadOnly();
        public IReadOnlyCollection<TEntity> Removed => this.removed.AsReadOnly();

        public void Add(TEntity item) => this.added.Add(item);
        public void Remove(TEntity item) => this.removed.Add(item);

        public IEnumerable<object> GetPrimaryKeyValues(IEnumerable<PropertyInfo>primaryKeys, TEntity entity)
        {
            var primaryKeyProperties = primaryKeys
                .Select(pi=>pi.GetValue(entity))
                .ToArray();

            return primaryKeyProperties;
        }

        public IEnumerable<TEntity> GetModifiedEntities(DbSet<TEntity> dbSet)
        {
            var modifiedEntities = new List<TEntity>();

            var primaryKeys = typeof(TEntity)
                .GetProperties()
                .Where(pi => pi.HasAttribute<KeyAttribute>())
                .ToArray();

            foreach (var proxyEntity in this.AllEntities)
            {
                var primaryKeyValues = GetPrimaryKeyValues(primaryKeys, proxyEntity)
                    .ToArray();

                var entity = dbSet.Entities
                    .Single(e => GetPrimaryKeyValues(primaryKeys, e).SequenceEqual(primaryKeyValues));

                var isModified = IsModifid(proxyEntity, entity);
                if (isModified)
                {
                    modifiedEntities.Add(entity);
                }
            }
            return modifiedEntities;
        }

        public static bool IsModifid(TEntity entity, TEntity clonedEntity)
        {
            var monitoredProtertis = typeof(TEntity).GetProperties()
                .Where(pi => AllowedSqlTypes.SqlTypes.Contains(pi.PropertyType))
                .ToArray();

            //var originalEntity = monitoredProtertis.Select(pi => pi.GetValue(clonedEntity));
            //var modifiedEntity = monitoredProtertis.Select(pi => pi.GetValue(entity));

            var modifiedProperties = monitoredProtertis
                .Where(pi => pi.GetValue(clonedEntity) != pi.GetValue(entity))
                .ToArray();

            var isModified = monitoredProtertis.Any();

            return isModified;
        }
    }
}