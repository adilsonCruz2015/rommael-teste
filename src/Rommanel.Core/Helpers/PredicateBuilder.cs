using System.Linq.Expressions;

namespace Rommanel.Core.Helpers
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> Combine<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            // Combine as duas expressões com "AND"
            var body = Expression.AndAlso(
                Expression.Invoke(expr1, parameter),
                Expression.Invoke(expr2, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public static Expression<Func<T, bool>> NotNull<T>(Expression<Func<T, bool>> expr)
        {
            // Para garantir que a propriedade não seja null antes de fazer o filtro
            var parameter = expr.Parameters[0];
            var body = Expression.AndAlso(
                Expression.NotEqual(Expression.Property(parameter, "Name"), Expression.Constant(null, typeof(string))),
                expr.Body
            );
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }

}
