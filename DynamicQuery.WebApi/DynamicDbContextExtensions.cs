using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace DynamicQuery.WebApi
{
    public static class DynamicDbContextExtensions
    {
        public static IQueryable<T> DynamicWhere<T>(this IQueryable<T> source, string? input) where T : class
        {
            return string.IsNullOrEmpty(input) ? source : source.Where(input);
        }

        public static IQueryable<T> DynamicOrderBy<T>(this IQueryable<T> source, string? input) where T : class
        {
            return string.IsNullOrEmpty(input) ? source : source.OrderBy(input);
        }

        public static IQueryable<T> DynamicInclude<T>(this IQueryable<T> source, string? input) where T : class
        {
            if (string.IsNullOrEmpty(input))
            {
                return source;
            }

            foreach (var item in input.Split(','))
            {
                source = source.Include(item);
            }

            return source;
        }
    }
}