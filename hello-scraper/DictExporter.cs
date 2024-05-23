using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


using Newtonsoft.Json;

namespace hello_scraper
{
    public static class DictExporter
    {
        public static string ToJson(List<IOffer> list)
        {
            return JsonConvert.SerializeObject(list);
        }

        public static string ToMajorCounter(List<IOffer> list)
        {
            var counter =  list
               .GroupBy(x => x.major)
               .Select(group => new { Major = group.Key, Value = group.Count() });

            return JsonConvert.SerializeObject(counter, Formatting.Indented);
        }

        public static string ToLocationCounter(List<IOffer> list)
        {
            var counter = list
                .GroupBy(x => x.location)
                .Select(group => new { Location = group.Key, Value = group.Count() });
            return JsonConvert.SerializeObject (counter, Formatting.Indented);
        }
    }
}
