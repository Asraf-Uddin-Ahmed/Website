using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ratul.Utility.Extensions
{
    public static class LinqExtension
    {
        private static readonly MethodInfo OrderByMethod =
            typeof(Queryable).GetMethods().Single(method =>
            method.Name == "OrderBy" && method.GetParameters().Length == 2);

        private static readonly MethodInfo OrderByDescendingMethod =
            typeof(Queryable).GetMethods().Single(method =>
            method.Name == "OrderByDescending" && method.GetParameters().Length == 2);


        public static IOrderedEnumerable<TSource> OrderByDirection<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, bool ascending)
        {
            return ascending ? source.OrderBy(keySelector) : source.OrderByDescending(keySelector);
        }

        public static IOrderedQueryable<TSource> OrderByDirection<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, bool ascending)
        {
            return ascending ? source.OrderBy(keySelector) : source.OrderByDescending(keySelector);
        }

        public static IQueryable<TSource> OrderByDirection<TSource>(this IQueryable<TSource> source, string propertyName, bool isAscending)
        {
            if (isAscending)
            {
                return source.OrderByProperty(propertyName);
            }
            return source.OrderByPropertyDescending(propertyName);
        }

        public static bool IsPropertyExists<TSource>(string propertyName)
        {
            return typeof(TSource).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null;
        }



        private static IQueryable<TSource> OrderByProperty<TSource>(this IQueryable<TSource> source, string propertyName)
        {
            if (typeof(TSource).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return null;
            }
            ParameterExpression paramterExpression = Expression.Parameter(typeof(TSource));
            Expression orderByProperty = Expression.Property(paramterExpression, propertyName);
            LambdaExpression lambda = Expression.Lambda(orderByProperty, paramterExpression);
            MethodInfo genericMethod = OrderByMethod.MakeGenericMethod(typeof(TSource), orderByProperty.Type);
            object ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<TSource>)ret;
        }
        private static IQueryable<TSource> OrderByPropertyDescending<TSource>(this IQueryable<TSource> source, string propertyName)
        {
            if (typeof(TSource).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return null;
            }
            ParameterExpression paramterExpression = Expression.Parameter(typeof(TSource));
            Expression orderByProperty = Expression.Property(paramterExpression, propertyName);
            LambdaExpression lambda = Expression.Lambda(orderByProperty, paramterExpression);
            MethodInfo genericMethod = OrderByDescendingMethod.MakeGenericMethod(typeof(TSource), orderByProperty.Type);
            object ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<TSource>)ret;
        }

    }
}
