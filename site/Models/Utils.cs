using System.Collections.Generic;
using System.Linq;

namespace site.Models
{
    public static class Utils
    {
        public static List<List<T>> Compare<T>(List<T> old, List<T> new_)
        {
            var similar = old.Intersect(new_);
            var delete = old.Where(u => u != null && similar.All(p => !u.Equals(p))).ToList();
            var add = new_.Where(u => u != null && similar.All(p => !u.Equals(p))).ToList();
            return new List<List<T>> {delete, add};
        }
    }
}