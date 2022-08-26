using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Components
{
    public class Output
    {
        public StringBuilder TemplateOutput { get; } = new(1024);

        public StringBuilder PropertiesOutput { get; } = new(1024);
    }
}
