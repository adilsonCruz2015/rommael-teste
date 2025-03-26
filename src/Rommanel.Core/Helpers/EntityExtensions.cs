using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

namespace Rommanel.Core.Helpers
{
    public static class EntityExtensions
    {
        public static PropertyInfo GetPrimaryKeyProperty<T>(this DbSet<T> dbSet) where T : class
        {
            var context = dbSet.GetService<ICurrentDbContext>().Context;
            var entityType = context.Model.FindEntityType(typeof(T));

            // Verifica se a entidade foi encontrada
            if (entityType == null)
                throw new InvalidOperationException($"Entity type {typeof(T).Name} not found in the model.");

            // Obtém a propriedade da chave primária
            var primaryKey = entityType.FindPrimaryKey();

            if (primaryKey == null)
                throw new InvalidOperationException("Primary key not found for the entity.");

            // Como a chave primária pode ter múltiplas colunas, pegamos a primeira (caso seja uma chave composta)
            var keyProperty = primaryKey.Properties.FirstOrDefault();
            if (keyProperty == null)
                throw new InvalidOperationException("Primary key property not found.");

            // Usa o nome da propriedade na classe (não o nome da coluna no banco de dados)
            var propertyName = keyProperty.Name;

            // Retorna a PropertyInfo com base no nome da propriedade
            return typeof(T).GetProperty(propertyName)
                   ?? throw new InvalidOperationException($"Property {propertyName} not found on {typeof(T).Name}.");
        }
    }
}
