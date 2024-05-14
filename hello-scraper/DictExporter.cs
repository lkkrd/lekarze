using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


using Newtonsoft.Json;

namespace hello_scraper
{
    internal static class DictExporter
    {
        public static string ToJson(Dictionary<string, int> dict)
        {
            return JsonConvert.SerializeObject(dict);
        }
    }
}
