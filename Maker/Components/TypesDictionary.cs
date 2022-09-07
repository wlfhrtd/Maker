using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Components
{
    public class TypesDictionary
    {
        private Dictionary<string, string> types;
        // array types
        // datetime types
        // other?
        public TypesDictionary()
        {
            types = new();

            types["string"] = "System.String";
            types["boolean"] = "System.Boolean";
            types["integer"] = "System.Int32";
            types["short"] = "System.Int16";
            types["long"] = "System.Int64";
            types["float"] = "System.Single";
            types["double"] = "System.Double";
        }


        public Dictionary<string, string> Types => types;
    }
}
