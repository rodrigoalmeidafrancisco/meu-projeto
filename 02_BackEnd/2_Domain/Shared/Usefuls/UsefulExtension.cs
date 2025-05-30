using System.Linq.Expressions;

namespace Shared.Usefuls
{
    public static class UsefulExtension
    {
        public static bool IsList<T>(this T obj)
        {
            if (typeof(T).IsGenericType)
            {
                if (typeof(T).GetGenericTypeDefinition() == typeof(IList<>) ||
                    typeof(T).GetGenericTypeDefinition() == typeof(List<>) ||
                    typeof(T).GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return true;
                }
            }

            return false;
        }

        public static Expression<Func<T, bool>> AddExpression<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));
            var body = Expression.AndAlso(Expression.Invoke(expr1, parameter), Expression.Invoke(expr2, parameter));
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
