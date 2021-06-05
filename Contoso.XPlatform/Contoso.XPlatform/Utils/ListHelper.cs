using System.Collections.Generic;
using System.Linq;

namespace Contoso.XPlatform.Utils
{
    public static class ListHelper
    {
        public static bool ExistsInList(this Dictionary<string, object> source, IEnumerable<Dictionary<string, object>> sourceList, List<string> keyFields)
            => GetByKey(source, sourceList, keyFields).SingleOrDefault() != null;

        public static Dictionary<string, object> GetExistingEntry(this Dictionary<string, object> source, IEnumerable<Dictionary<string, object>> sourceList, List<string> keyFields)
            => GetByKey(source, sourceList, keyFields).SingleOrDefault();

        private static IEnumerable<Dictionary<string, object>> GetByKey(Dictionary<string, object> source, IEnumerable<Dictionary<string, object>> sourceList, List<string> keyFields)
        {
            if (sourceList?.Any() != true)
                return new List<Dictionary<string, object>>();

            return keyFields.Aggregate
            (
                sourceList,
                (list, propertyName) => list.Where(item => item[propertyName].Equals(source[propertyName]))
            );
        }
    }
}
