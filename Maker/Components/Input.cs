using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Components
{
    public class Input
    {
        // TODO

        // args
        // args validation
        // options pattern?
        // static input buffer?

        public string ClassName { get; set; }

        public Dictionary<string, string> Properties { get; } = new(10);
    }
}
