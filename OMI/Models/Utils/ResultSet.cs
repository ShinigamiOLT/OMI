using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace OMI.Models.Utils
{
    internal static class ResultSet<T> where T : class
    {
        public static List<T> GetPaginatedResult(string search, string sortOrder, int start, int length, IQueryable<T> dtResult)
        {
            return GetFilteredOrderedResult(search, sortOrder, dtResult)
                .Skip(start)
                .Take(length)
                .ToList();
        }

        public static IQueryable<T> GetFilteredResult(string search, IQueryable<T> dtResult)
        {
            return FilterResult(search, dtResult);
        }

        public static IQueryable<T> GetFilteredOrderedResult(string search, string sortOrder, IQueryable<T> dtResult)
        {
            return GetFilteredResult(search, dtResult)
                .SortBy(sortOrder);
        }

        public static int Count(string search, IQueryable<T> dtResult)
        {
            return GetFilteredResult(search, dtResult).Count();
        }

        private static IQueryable<T> FilterResult(string search, IEnumerable<T> dtResult)
        {
            return dtResult
                .AsEnumerable()
                .Where(p => Search(p, search))
                .AsQueryable();
        }

        private static bool Search(T p, string search)
        {
            var result = (string.IsNullOrWhiteSpace(search));
            if (result) return result;

            search = search.ToLower();
            return p.GetType()
                .GetProperties()
                .Select(property => property.GetValue(p))
                .Aggregate(
                    result,
                    (current, value) =>
                    {
                        var val = value ?? "";
                        return current || (val.ToString().ToLower().Contains(search));
                    }
                );
        }
    }
}